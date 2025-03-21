using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Threading;

namespace DarkUI.Collections
{
    public class ObservableList<T> : IList<T>, IDisposable, IReadOnlyList<T>
    {
        #region Fields

        private readonly List<T> _innerList;
        private readonly ReaderWriterLockSlim _lock;
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
        public event EventHandler<EventArgs> CollectionChanged;

        #endregion

        #region Constructors

        public ObservableList()
        {
            _innerList = new List<T>();
            _lock = new ReaderWriterLockSlim();
            _isReadOnly = false;
        }

        public ObservableList(int capacity)
        {
            _innerList = new List<T>(capacity);
            _lock = new ReaderWriterLockSlim();
            _isReadOnly = false;
        }

        public ObservableList(IEnumerable<T> collection)
        {
            _innerList = new List<T>(collection);
            _lock = new ReaderWriterLockSlim();
            _isReadOnly = false;
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
                _innerList.Add(item);
                OnItemsAdded(new List<T> { item });
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
                _innerList.AddRange(items);
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
                OnItemsRemoved(removedItems);
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
                OnItemsAdded(new List<T> { item });
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

        public void Replace(T oldItem, T newItem)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                int index = _innerList.IndexOf(oldItem);
                if (index < 0) throw new ArgumentException("Item not found in collection");
                _innerList[index] = newItem;
                OnItemsModified(new List<(T OldItem, T NewItem)> { (oldItem, newItem) });
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
                if (comparer == null)
                    _innerList.Sort();
                else
                    _innerList.Sort(comparer);
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

        public void Sort(int index, int count, IComparer<T> comparer)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                _innerList.Sort(index, count, comparer);
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

        public void Reverse(int index, int count)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                _innerList.Reverse(index, count);
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

        public void RemoveRange(int index, int count)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                if (index < 0 || count < 0 || index + count > _innerList.Count)
                    throw new ArgumentOutOfRangeException();
                if (count == 0) return;
                var removedItems = _innerList.GetRange(index, count);
                _innerList.RemoveRange(index, count);
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

        public void ReplaceRange(int index, int count, IEnumerable<T> newItems)
        {
            _lock.EnterWriteLock();
            try
            {
                CheckReadOnly();
                if (index < 0 || count < 0 || index + count > _innerList.Count)
                    throw new ArgumentOutOfRangeException();
                var newList = newItems.ToList();
                if (count == newList.Count)
                {
                    var changes = new List<(T, T)>();
                    for (int i = 0; i < count; i++)
                    {
                        var oldItem = _innerList[index + i];
                        var newItem = newList[i];
                        _innerList[index + i] = newItem;
                        changes.Add((oldItem, newItem));
                    }
                    OnItemsModified(changes);
                }
                else
                {
                    var removedItems = _innerList.GetRange(index, count);
                    _innerList.RemoveRange(index, count);
                    _innerList.InsertRange(index, newList);
                    OnItemsRemoved(removedItems);
                    OnItemsAdded(newList);
                }
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

        public List<T> GetRange(int index, int count)
        {
            _lock.EnterReadLock();
            try
            {
                return _innerList.GetRange(index, count);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int BinarySearch(T item)
        {
            _lock.EnterReadLock();
            try
            {
                return _innerList.BinarySearch(item);
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
                OnItemsModified(new List<(T OldItem, T NewItem)> { (oldItem, value) });
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

        #endregion

        #region Event Raising Methods

        protected virtual void OnItemsAdded(IList<T> items)
        {
            if (_updateCount == 0)
            {
                ItemsAdded?.Invoke(this, new ObservableListModified<T>(items));
                OnCollectionChanged();
            }
            else
            {
                _hasChanges = true;
            }
        }

        protected virtual void OnItemsRemoved(IList<T> items)
        {
            if (_updateCount == 0)
            {
                ItemsRemoved?.Invoke(this, new ObservableListModified<T>(items));
                OnCollectionChanged();
            }
            else
            {
                _hasChanges = true;
            }
        }

        protected virtual void OnItemsModified(IList<(T OldItem, T NewItem)> changes)
        {
            if (_updateCount == 0)
            {
                ItemsModified?.Invoke(this, new ObservableListModified<T>(changes));
                OnCollectionChanged();
            }
            else
            {
                _hasChanges = true;
            }
        }

        protected virtual void OnItemsMoved(T item, int oldIndex, int newIndex)
        {
            if (_updateCount == 0)
            {
                ItemsMoved?.Invoke(this, new ObservableListMoved<T>(item, oldIndex, newIndex));
                OnCollectionChanged();
            }
            else
            {
                _hasChanges = true;
            }
        }

        protected virtual void OnCollectionChanged()
        {
            if (_updateCount == 0)
            {
                CollectionChanged?.Invoke(this, EventArgs.Empty);
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
                    ItemsAdded = null;
                    ItemsRemoved = null;
                    ItemsModified = null;
                    ItemsMoved = null;
                    CollectionChanged = null;
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
}
