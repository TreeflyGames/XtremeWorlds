using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;

namespace DarkUI.Collections
{
    /// <summary>
    /// A thread-safe observable list that supports undo/redo, batch updates, validation, and data-binding notifications.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    [Serializable]
    public class ObservableList<T> : IList<T>, IDisposable, IReadOnlyList<T>, ISerializable, INotifyCollectionChanged
    {
        #region Fields

        private readonly List<T> _innerList;
        private readonly ReaderWriterLockSlim _lock;
        private readonly List<Change> _undoStack; // Changed to List for MaxUndoSteps support
        private readonly List<Change> _redoStack;
        private bool _disposed;
        private bool _isReadOnly;
        private int _updateCount = 0;
        private bool _hasChanges = false;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the lock
        /// </summary>
        /// 
        public ReaderWriterLockSlim Lock => _lock;

        /// <summary>
        /// Gets the inner list.
        /// </summary>
        public List<T> InnerList => _innerList;

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
        /// </summary>
        public bool IsReadOnly => _isReadOnly;

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

        /// <summary>
        /// Occurs when items are added to the list.
        /// </summary>
        public event EventHandler<ObservableListModified<T>> ItemsAdded;

        /// <summary>
        /// Occurs when items are removed from the list.
        /// </summary>
        public event EventHandler<ObservableListModified<T>> ItemsRemoved;

        /// <summary>
        /// Occurs when items in the list are modified.
        /// </summary>
        public event EventHandler<ObservableListModified<T>> ItemsModified;

        /// <summary>
        /// Occurs when items are moved within the list.
        /// </summary>
        public event EventHandler<ObservableListMoved<T>> ItemsMoved;

        /// <summary>
        /// Occurs when an item is inserted into the list.
        /// </summary>
        public event EventHandler<ItemInsertedEventArgs<T>> ItemInserted;

        /// <summary>
        /// Occurs when an item is replaced in the list.
        /// </summary>
        public event EventHandler<ItemReplacedEventArgs<T>> ItemReplaced;

        /// <summary>
        /// Occurs when the collection changes, as defined by <see cref="INotifyCollectionChanged"/>.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Occurs when the collection is reset.
        /// </summary>
        public event EventHandler<EventArgs> CollectionReset;

