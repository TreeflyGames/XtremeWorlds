using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;

namespace DarkUI.Collections
{
    [Serializable]
    public class ObservableList<T> : IList<T>, IDisposable, IReadOnlyList<T>, ISerializable
    {
        #region Fields

        private readonly List<T> _innerList;
        private readonly ReaderWriterLockSlim _lock;
        private readonly Stack<Change> _undoStack;
        private readonly Stack<Change> _redoStack;
        private bool _disposed;
        private bool _isReadOnly;
        private int _updateCount = 0;
        private bool _hasChanges = false;

        #endregion

        #region Events

        public event EventHandler<ObservableListModified<T>> ItemsAdded;
        public event EventHandler<ObservableListModified<T>> ItemsRemoved;
        public event EventHandler<ObservableListModified<T>> ItemsModified;
        public event EventHandler<ObservableListMoved<T>> ItemsMoved;
        public event EventHandler<ItemInsertedEventArgs<T>> ItemInserted;
        public event EventHandler<ItemReplacedEventArgs<T>> ItemReplaced;
        public event EventHandler<EventArgs> CollectionChanged;
        public event EventHandler<EventArgs> CollectionReset;

        #endregion

        #region Constructors

        public ObservableList()
        {
            _innerList = new List<T>();
            _lock = new ReaderWriterLockSlim();
            _undoStack = new Stack<Change>();
            _redoStack = new Stack<Change>();
            _isReadOnly = false;
        }

        public ObservableList(int capacity)
        {
            _innerList = new List<T>(capacity);
            _lock = new ReaderWriterLockSlim();
            _undoStack = new Stack<Change>();
            _redoStack = new Stack<Change>();
            _isReadOnly = false;
        }

        public ObservableList(IEnumerable<T> collection)
        {
            _innerList = new List<T>(collection);
            _lock = new ReaderWriterLockSlim();
            _undoStack = new Stack<Change>();
            _redoStack = new Stack<Change>();
            _isReadOnly = false;
        }

        // Serialization constructor
        protected ObservableList(SerializationInfo info, StreamingContext context)
        {
            _innerList = (List<T>)info.GetValue("Items", typeof(List<T>));
            _lock = new ReaderWriterLockSlim();
            _undoStack = new Stack<Change>();
            _redoStack = new Stack<Change>();
            _isReadOnly = info.GetBoolean("IsReadOnly");
        }

        #endregion

        #region Properties

        public int Count => _innerList.Count;
        public bool IsReadOnly => _isReadOnly;
        public T this[int index]
        {
            get => GetItem(index);
            set => SetItem(index, value);
        }

        #endregion

        #region Core Methods

