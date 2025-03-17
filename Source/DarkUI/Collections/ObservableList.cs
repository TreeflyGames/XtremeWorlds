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

        #endregion

        #region Events

        public event EventHandler<ObservableListModified<T>> ItemsAdded;
        public event EventHandler<ObservableListModified<T>> ItemsRemoved;
        public event EventHandler<ObservableListModified<T>> ItemsModified;
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
            ItemsAdded?.Invoke(this, new ObservableListModified<T>(items));
            OnCollectionChanged();
        }

        protected virtual void OnItemsRemoved(IList<T> items)
        {
            ItemsRemoved?.Invoke(this, new ObservableListModified<T>(items));
            OnCollectionChanged();
        }

        protected virtual void OnItemsModified(IList<(T OldItem, T NewItem)> changes)
        {
            ItemsModified?.Invoke(this, new ObservableListModified<T>(changes));
            OnCollectionChanged();
        }

        protected virtual void OnCollectionChanged()
        {
            CollectionChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Interface Implementations

        public int IndexOf(T item) => _innerList.IndexOf(item);
        public bool Contains(T item) => _innerList.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => _innerList.CopyTo(array, arrayIndex);
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
                    CollectionChanged = null;
                    _lock.Dispose();
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
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
}