        /// <summary>
        /// Occurs before an item is added or modified to allow validation.
        /// </summary>
        public event EventHandler<ItemValidationEventArgs<T>> ItemValidating;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableList{T}"/> class that is empty.
        /// </summary>
        public ObservableList()
        {
            _innerList = new List<T>();
            _lock = new ReaderWriterLockSlim();
            _undoStack = new List<Change>();
            _redoStack = new List<Change>();
            _isReadOnly = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableList{T}"/> class with the specified capacity.
        /// </summary>
        /// <param name="capacity">The initial capacity of the list.</param>
        public ObservableList(int capacity)
        {
            _innerList = new List<T>(capacity);
            _lock = new ReaderWriterLockSlim();
            _undoStack = new List<Change>();
            _redoStack = new List<Change>();
            _isReadOnly = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableList{T}"/> class with the specified collection.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        public ObservableList(IEnumerable<T> collection)
        {
            _innerList = new List<T>(collection);
            _lock = new ReaderWriterLockSlim();
            _undoStack = new List<Change>();
            _redoStack = new List<Change>();
            _isReadOnly = false;
        }

        // Serialization constructor
        protected ObservableList(SerializationInfo info, StreamingContext context)
        {
            _innerList = (List<T>)info.GetValue("Items", typeof(List<T>));
            _lock = new ReaderWriterLockSlim();
            _undoStack = new List<Change>();
            _redoStack = new List<Change>();
            _isReadOnly = info.GetBoolean("IsReadOnly");
        }

        #endregion

        #region Core Methods

        /// <summary>
        /// Adds an item to the end of the list.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(T item)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                ValidateItem(item);
                int index = _innerList.Count;
                _innerList.Add(item);
                RecordChange(ChangeType.Add, index, null, item);
                OnItemsAdded(new List<T> { item });
                OnItemInserted(index, item);
                NotifyCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Adds a range of items to the end of the list.
        /// </summary>
        /// <param name="collection">The collection of items to add.</param>
        public void AddRange(IEnumerable<T> collection)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                var items = collection.ToList();
                foreach (var item in items)
                {
                    ValidateItem(item);
                }
                if (items.Count == 0) return;
                int startIndex = _innerList.Count;
                _innerList.AddRange(items);
                RecordChange(ChangeType.AddRange, startIndex, null, items);
                OnItemsAdded(items);
                NotifyCollectionChanged(NotifyCollectionChangedAction.Add, items, startIndex);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific item from the list.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns><c>true</c> if the item was removed; otherwise, <c>false</c>.</returns>
        public bool Remove(T item)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                int index = _innerList.IndexOf(item);
                if (index < 0) return false;
                _innerList.RemoveAt(index);
                RecordChange(ChangeType.Remove, index, item, null);
                OnItemsRemoved(new List<T> { item });
                NotifyCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
                return true;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        public void RemoveAt(int index)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                ValidateIndex(index);
                var item = _innerList[index];
                _innerList.RemoveAt(index);
                RecordChange(ChangeType.Remove, index, item, null);
                OnItemsRemoved(new List<T> { item });
                NotifyCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Removes all items from the list.
        /// </summary>
        public void Clear()
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                if (_innerList.Count == 0) return;
                var removedItems = new List<T>(_innerList);
                _innerList.Clear();
                RecordChange(ChangeType.Clear, 0, removedItems, null);
                OnItemsRemoved(removedItems);
                OnCollectionReset();
                NotifyCollectionChanged(NotifyCollectionChangedAction.Reset);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Inserts an item into the list at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which to insert the item.</param>
        /// <param name="item">The item to insert.</param>
        public void Insert(int index, T item)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                ValidateIndex(index, true);
                ValidateItem(item);
                _innerList.Insert(index, item);
                RecordChange(ChangeType.Insert, index, null, item);
                OnItemsAdded(new List<T> { item });
                OnItemInserted(index, item);
                NotifyCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        #endregion

        #region Batch Update Methods

        /// <summary>
        /// Begins a batch update, suppressing individual event notifications until <see cref="EndUpdate"/> is called.
        /// </summary>
        public void BeginUpdate()
        {
            _lock.EnterWriteLock();
            try
            {
                _updateCount++;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Ends a batch update, raising a reset event if changes occurred during the update.
        /// </summary>
        public void EndUpdate()
        {
            _lock.EnterWriteLock();
            try
            {
                if (_updateCount > 0)
                {
                    _updateCount--;
                    if (_updateCount == 0 && _hasChanges)
                    {
                        OnCollectionReset();
                        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
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

        /// <summary>
        /// Undoes the last change made to the list.
        /// </summary>
        public void Undo()
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                if (_undoStack.Count == 0) return;
                var change = _undoStack[_undoStack.Count - 1];
                _undoStack.RemoveAt(_undoStack.Count - 1);
                ApplyChange(change, reverse: true);
                _redoStack.Add(change);
                NotifyCollectionChangedForChange(change, reverse: true);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Redoes the last undone change.
        /// </summary>
        public void Redo()
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                if (_redoStack.Count == 0) return;
                var change = _redoStack[_redoStack.Count - 1];
                _redoStack.RemoveAt(_redoStack.Count - 1);
                ApplyChange(change, reverse: false);
                _undoStack.Add(change);
                NotifyCollectionChangedForChange(change, reverse: false);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        private void RecordChange(ChangeType type, int index, object oldValue, object newValue)
        {
            if (_updateCount > 0)
            {
                _hasChanges = true;
                return;
            }
            var change = new Change(type, index, oldValue, newValue);
            _undoStack.Add(change);
            if (_undoStack.Count > MaxUndoSteps)
            {
                _undoStack.RemoveAt(0);
            }
            _redoStack.Clear();
        }

        private void ApplyChange(Change change, bool reverse)
        {
            switch (change.Type)
            {
                case ChangeType.Add:
                    if (reverse) _innerList.RemoveAt(change.Index);
                    else _innerList.Insert(change.Index, (T)change.NewValue);
                    break;
                case ChangeType.Remove:
                    if (reverse) _innerList.Insert(change.Index, (T)change.OldValue);
                    else _innerList.RemoveAt(change.Index);
                    break;
                case ChangeType.Insert:
                    if (reverse) _innerList.RemoveAt(change.Index);
                    else _innerList.Insert(change.Index, (T)change.NewValue);
                    break;
                case ChangeType.Replace:
                    var (oldItem, newItem) = ((T Old, T New))change.NewValue;
                    _innerList[change.Index] = reverse ? oldItem : newItem;
                    break;
                case ChangeType.AddRange:
                    var items = (IList<T>)change.NewValue;
                    if (reverse) _innerList.RemoveRange(change.Index, items.Count);
                    else _innerList.InsertRange(change.Index, items);
                    break;
                case ChangeType.Clear:
                    var oldItems = (IList<T>)change.OldValue;
                    if (reverse) _innerList.AddRange(oldItems);
                    else _innerList.Clear();
                    break;
            }
        }

        private void NotifyCollectionChangedForChange(Change change, bool reverse)
        {
            if (_updateCount > 0)
            {
                _hasChanges = true;
                return;
            }
            switch (change.Type)
            {
                case ChangeType.Add:
                    NotifyCollectionChanged(reverse ? NotifyCollectionChangedAction.Remove : NotifyCollectionChangedAction.Add, (T)(reverse ? change.NewValue : change.NewValue), change.Index);
                    break;
                case ChangeType.Remove:
                    NotifyCollectionChanged(reverse ? NotifyCollectionChangedAction.Add : NotifyCollectionChangedAction.Remove, (T)(reverse ? change.OldValue : change.OldValue), change.Index);
                    break;
                case ChangeType.Insert:
                    NotifyCollectionChanged(reverse ? NotifyCollectionChangedAction.Remove : NotifyCollectionChangedAction.Add, (T)change.NewValue, change.Index);
                    break;
                case ChangeType.Replace:
                    var (oldItem, newItem) = ((T Old, T New))change.NewValue;
                    NotifyCollectionChanged(NotifyCollectionChangedAction.Replace, reverse ? oldItem : newItem, reverse ? newItem : oldItem, change.Index);
                    break;
                case ChangeType.AddRange:
                    var items = (IList<T>)change.NewValue;
                    NotifyCollectionChanged(reverse ? NotifyCollectionChangedAction.Remove : NotifyCollectionChangedAction.Add, items, change.Index);
                    break;
                case ChangeType.Clear:
                    NotifyCollectionChanged(NotifyCollectionChangedAction.Reset);
                    break;
            }
        }

        #endregion

        #region Additional Features

        /// <summary>
        /// Sets whether the list is read-only.
        /// </summary>
        /// <param name="readOnly">If <c>true</c>, the list becomes read-only; otherwise, it becomes modifiable.</param>
        public void SetReadOnly(bool readOnly)
        {
            _lock.EnterWriteLock();
            try
            {
                _isReadOnly = readOnly;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Returns a non-thread-safe read-only view of the list.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyList{T}"/> representing the list.</returns>
        public IReadOnlyList<T> AsReadOnly() => _innerList.AsReadOnly();

        /// <summary>
        /// Returns a thread-safe read-only view of the list.
        /// </summary>
        /// <returns>An <see cref="IReadOnlyList{T}"/> that is safe to access concurrently.</returns>
        public IReadOnlyList<T> AsThreadSafeReadOnly() => new ReadOnlyObservableList<T>(this);

        /// <summary>
        /// Replaces an existing item with a new item.
        /// </summary>
        /// <param name="oldItem">The item to replace.</param>
        /// <param name="newItem">The new item to insert.</param>
        public void Replace(T oldItem, T newItem)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                ValidateItem(newItem);
                int index = _innerList.IndexOf(oldItem);
                if (index < 0) throw new ArgumentException("Item not found in collection", nameof(oldItem));
                _innerList[index] = newItem;
                RecordChange(ChangeType.Replace, index, null, (oldItem, newItem));
                OnItemsModified(new List<(T OldItem, T NewItem)> { (oldItem, newItem) });
                OnItemReplaced(oldItem, newItem);
                NotifyCollectionChanged(NotifyCollectionChangedAction.Replace, newItem, oldItem, index);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Sorts the list using the specified comparer.
        /// </summary>
        /// <param name="comparer">The comparer to use, or <c>null</c> to use the default comparer.</param>
        public void Sort(IComparer<T> comparer = null)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                _innerList.Sort(comparer ?? Comparer<T>.Default);
                NotifyCollectionChanged(NotifyCollectionChangedAction.Reset);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Sorts the list using the specified comparison.
        /// </summary>
        /// <param name="comparison">The comparison to use.</param>
        public void Sort(Comparison<T> comparison)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                _innerList.Sort(comparison);
                NotifyCollectionChanged(NotifyCollectionChangedAction.Reset);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Reverses the order of the items in the list.
        /// </summary>
        public void Reverse()
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                _innerList.Reverse();
                NotifyCollectionChanged(NotifyCollectionChangedAction.Reset);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Finds all items that match the specified predicate.
        /// </summary>
        /// <param name="match">The predicate to match items against.</param>
        /// <returns>A read-only list of matching items.</returns>
        public IEnumerable<T> FindAll(Predicate<T> match)
        {
            _lock.EnterReadLock();
            try
            {
                return _innerList.FindAll(match).AsReadOnly();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Finds the first item that matches the specified predicate.
        /// </summary>
        /// <param name="match">The predicate to match items against.</param>
        /// <returns>The first matching item, or the default value if none found.</returns>
        public T Find(Predicate<T> match)
        {
            _lock.EnterReadLock();
            try
            {
                return _innerList.Find(match);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Finds the last item that matches the specified predicate.
        /// </summary>
        /// <param name="match">The predicate to match items against.</param>
        /// <returns>The last matching item, or the default value if none found.</returns>
        public T FindLast(Predicate<T> match)
        {
            _lock.EnterReadLock();
            try
            {
                return _innerList.FindLast(match);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Finds the index of the first item that matches the specified predicate.
        /// </summary>
        /// <param name="match">The predicate to match items against.</param>
        /// <returns>The index of the first matching item, or -1 if none found.</returns>
        public int FindIndex(Predicate<T> match)
        {
            _lock.EnterReadLock();
            try
            {
                return _innerList.FindIndex(match);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Finds the index of the last item that matches the specified predicate.
        /// </summary>
        /// <param name="match">The predicate to match items against.</param>
        /// <returns>The index of the last matching item, or -1 if none found.</returns>
        public int FindLastIndex(Predicate<T> match)
        {
            _lock.EnterReadLock();
            try
            {
                return _innerList.FindLastIndex(match);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Removes a range of items from the list.
        /// </summary>
        /// <param name="index">The starting index of the range to remove.</param>
        /// <param name="count">The number of items to remove.</param>
        public void RemoveRange(int index, int count)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                ValidateRange(index, count);
                if (count == 0) return;
                var removedItems = _innerList.GetRange(index, count);
                _innerList.RemoveRange(index, count);
                RecordChange(ChangeType.AddRange, index, removedItems, null);
                OnItemsRemoved(removedItems);
                NotifyCollectionChanged(NotifyCollectionChangedAction.Remove, removedItems, index);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Inserts a range of items into the list at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which to insert the items.</param>
        /// <param name="collection">The collection of items to insert.</param>
        public void InsertRange(int index, IEnumerable<T> collection)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                ValidateIndex(index, true);
                var items = collection.ToList();
                foreach (var item in items)
                {
                    ValidateItem(item);
                }
                if (items.Count == 0) return;
                _innerList.InsertRange(index, items);
                RecordChange(ChangeType.AddRange, index, null, items);
                OnItemsAdded(items);
                NotifyCollectionChanged(NotifyCollectionChangedAction.Add, items, index);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Moves an item from one index to another within the list.
        /// </summary>
        /// <param name="oldIndex">The current index of the item to move.</param>
        /// <param name="newIndex">The new index for the item.</param>
        public void Move(int oldIndex, int newIndex)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                ValidateIndex(oldIndex);
                ValidateIndex(newIndex, true);
                if (oldIndex == newIndex) return;
                T item = _innerList[oldIndex];
                _innerList.RemoveAt(oldIndex);
                _innerList.Insert(newIndex, item);
                OnItemsMoved(item, oldIndex, newIndex);
                NotifyCollectionChanged(NotifyCollectionChangedAction.Move, item, newIndex, oldIndex);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Copies the elements of the list to an array.
        /// </summary>
        /// <returns>An array containing the list's elements.</returns>
        public T[] ToArray()
        {
            _lock.EnterReadLock();
            try
            {
                return _innerList.ToArray();
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
                ValidateIndex(index);
                return _innerList[index];
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        private void SetItem(int index, T value)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                ValidateIndex(index);
                ValidateItem(value);
                var oldItem = _innerList[index];
                _innerList[index] = value;
                RecordChange(ChangeType.Replace, index, null, (oldItem, value));
                OnItemsModified(new List<(T OldItem, T NewItem)> { (oldItem, value) });
                OnItemReplaced(oldItem, value);
                NotifyCollectionChanged(NotifyCollectionChangedAction.Replace, value, oldItem, index);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        private void CheckReadOnly()
        {
            if (_isReadOnly)
                throw new InvalidOperationException("Collection is read-only");
        }

        private void ValidateIndex(int index, bool allowEnd = false)
        {
            int max = allowEnd ? Count : Count - 1;
            if (index < 0 || index > max)
                throw new ArgumentOutOfRangeException(nameof(index));
        }

        private void ValidateRange(int index, int count)
        {
            if (index < 0 || count < 0 || index + count > Count)
                throw new ArgumentOutOfRangeException();
        }

        private void ValidateItem(T item)
        {
            if (ItemValidating != null)
            {
                var args = new ItemValidationEventArgs<T>(item);
                ItemValidating(this, args);
                if (!args.IsValid)
                {
                    throw new ArgumentException(args.ErrorMessage ?? "Item validation failed", nameof(item));
                }
            }
        }

        #endregion

        #region Event Raising Methods

        protected virtual void OnItemsAdded(IList<T> items)
        {
            if (_updateCount == 0 && ItemsAdded != null)
            {
                ItemsAdded(this, new ObservableListModified<T>(items));
            }
            else
            {
                _hasChanges = true;
            }
        }

        protected virtual void OnItemsRemoved(IList<T> items)
        {
            if (_updateCount == 0 && ItemsRemoved != null)
            {
                ItemsRemoved(this, new ObservableListModified<T>(items));
            }
            else
            {
                _hasChanges = true;
            }
        }

        protected virtual void OnItemsModified(IList<(T OldItem, T NewItem)> changes)
        {
            if (_updateCount == 0 && ItemsModified != null)
            {
                ItemsModified(this, new ObservableListModified<T>(changes));
            }
            else
            {
                _hasChanges = true;
            }
        }

        protected virtual void OnItemsMoved(T item, int oldIndex, int newIndex)
        {
            if (_updateCount == 0 && ItemsMoved != null)
            {
                ItemsMoved(this, new ObservableListMoved<T>(item, oldIndex, newIndex));
            }
            else
            {
                _hasChanges = true;
            }
        }

        protected virtual void OnItemInserted(int index, T item)
        {
            if (_updateCount == 0 && ItemInserted != null)
            {
                ItemInserted(this, new ItemInsertedEventArgs<T>(index, item));
            }
            else
            {
                _hasChanges = true;
            }
        }

        protected virtual void OnItemReplaced(T oldItem, T newItem)
        {
            if (_updateCount == 0 && ItemReplaced != null)
            {
                ItemReplaced(this, new ItemReplacedEventArgs<T>(oldItem, newItem));
            }
            else
            {
                _hasChanges = true;
            }
        }

        protected virtual void OnCollectionReset()
        {
            if (_updateCount == 0 && CollectionReset != null)
            {
                CollectionReset(this, EventArgs.Empty);
            }
            else
            {
                _hasChanges = true;
            }
        }

        private void NotifyCollectionChanged(NotifyCollectionChangedAction action)
        {
            if (_updateCount == 0 && CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(action));
            }
            else
            {
                _hasChanges = true;
            }
        }

        private void NotifyCollectionChanged(NotifyCollectionChangedAction action, T changedItem, int index)
        {
            if (_updateCount == 0 && CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, changedItem, index));
            }
            else
            {
                _hasChanges = true;
            }
        }

        private void NotifyCollectionChanged(NotifyCollectionChangedAction action, IList<T> changedItems, int startingIndex)
        {
            if (_updateCount == 0 && CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, changedItems, startingIndex));
            }
            else
            {
                _hasChanges = true;
            }
        }

        private void NotifyCollectionChanged(NotifyCollectionChangedAction action, T newItem, T oldItem, int index)
        {
            if (_updateCount == 0 && CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
            }
            else
            {
                _hasChanges = true;
            }
        }

        private void NotifyCollectionChanged(NotifyCollectionChangedAction action, T item, int newIndex, int oldIndex)
        {
            if (_updateCount == 0 && CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, item, newIndex, oldIndex));
            }
            else
            {
                _hasChanges = true;
            }
        }

        #endregion

        #region Interface Implementations

        /// <summary>
        /// Determines the index of a specific item in the list.
        /// </summary>
        /// <param name="item">The item to locate.</param>
        /// <returns>The index of the item if found; otherwise, -1.</returns>
        public int IndexOf(T item)
        {
            _lock.EnterReadLock();
            try
            {
                return _innerList.IndexOf(item);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Determines whether the list contains a specific item.
        /// </summary>
        /// <param name="item">The item to locate.</param>
        /// <returns><c>true</c> if the item is found; otherwise, <c>false</c>.</returns>
        public bool Contains(T item)
        {
            _lock.EnterReadLock();
            try
            {
                return _innerList.Contains(item);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Copies the elements of the list to an array, starting at a particular array index.
        /// </summary>
        /// <param name="array">The destination array.</param>
        /// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _lock.EnterReadLock();
            try
            {
                _innerList.CopyTo(array, arrayIndex);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the list.
        /// </summary>
        /// <returns>An enumerator for the list.</returns>
        public IEnumerator<T> GetEnumerator() => _innerList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Populates a <see cref="SerializationInfo"/> with the data needed to serialize the list.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> to populate.</param>
        /// <param name="context">The destination for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _lock.EnterReadLock();
            try
            {
                info.AddValue("Items", _innerList);
                info.AddValue("IsReadOnly", _isReadOnly);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        #endregion

        #region Dispose Pattern

        /// <summary>
        /// Finalizer for the <see cref="ObservableList{T}"/> class.
        /// </summary>
        ~ObservableList()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="ObservableList{T}"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="ObservableList{T}"/> and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _lock.EnterWriteLock();
                try
                {
                    // Do not clear _innerList to preserve data
                    _undoStack.Clear();
                    _redoStack.Clear();
                    ItemsAdded = null;
                    ItemsRemoved = null;
                    ItemsModified = null;
                    ItemsMoved = null;
                    ItemInserted = null;
                    ItemReplaced = null;
                    CollectionChanged = null;
                    CollectionReset = null;
                    ItemValidating = null;
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
                _lock.Dispose();
            }
            _disposed = true;
        }

        #region Getters for Fields

        /// <summary>
        /// Gets the inner list of the observable list.
        /// </summary>
        public List<T> GetInnerList()
        {
            _lock.EnterReadLock();
            try
            {
                return new List<T>(_innerList); // Return a copy to ensure thread safety
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Gets whether the list is disposed.
        /// </summary>
        public bool GetIsDisposed()
        {
            _lock.EnterReadLock();
            try
            {
                return _disposed;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Gets whether the list is read-only.
        /// </summary>
        public bool GetIsReadOnly()
        {
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

        /// <summary>
        /// Gets the current update count.
        /// </summary>
        public int GetUpdateCount()
        {
            _lock.EnterReadLock();
            try
            {
                return _updateCount;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Gets whether the list has changes.
        /// </summary>
        public bool GetHasChanges()
        {
            _lock.EnterReadLock();
            try
            {
                return _hasChanges;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        #endregion
    }

    #region Supporting Classes

    /// <summary>
    /// Event arguments for modifications to an <see cref="ObservableList{T}"/>.
    /// </summary>
    public class ObservableListModified<T> : EventArgs
    {
        public IReadOnlyList<T> Items { get; }
        public IReadOnlyList<(T OldItem, T NewItem)> Changes { get; }

        public ObservableListModified(IList<T> items)
        {
            Items = items.AsReadOnly();
            Changes = Array.Empty<(T, T)>();
        }

        public ObservableListModified(IList<(T OldItem, T NewItem)> changes)
        {
            Items = Array.Empty<T>();
            Changes = changes.AsReadOnly();
        }
    }

    /// <summary>
    /// Event arguments for when an item is moved within an <see cref="ObservableList{T}"/>.
    /// </summary>
    public class ObservableListMoved<T> : EventArgs
    {
        public T Item { get; }
        public int OldIndex { get; }
        public int NewIndex { get; }

        public ObservableListMoved(T item, int oldIndex, int newIndex)
        {
            Item = item;
            OldIndex = oldIndex;
            NewIndex = newIndex;
        }
    }

    /// <summary>
    /// Event arguments for when an item is inserted into an <see cref="ObservableList{T}"/>.
    /// </summary>
    public class ItemInsertedEventArgs<T> : EventArgs
    {
        public int Index { get; }
        public T Item { get; }
        public ItemInsertedEventArgs(int index, T item)
        {
            Index = index;
            Item = item;
        }
    }

    /// <summary>
    /// Event arguments for when an item is replaced in an <see cref="ObservableList{T}"/>.
    /// </summary>
    public class ItemReplacedEventArgs<T> : EventArgs
    {
        public T OldItem { get; }
        public T NewItem { get; }
        public ItemReplacedEventArgs(T oldItem, T newItem)
        {
            OldItem = oldItem;
            NewItem = newItem;
        }
    }

    /// <summary>
    /// Event arguments for validating an item before it is added or modified in an <see cref="ObservableList{T}"/>.
    /// </summary>
    public class ItemValidationEventArgs<T> : EventArgs
    {
        public T Item { get; }
        public bool IsValid { get; set; } = true;
        public string ErrorMessage { get; set; }

        public ItemValidationEventArgs(T item)
        {
            Item = item;
        }
    }

    /// <summary>
    /// A thread-safe read-only wrapper around an <see cref="ObservableList{T}"/>.
    /// </summary>
    public class ReadOnlyObservableList<T> : IReadOnlyList<T>
    {
        private readonly ObservableList<T> _parent;

        public ReadOnlyObservableList(ObservableList<T> parent)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        public T this[int index]
        {
            get
            {
                _parent.Lock.EnterReadLock();
                try
                {
                    return _parent.InnerList[index];
                }
                finally
                {
                    _parent.Lock.ExitReadLock();
                }
            }
            #endregion // Dispose Pattern
        }

        public int Count
        {
            get
            {
                _parent.Lock.EnterReadLock();
                try
                {
                    return _parent.InnerList.Count;
                }
                finally
                {
                    _parent.Lock.ExitReadLock();
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            _parent.Lock.EnterReadLock();
            try
            {
                return _parent.InnerList.ToList().GetEnumerator(); // Snapshot to avoid locking during enumeration
            }
            finally
            {
                _parent.Lock.ExitReadLock();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal enum ChangeType
    {
        Add,
        Remove,
        Insert,
        Replace,
        AddRange,
        Clear
    }

    internal class Change
    {
        public ChangeType Type { get; }
        public int Index { get; }
        public object OldValue { get; }
        public object NewValue { get; }

        public Change(ChangeType type, int index, object oldValue, object newValue)
        {
            Type = type;
            Index = index;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    #endregion
}