        public void Add(T item)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                int index = _innerList.Count;
                _innerList.Add(item);
                RecordChange(ChangeType.Add, index, null, item);
                OnItemsAdded(new List<T> { item });
                OnItemInserted(index, item);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void AddRange(IEnumerable<T> collection)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                var items = collection.ToList();
                if (items.Count == 0) return;
                int startIndex = _innerList.Count;
                _innerList.AddRange(items);
                RecordChange(ChangeType.AddRange, startIndex, null, items);
                OnItemsAdded(items);
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
                CheckReadOnly();
                int index = _innerList.IndexOf(item);
                if (index < 0) return false;
                _innerList.RemoveAt(index);
                RecordChange(ChangeType.Remove, index, item, null);
                OnItemsRemoved(new List<T> { item });
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
                CheckReadOnly();
                ValidateIndex(index);
                var item = _innerList[index];
                _innerList.RemoveAt(index);
                RecordChange(ChangeType.Remove, index, item, null);
                OnItemsRemoved(new List<T> { item });
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
                CheckReadOnly();
                if (_innerList.Count == 0) return;
                var removedItems = new List<T>(_innerList);
                _innerList.Clear();
                RecordChange(ChangeType.Clear, 0, removedItems, null);
                OnItemsRemoved(removedItems);
                OnCollectionReset();
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
                CheckReadOnly();
                ValidateIndex(index, true);
                _innerList.Insert(index, item);
                RecordChange(ChangeType.Insert, index, null, item);
                OnItemsAdded(new List<T> { item });
                OnItemInserted(index, item);
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
                if (_updateCount > 0)
                {
                    _updateCount--;
                    if (_updateCount == 0 && _hasChanges)
                    {
                        OnCollectionChanged();
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

        public void Undo()
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                if (_undoStack.Count == 0) return;
                var change = _undoStack.Pop();
                ApplyChange(change, reverse: true);
                _redoStack.Push(change);
                OnCollectionChanged();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Redo()
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                if (_redoStack.Count == 0) return;
                var change = _redoStack.Pop();
                ApplyChange(change, reverse: false);
                _undoStack.Push(change);
                OnCollectionChanged();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        private void RecordChange(ChangeType type, int index, object oldValue, object newValue)
        {
            if (_updateCount > 0) return; // No history during batch updates
            _undoStack.Push(new Change(type, index, oldValue, newValue));
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

        #endregion

        #region Additional Features

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

        public IReadOnlyList<T> AsReadOnly() => _innerList.AsReadOnly();

        public void Replace(T oldItem, T newItem)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                int index = _innerList.IndexOf(oldItem);
                if (index < 0) throw new ArgumentException("Item not found in collection");
                _innerList[index] = newItem;
                RecordChange(ChangeType.Replace, index, null, (oldItem, newItem));
                OnItemsModified(new List<(T OldItem, T NewItem)> { (oldItem, newItem) });
                OnItemReplaced(oldItem, newItem);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Sort(IComparer<T> comparer = null)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                _innerList.Sort(comparer ?? Comparer<T>.Default);
                OnCollectionChanged();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Sort(Comparison<T> comparison)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                _innerList.Sort(comparison);
                OnCollectionChanged();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Reverse()
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                _innerList.Reverse();
                OnCollectionChanged();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

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
                RecordChange(ChangeType.AddRange, index, removedItems, null); // For undo
                OnItemsRemoved(removedItems);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                ValidateIndex(index, true);
                var items = collection.ToList();
                if (items.Count == 0) return;
                _innerList.InsertRange(index, items);
                RecordChange(ChangeType.AddRange, index, null, items);
                OnItemsAdded(items);
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
                CheckReadOnly();
                ValidateIndex(oldIndex);
                ValidateIndex(newIndex, true);
                if (oldIndex == newIndex) return;
                T item = _innerList[oldIndex];
                _innerList.RemoveAt(oldIndex);
                _innerList.Insert(newIndex, item);
                OnItemsMoved(item, oldIndex, newIndex);
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
                var oldItem = _innerList[index];
                _innerList[index] = value;
                RecordChange(ChangeType.Replace, index, null, (oldItem, value));
                OnItemsModified(new List<(T OldItem, T NewItem)> { (oldItem, value) });
                OnItemReplaced(oldItem, value);
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

        #endregion

        #region Event Raising Methods

        protected virtual void OnItemsAdded(IList<T> items)
        {
            if (_updateCount == 0 && ItemsAdded != null)
            {
                ItemsAdded(this, new ObservableListModified<T>(items));
                OnCollectionChanged();
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
                OnCollectionChanged();
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
                OnCollectionChanged();
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
                OnCollectionChanged();
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
                OnCollectionChanged();
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
                OnCollectionChanged();
            }
            else
            {
                _hasChanges = true;
            }
        }

        protected virtual void OnCollectionChanged()
        {
            if (_updateCount == 0 && CollectionChanged != null)
            {
                CollectionChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void OnCollectionReset()
        {
            if (_updateCount == 0 && CollectionReset != null)
            {
                CollectionReset(this, EventArgs.Empty);
                OnCollectionChanged();
            }
            else
            {
                _hasChanges = true;
            }
        }

        #endregion

        #region Interface Implementations

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

        public IEnumerator<T> GetEnumerator() => _innerList.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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
                _lock.EnterWriteLock();
                try
                {
                    _innerList.Clear();
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
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
                _lock.Dispose();
            }
            _disposed = true;
        }

        #endregion
    }

    #region Supporting Classes

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
