using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel; // Added for INotifyPropertyChanged
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;

namespace DarkUI.Collections
{
    /// <summary>
    /// A thread-safe observable list that supports undo/redo, batch updates, validation, and data-binding notifications.
    /// Implements INotifyCollectionChanged and INotifyPropertyChanged.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    [Serializable]
    public class ObservableList<T> : IList<T>, IDisposable, IReadOnlyList<T>, ISerializable,
                                      INotifyCollectionChanged, INotifyPropertyChanged // Added INotifyPropertyChanged
    {
        #region Fields

        // Changed to internal to allow ReadOnlyObservableList to access it efficiently under parent's lock.
        // Consumers should use public accessors like GetEnumerator(), ToArray(), or AsThreadSafeReadOnly().
        internal readonly List<T> _innerList;
        private readonly ReaderWriterLockSlim _lock;
        private readonly List<Change> _undoStack;
        private readonly List<Change> _redoStack;
        private bool _disposed;
        private bool _isReadOnly;
        private int _updateCount = 0;
        private bool _hasChanges = false; // Tracks if changes occurred during a batch update

        #endregion

        #region Properties

        /// <summary>
        /// Gets the lock used for synchronization. Use with caution.
        /// </summary>
        public ReaderWriterLockSlim Lock => _lock;

        // Removed direct public access to _innerList to enforce controlled access and thread safety.
        // Use GetInnerListCopy(), ToArray(), AsReadOnly(), or AsThreadSafeReadOnly() instead.
        // public List<T> InnerList => _innerList; // Removed

        /// <summary>
        /// Gets the number of elements contained in the list.
        /// </summary>
        public int Count
        {
            get
            {
                _lock.EnterReadLock();
                try
                {
                    return _innerList.Count;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the list is read-only.
        /// This property implements INotifyPropertyChanged.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                // _isReadOnly is typically read without lock in simple property getters if modification is locked.
                // However, to be strictly safe with ReaderWriterLockSlim if reads need to be consistent with writes:
                _lock.EnterReadLock();
                try
                {
                    return _isReadOnly;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }
            // No public setter; use SetReadOnly method.
        }


        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element.</param>
        public T this[int index]
        {
            get => GetItem(index);
            set => SetItem(index, value);
        }

        /// <summary>
        /// Gets or sets the maximum number of undo steps to retain. Defaults to <see cref="int.MaxValue"/>.
        /// </summary>
        public int MaxUndoSteps { get; set; } = int.MaxValue;

        #endregion

        #region Events

        /// <summary>Occurs when items are added to the list.</summary>
        public event EventHandler<ObservableListModified<T>> ItemsAdded;
        /// <summary>Occurs when items are removed from the list.</summary>
        public event EventHandler<ObservableListModified<T>> ItemsRemoved;
        /// <summary>Occurs when items in the list are modified.</summary>
        public event EventHandler<ObservableListModified<T>> ItemsModified;
        /// <summary>Occurs when items are moved within the list.</summary>
        public event EventHandler<ObservableListMoved<T>> ItemsMoved;
        /// <summary>Occurs when an item is inserted into the list.</summary>
        public event EventHandler<ItemInsertedEventArgs<T>> ItemInserted;
        /// <summary>Occurs when an item is replaced in the list.</summary>
        public event EventHandler<ItemReplacedEventArgs<T>> ItemReplaced;
        /// <summary>Occurs when the collection is reset.</summary>
        public event EventHandler<EventArgs> CollectionReset;
        /// <summary>Occurs before an item is added or modified to allow validation.</summary>
        public event EventHandler<ItemValidationEventArgs<T>> ItemValidating;

        /// <summary>
        /// Occurs when the collection changes, as defined by <see cref="INotifyCollectionChanged"/>.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public ObservableList()
        {
            _innerList = new List<T>();
            _lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion); // Consider NoRecursion for performance/simplicity if applicable
            _undoStack = new List<Change>();
            _redoStack = new List<Change>();
        }

        public ObservableList(int capacity) : this()
        {
            _innerList = new List<T>(capacity);
        }

        public ObservableList(IEnumerable<T> collection) : this()
        {
            _innerList = new List<T>(collection ?? throw new ArgumentNullException(nameof(collection)));
        }

        protected ObservableList(SerializationInfo info, StreamingContext context) : this()
        {
            // Note: _lock, _undoStack, _redoStack are not typically serialized.
            // They are runtime states. Consider if they need custom serialization.
            var items = (List<T>)info.GetValue("Items", typeof(List<T>));
            if (items != null) _innerList = new List<T>(items); // Create new list from serialized items
            _isReadOnly = info.GetBoolean("IsReadOnly");
            MaxUndoSteps = info.GetInt32("MaxUndoSteps");
        }

        #endregion

        #region Core Methods

        public void Add(T item)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnlyAndDisposed();
                ValidateItem(item);
                int index = _innerList.Count;
                _innerList.Add(item);
                RecordChange(ChangeType.Add, index, default(T), item); // OldValue for Add is conventionally null/default
                OnItemsAdded(new List<T> { item });
                OnItemInserted(index, item);
                NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction.Add, item, index);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnlyAndDisposed();
                var items = collection.ToList(); // Materialize to avoid issues with multiple enumeration & locking
                if (items.Count == 0) return;

                foreach (var item in items) ValidateItem(item);

                int startIndex = _innerList.Count;
                _innerList.AddRange(items);
                RecordChange(ChangeType.AddRange, startIndex, null, new List<T>(items)); // Store a copy for undo
                OnItemsAdded(items);
                NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction.Add, items, startIndex);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public bool Remove(T item)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnlyAndDisposed();
                int index = _innerList.IndexOf(item);
                if (index < 0) return false;

                // Item to be removed is _innerList[index], which is 'item' if found by reference or value equality.
                // For undo, we need the actual item removed.
                T removedItem = _innerList[index];
                _innerList.RemoveAt(index);
                RecordChange(ChangeType.Remove, index, removedItem, default(T));
                OnItemsRemoved(new List<T> { removedItem });
                NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction.Remove, removedItem, index);
                return true;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void RemoveAt(int index)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnlyAndDisposed();
                ValidateIndex(index);
                var item = _innerList[index];
                _innerList.RemoveAt(index);
                RecordChange(ChangeType.Remove, index, item, default(T));
                OnItemsRemoved(new List<T> { item });
                NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction.Remove, item, index);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Clear()
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnlyAndDisposed();
                if (_innerList.Count == 0) return;
                var removedItems = new List<T>(_innerList); // Copy for undo and event
                _innerList.Clear();
                RecordChange(ChangeType.Clear, 0, removedItems, null);
                OnItemsRemoved(removedItems);
                OnCollectionReset(); // Custom event
                NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction.Reset);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Insert(int index, T item)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnlyAndDisposed();
                ValidateIndex(index, allowEnd: true); // Allow insert at Count
                ValidateItem(item);
                _innerList.Insert(index, item);
                RecordChange(ChangeType.Insert, index, default(T), item);
                OnItemsAdded(new List<T> { item }); // Semantically similar to Add for single item
                OnItemInserted(index, item);
                NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction.Add, item, index);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        #endregion

        #region Batch Update Methods

        public void BeginUpdate()
        {
            _lock.EnterWriteLock();
            try
            {
                CheckDisposed();
                _updateCount++;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void EndUpdate()
        {
            _lock.EnterWriteLock();
            try
            {
                CheckDisposed();
                if (_updateCount > 0)
                {
                    _updateCount--;
                    if (_updateCount == 0 && _hasChanges)
                    {
                        OnCollectionReset(); // Custom event
                        NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction.Reset);
                        _hasChanges = false;
                    }
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        #endregion

        #region Undo/Redo Support

        /// <summary>Undoes the last change. Does nothing if the undo stack is empty.</summary>
        public void Undo()
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnlyAndDisposed();
                if (_undoStack.Count == 0) return;

                var change = _undoStack[_undoStack.Count - 1];
                _undoStack.RemoveAt(_undoStack.Count - 1);

                ApplyChange(change, reverse: true);
                _redoStack.Add(change); // Add to redo stack

                NotifyCollectionChangedForUndoRedo(change, reverse: true);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>Redoes the last undone change. Does nothing if the redo stack is empty.</summary>
        public void Redo()
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnlyAndDisposed();
                if (_redoStack.Count == 0) return;

                var change = _redoStack[_redoStack.Count - 1];
                _redoStack.RemoveAt(_redoStack.Count - 1);

                ApplyChange(change, reverse: false);
                _undoStack.Add(change); // Add back to undo stack (within MaxUndoSteps limit handled by RecordChange)

                NotifyCollectionChangedForUndoRedo(change, reverse: false);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        private void RecordChange(ChangeType type, int index, object oldValue, object newValue)
        {
            CheckDisposed(); // Called internally, but good practice
            if (_updateCount > 0)
            {
                _hasChanges = true; // Mark that changes happened during batch update
                return; // Don't record individual changes during batch update
            }

            var change = new Change(type, index, oldValue, newValue);
            _undoStack.Add(change);

            if (_undoStack.Count > MaxUndoSteps && MaxUndoSteps >= 0) // Ensure MaxUndoSteps is not negative
            {
                _undoStack.RemoveAt(0);
            }
            _redoStack.Clear(); // Any new action clears the redo stack
        }

        private void ApplyChange(Change change, bool reverse)
        {
            switch (change.Type)
            {
                case ChangeType.Add:
                case ChangeType.Insert: // Add and Insert are similar for undo/redo logic
                    if (reverse) _innerList.RemoveAt(change.Index); // Undo Add/Insert
                    else _innerList.Insert(change.Index, (T)change.NewValue); // Redo Add/Insert
                    break;
                case ChangeType.Remove:
                    if (reverse) _innerList.Insert(change.Index, (T)change.OldValue); // Undo Remove
                    else _innerList.RemoveAt(change.Index); // Redo Remove
                    break;
                case ChangeType.Replace:
                    _innerList[change.Index] = reverse ? (T)change.OldValue : (T)change.NewValue;
                    break;
                case ChangeType.Move:
                    var movedItem = (T)change.NewValue; // Item that was moved
                    int originalOldIndex = (int)change.OldValue;
                    int originalNewIndex = change.Index;
                    if (reverse) // Move item from originalNewIndex back to originalOldIndex
                    {
                        _innerList.RemoveAt(originalNewIndex);
                        _innerList.Insert(originalOldIndex, movedItem);
                    }
                    else // Move item from originalOldIndex to originalNewIndex
                    {
                        _innerList.RemoveAt(originalOldIndex);
                        _innerList.Insert(originalNewIndex, movedItem);
                    }
                    break;
                case ChangeType.AddRange: // Used for AddRange, InsertRange operations
                    var itemsAdded = (IList<T>)change.NewValue;
                    if (reverse) _innerList.RemoveRange(change.Index, itemsAdded.Count);
                    else _innerList.InsertRange(change.Index, itemsAdded);
                    break;
                case ChangeType.RemoveRange: // Specific type for RemoveRange
                    var itemsRemoved = (IList<T>)change.OldValue;
                    if (reverse) _innerList.InsertRange(change.Index, itemsRemoved);
                    else _innerList.RemoveRange(change.Index, itemsRemoved.Count);
                    break;
                case ChangeType.Clear:
                    var clearedItems = (IList<T>)change.OldValue;
                    if (reverse) _innerList.AddRange(clearedItems);
                    else _innerList.Clear();
                    break;
            }
        }

        private void NotifyCollectionChangedForUndoRedo(Change change, bool reverse)
        {
            if (_updateCount > 0)
            {
                _hasChanges = true;
                return;
            }

            switch (change.Type)
            {
                case ChangeType.Add:
                case ChangeType.Insert:
                    NotifyCollectionAndPropertiesChanged(
                        reverse ? NotifyCollectionChangedAction.Remove : NotifyCollectionChangedAction.Add,
                        (T)change.NewValue, change.Index);
                    break;
                case ChangeType.Remove:
                    NotifyCollectionAndPropertiesChanged(
                        reverse ? NotifyCollectionChangedAction.Add : NotifyCollectionChangedAction.Remove,
                        (T)change.OldValue, change.Index);
                    break;
                case ChangeType.Replace:
                    NotifyCollectionAndPropertiesChanged(
                        NotifyCollectionChangedAction.Replace,
                        reverse ? (T)change.OldValue : (T)change.NewValue, // New item for notification
                        reverse ? (T)change.NewValue : (T)change.OldValue, // Old item for notification
                        change.Index);
                    break;
                case ChangeType.Move:
                    NotifyCollectionAndPropertiesChanged(
                        NotifyCollectionChangedAction.Move,
                        (T)change.NewValue, // The item
                        reverse ? (int)change.OldValue : change.Index, // Target index
                        reverse ? change.Index : (int)change.OldValue  // Source index
                    );
                    break;
                case ChangeType.AddRange: // AddRange or InsertRange
                    var itemsAdded = (IList<T>)change.NewValue;
                    NotifyCollectionAndPropertiesChanged(
                        reverse ? NotifyCollectionChangedAction.Remove : NotifyCollectionChangedAction.Add,
                        itemsAdded, change.Index);
                    break;
                case ChangeType.RemoveRange:
                     var itemsRemoved = (IList<T>)change.OldValue;
                    NotifyCollectionAndPropertiesChanged(
                        reverse ? NotifyCollectionChangedAction.Add : NotifyCollectionChangedAction.Remove,
                        itemsRemoved, change.Index);
                    break;
                case ChangeType.Clear:
                    NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction.Reset);
                    break;
            }
        }

        private void ClearUndoRedoStacks()
        {
            _undoStack.Clear();
            _redoStack.Clear();
        }

        #endregion

        #region Additional Features

        public void SetReadOnly(bool readOnly)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckDisposed();
                if (_isReadOnly != readOnly)
                {
                    _isReadOnly = readOnly;
                    OnPropertyChanged(nameof(IsReadOnly));
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Returns a read-only <see cref="System.Collections.ObjectModel.ReadOnlyCollection{T}"/> wrapper for the current list.
        /// Note: This wrapper is NOT thread-safe if the underlying ObservableList is modified concurrently
        /// by other threads without proper synchronization on the ObservableList's Lock.
        /// For a thread-safe read-only view, use <see cref="AsThreadSafeReadOnly"/>.
        /// </summary>
        public System.Collections.ObjectModel.ReadOnlyCollection<T> AsReadOnly()
        {
            _lock.EnterReadLock();
            try
            {
                // It's conventional for AsReadOnly() to wrap the live list.
                // User must understand thread-safety implications.
                return new System.Collections.ObjectModel.ReadOnlyCollection<T>(_innerList);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }


        /// <summary>
        /// Returns a thread-safe read-only view of the list.
        /// </summary>
        public IReadOnlyList<T> AsThreadSafeReadOnly() => new ReadOnlyObservableList<T>(this);

        public void Replace(T oldItem, T newItem)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnlyAndDisposed();
                ValidateItem(newItem);
                int index = _innerList.IndexOf(oldItem);
                if (index < 0) throw new ArgumentException("Item to replace not found in collection.", nameof(oldItem));

                T actualOldItem = _innerList[index]; // Get the exact instance from the list
                _innerList[index] = newItem;

                RecordChange(ChangeType.Replace, index, actualOldItem, newItem);
                OnItemsModified(new List<(T OldItem, T NewItem)> { (actualOldItem, newItem) });
                OnItemReplaced(actualOldItem, newItem); // Specific event
                NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction.Replace, newItem, actualOldItem, index);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Sorts the elements in the entire list using the specified comparer.
        /// This operation clears the undo/redo stack.
        /// </summary>
        public void Sort(IComparer<T> comparer = null)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnlyAndDisposed();
                if (_innerList.Count <= 1) return;
                _innerList.Sort(comparer ?? Comparer<T>.Default);
                ClearUndoRedoStacks();
                NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction.Reset);
                // Consider raising a specific "Sorted" event if needed
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Sorts the elements in the entire list using the specified comparison.
        /// This operation clears the undo/redo stack.
        /// </summary>
        public void Sort(Comparison<T> comparison)
        {
            if (comparison == null) throw new ArgumentNullException(nameof(comparison));
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnlyAndDisposed();
                if (_innerList.Count <= 1) return;
                _innerList.Sort(comparison);
                ClearUndoRedoStacks();
                NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction.Reset);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Reverses the order of the elements in the entire list.
        /// This operation clears the undo/redo stack.
        /// </summary>
        public void Reverse()
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnlyAndDisposed();
                if (_innerList.Count <= 1) return;
                _innerList.Reverse();
                ClearUndoRedoStacks();
                NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction.Reset);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public IEnumerable<T> FindAll(Predicate<T> match)
        {
            if (match == null) throw new ArgumentNullException(nameof(match));
            _lock.EnterReadLock();
            try
            {
                CheckDisposed();
                return _innerList.FindAll(match).AsReadOnly(); // Returns a read-only copy
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public T Find(Predicate<T> match)
        {
            if (match == null) throw new ArgumentNullException(nameof(match));
            _lock.EnterReadLock();
            try
            {
                CheckDisposed();
                return _innerList.Find(match);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public T FindLast(Predicate<T> match)
        {
            if (match == null) throw new ArgumentNullException(nameof(match));
            _lock.EnterReadLock();
            try
            {
                CheckDisposed();
                return _innerList.FindLast(match);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int FindIndex(Predicate<T> match)
        {
            if (match == null) throw new ArgumentNullException(nameof(match));
            _lock.EnterReadLock();
            try
            {
                CheckDisposed();
                return _innerList.FindIndex(match);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
        public int FindLastIndex(Predicate<T> match)
        {
            if (match == null) throw new ArgumentNullException(nameof(match));
            _lock.EnterReadLock();
            try
            {
                CheckDisposed();
                return _innerList.FindLastIndex(match);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void RemoveRange(int index, int count)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnlyAndDisposed();
                ValidateRange(index, count);
                if (count == 0) return;

                var removedItems = _innerList.GetRange(index, count); // Get copy before removal
                _innerList.RemoveRange(index, count);

                RecordChange(ChangeType.RemoveRange, index, removedItems, null);
                OnItemsRemoved(removedItems);
                NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction.Remove, removedItems, index);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnlyAndDisposed();
                ValidateIndex(index, allowEnd: true);
                var itemsToInsert = collection.ToList(); // Materialize
                if (itemsToInsert.Count == 0) return;

                foreach (var item in itemsToInsert) ValidateItem(item);

                _innerList.InsertRange(index, itemsToInsert);
                RecordChange(ChangeType.AddRange, index, null, new List<T>(itemsToInsert)); // Use AddRange type, store copy
                OnItemsAdded(itemsToInsert);
                NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction.Add, itemsToInsert, index);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Move(int oldIndex, int newIndex)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnlyAndDisposed();
                int listCount = _innerList.Count;
                if (oldIndex < 0 || oldIndex >= listCount) throw new ArgumentOutOfRangeException(nameof(oldIndex));
                if (newIndex < 0 || newIndex >= listCount) throw new ArgumentOutOfRangeException(nameof(newIndex), "Target index for move must be within current list bounds."); // Standard ObservableCollection<T> requires newIndex to be within [0, Count-1]

                if (oldIndex == newIndex) return;

                T item = _innerList[oldIndex];
                _innerList.RemoveAt(oldIndex);
                _innerList.Insert(newIndex, item); // newIndex is the target index in the list *after* removal from oldIndex

                // For RecordChange, OldValue = original oldIndex, NewValue = item, Index = final newIndex
                RecordChange(ChangeType.Move, newIndex, oldIndex, item);
                OnItemsMoved(item, oldIndex, newIndex);
                NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction.Move, item, newIndex, oldIndex);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public T[] ToArray()
        {
            _lock.EnterReadLock();
            try
            {
                CheckDisposed();
                return _innerList.ToArray();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Gets a copy of the inner list. This is a snapshot and is thread-safe to iterate.
        /// </summary>
        public List<T> GetInnerListCopy()
        {
            _lock.EnterReadLock();
            try
            {
                CheckDisposed();
                return new List<T>(_innerList);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        #endregion

        #region Helper Methods

        private T GetItem(int index)
        {
            _lock.EnterReadLock();
            try
            {
                CheckDisposed();
                ValidateIndex(index);
                return _innerList[index];
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        private void SetItem(int index, T value) // This is for this[index] = value
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnlyAndDisposed();
                ValidateIndex(index);
                ValidateItem(value);

                var oldItem = _innerList[index];
                if (EqualityComparer<T>.Default.Equals(oldItem, value)) return; // No change

                _innerList[index] = value;

                RecordChange(ChangeType.Replace, index, oldItem, value);
                OnItemsModified(new List<(T OldItem, T NewItem)> { (oldItem, value) });
                OnItemReplaced(oldItem, value); // Specific event
                NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction.Replace, value, oldItem, index);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
        
        private void CheckDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(GetType().Name);
        }

        private void CheckReadOnlyAndDisposed()
        {
            CheckDisposed();
            if (_isReadOnly) throw new InvalidOperationException("Collection is read-only.");
        }

        private void ValidateIndex(int index, bool allowEnd = false)
        {
            // Assumes read lock is held or not needed if called from write-locked context
            int count = _innerList.Count;
            int max = allowEnd ? count : count - 1;
            if (count == 0 && allowEnd && index == 0) return; // Valid to insert at index 0 in empty list
            if (index < 0 || index > max)
                throw new ArgumentOutOfRangeException(nameof(index), $"Index must be within the range [0, {max}].");
        }

        private void ValidateRange(int index, int count)
        {
            // Assumes read lock is held or not needed
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > _innerList.Count) throw new ArgumentException("Range exceeds the bounds of the list.");
        }

        private void ValidateItem(T item)
        {
            // Assumes lock is held (typically write lock)
            var handler = ItemValidating;
            if (handler != null)
            {
                var args = new ItemValidationEventArgs<T>(item);
                handler(this, args);
                if (!args.IsValid)
                {
                    throw new ArgumentException(args.ErrorMessage ?? "Item validation failed.", nameof(item));
                }
            }
        }

        #endregion

        #region Event Raising Methods

        protected virtual void OnPropertyChanged(string propertyName)
        {
            // This method should be called when Count or IsReadOnly actually changes.
            // Item[] (indexer) changes are also typically signalled.
            // No _updateCount check here; PropertyChanged typically fires immediately.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Combined notification helper
        private void NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction action)
        {
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]"); // Indexer name for WPF binding
            NotifyCollectionChanged(action);
        }
        private void NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction action, object changedItem, int index)
        {
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            NotifyCollectionChanged(action, changedItem, index);
        }
        private void NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
        {
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            NotifyCollectionChanged(action, changedItems, startingIndex);
        }
        private void NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction action, object newItem, object oldItem, int index)
        {
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            NotifyCollectionChanged(action, newItem, oldItem, index);
        }
        private void NotifyCollectionAndPropertiesChanged(NotifyCollectionChangedAction action, object item, int newIndex, int oldIndex)
        {
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            NotifyCollectionChanged(action, item, newIndex, oldIndex);
        }


        // Original NotifyCollectionChanged methods (now private, called by combined helpers)
        private void NotifyCollectionChanged(NotifyCollectionChangedAction action)
        {
            if (_updateCount == 0) CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action));
            else _hasChanges = true;
        }
        private void NotifyCollectionChanged(NotifyCollectionChangedAction action, object changedItem, int index)
        {
            if (_updateCount == 0) CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, changedItem, index));
            else _hasChanges = true;
        }
        private void NotifyCollectionChanged(NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
        {
            if (_updateCount == 0) CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, changedItems, startingIndex));
            else _hasChanges = true;
        }
        private void NotifyCollectionChanged(NotifyCollectionChangedAction action, object newItem, object oldItem, int index)
        {
            if (_updateCount == 0) CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
            else _hasChanges = true;
        }
        private void NotifyCollectionChanged(NotifyCollectionChangedAction action, object item, int newIndex, int oldIndex)
        {
            if (_updateCount == 0) CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, item, newIndex, oldIndex));
            else _hasChanges = true;
        }

        // Custom event raising methods
        protected virtual void OnItemsAdded(IList<T> items)
        {
            if (_updateCount == 0) ItemsAdded?.Invoke(this, new ObservableListModified<T>(items));
            else _hasChanges = true;
        }
        protected virtual void OnItemsRemoved(IList<T> items)
        {
            if (_updateCount == 0) ItemsRemoved?.Invoke(this, new ObservableListModified<T>(items));
            else _hasChanges = true;
        }
        protected virtual void OnItemsModified(IList<(T OldItem, T NewItem)> changes)
        {
            if (_updateCount == 0) ItemsModified?.Invoke(this, new ObservableListModified<T>(changes));
            else _hasChanges = true;
        }
        protected virtual void OnItemsMoved(T item, int oldIndex, int newIndex)
        {
            if (_updateCount == 0) ItemsMoved?.Invoke(this, new ObservableListMoved<T>(item, oldIndex, newIndex));
            else _hasChanges = true;
        }
        protected virtual void OnItemInserted(int index, T item)
        {
            if (_updateCount == 0) ItemInserted?.Invoke(this, new ItemInsertedEventArgs<T>(index, item));
            else _hasChanges = true;
        }
        protected virtual void OnItemReplaced(T oldItem, T newItem)
        {
            if (_updateCount == 0) ItemReplaced?.Invoke(this, new ItemReplacedEventArgs<T>(oldItem, newItem));
            else _hasChanges = true;
        }
        protected virtual void OnCollectionReset()
        {
            if (_updateCount == 0) CollectionReset?.Invoke(this, EventArgs.Empty);
            else _hasChanges = true;
        }

        #endregion

        #region Interface Implementations

        public int IndexOf(T item)
        {
            _lock.EnterReadLock();
            try
            {
                CheckDisposed();
                return _innerList.IndexOf(item);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public bool Contains(T item)
        {
            _lock.EnterReadLock();
            try
            {
                CheckDisposed();
                return _innerList.Contains(item);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _lock.EnterReadLock();
            try
            {
                CheckDisposed();
                _innerList.CopyTo(array, arrayIndex);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a snapshot of the collection.
        /// This is thread-safe, as it iterates over a copy taken while holding a read lock.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            _lock.EnterReadLock();
            try
            {
                CheckDisposed();
                return new List<T>(_innerList).GetEnumerator(); // Snapshot
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            _lock.EnterReadLock(); // Ensure consistent state during serialization
            try
            {
                CheckDisposed();
                info.AddValue("Items", _innerList);
                info.AddValue("IsReadOnly", _isReadOnly);
                info.AddValue("MaxUndoSteps", MaxUndoSteps);
                // Note: _undoStack, _redoStack, and event subscribers are not serialized.
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        #endregion

        #region Dispose Pattern

        ~ObservableList()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // Acquire write lock to ensure no other operations are in progress
                // and to safely clear resources.
                bool lockAcquired = false;
                try
                {
                    // TryEnterWriteLock to avoid deadlocks if Dispose is called from a finalizer
                    // or in a situation where the lock might already be held contentiously.
                    // However, for a typical Dispose pattern, a blocking EnterWriteLock is common.
                    // If _lock itself is null (e.g. ctor failed), this would be an issue.
                    if (_lock != null)
                    {
                        _lock.EnterWriteLock();
                        lockAcquired = true;
                    }

                    // Clear event handlers to prevent memory leaks
                    ItemsAdded = null;
                    ItemsRemoved = null;
                    ItemsModified = null;
                    ItemsMoved = null;
                    ItemInserted = null;
                    ItemReplaced = null;
                    CollectionChanged = null;
                    PropertyChanged = null;
                    CollectionReset = null;
                    ItemValidating = null;

                    _undoStack?.Clear();
                    _redoStack?.Clear();
                    // _innerList?.Clear(); // Optional: whether to clear data on dispose. Current code preserves it.

                    _lock?.Dispose(); // Dispose the lock itself
                }
                finally
                {
                    if (lockAcquired)
                    {
                        _lock.ExitWriteLock();
                    }
                }
            }
            _disposed = true;
        }

        #endregion

        // Removed Getters for Fields region as most are covered by public properties or methods like GetInnerListCopy()
        // _updateCount and _hasChanges are internal implementation details.
    }

    #region Supporting Classes & Enums

    public class ObservableListModified<T> : EventArgs
    {
        public IReadOnlyList<T> Items { get; }
        public IReadOnlyList<(T OldItem, T NewItem)> Changes { get; }

        public ObservableListModified(IList<T> items)
        {
            Items = new System.Collections.ObjectModel.ReadOnlyCollection<T>(items ?? Array.Empty<T>());
            Changes = Array.Empty<(T, T)>();
        }

        public ObservableListModified(IList<(T OldItem, T NewItem)> changes)
        {
            Items = Array.Empty<T>();
            Changes = new System.Collections.ObjectModel.ReadOnlyCollection<(T OldItem, T NewItem)>(changes ?? Array.Empty<(T OldItem, T NewItem)>());
        }
    }

    public class ObservableListMoved<T> : EventArgs
    {
        public T Item { get; }
        public int OldIndex { get; }
        public int NewIndex { get; }
        public ObservableListMoved(T item, int oldIndex, int newIndex)
        {
            Item = item; OldIndex = oldIndex; NewIndex = newIndex;
        }
    }

    public class ItemInsertedEventArgs<T> : EventArgs
    {
        public int Index { get; }
        public T Item { get; }
        public ItemInsertedEventArgs(int index, T item) { Index = index; Item = item; }
    }

    public class ItemReplacedEventArgs<T> : EventArgs
    {
        public T OldItem { get; }
        public T NewItem { get; }
        public ItemReplacedEventArgs(T oldItem, T newItem) { OldItem = oldItem; NewItem = newItem; }
    }

    public class ItemValidationEventArgs<T> : EventArgs
    {
        public T Item { get; }
        public bool IsValid { get; set; } = true;
        public string ErrorMessage { get; set; }
        public ItemValidationEventArgs(T item) { Item = item; }
    }

    /// <summary>A thread-safe read-only wrapper for an <see cref="ObservableList{T}"/>.</summary>
    public class ReadOnlyObservableList<T> : IReadOnlyList<T>
    {
        private readonly ObservableList<T> _source;

        public ReadOnlyObservableList(ObservableList<T> source)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
        }

        // Corrected: Indexer should use the source's public indexer which handles locking
        public T this[int index] => _source[index];

        // Corrected: Count should use the source's public Count which handles locking
        public int Count => _source.Count;

        /// <summary>
        /// Returns an enumerator that iterates through a snapshot of the source collection.
        /// This is thread-safe.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            // _source.GetEnumerator() is already snapshot-based and thread-safe
            return _source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        // Removed misplaced endregion here
    }


    internal enum ChangeType
    {
        Add, Remove, Insert, Replace, Move,
        AddRange, RemoveRange, // Made RemoveRange distinct for clarity
        Clear
    }

    internal class Change
    {
        public ChangeType Type { get; }
        public int Index { get; } // For Add, Remove, Insert, Replace, Move: relevant index. For AddRange/RemoveRange: starting index. For Clear: 0.
        public object OldValue { get; } // For Remove, Replace: old item. For Move: old index. For RemoveRange, Clear: list of removed items.
        public object NewValue { get; } // For Add, Insert, Replace: new item. For Move: moved item. For AddRange: list of added items.

        public Change(ChangeType type, int index, object oldValue, object newValue)
        {
            Type = type; Index = index; OldValue = oldValue; NewValue = newValue;
        }
    }

    #endregion
}
