using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Represents an enhanced locking mechanism supporting synchronous and asynchronous waits,
    /// timeouts, cancellation, and RAII patterns via 'using'. Based on SemaphoreSlim(1, 1).
    /// This lock is NOT reentrant, unlike locks based on Monitor.
    /// </summary>
    public sealed class Lock : IDisposable
    {
        private readonly SemaphoreSlim _semaphore;
        private readonly string? _name; // Optional name for debugging/logging
        private bool _isDisposed;

        #region Properties

        /// <summary>
        /// Gets an optional name for the lock, useful for debugging and logging.
        /// </summary>
        public string? Name => _name;

        /// <summary>
        /// Gets a value indicating whether the lock is currently held.
        /// Returns true if the lock is held (semaphore count is 0), false otherwise.
        /// Note: This is indicative and subject to race conditions if checked outside the lock.
        /// </summary>
        public bool IsHeld => _semaphore.CurrentCount == 0;

        /// <summary>
        /// Gets the number of remaining threads that can enter the semaphore.
        /// For this lock (SemaphoreSlim(1, 1)), this will be 0 if held, 1 if free.
        /// </summary>
        public int CurrentCount => _semaphore.CurrentCount;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Lock"/> class.
        /// </summary>
        public Lock() : this(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Lock"/> class with an optional name.
        /// </summary>
        /// <param name="name">An optional name for the lock, used for diagnostics.</param>
        public Lock(string? name)
        {
            _semaphore = new SemaphoreSlim(1, 1); // Initial count 1, Max count 1
            _name = name;
            _isDisposed = false;
        }

        #endregion

        #region Core Synchronous Methods (Enter/Exit/TryEnter)

        /// <summary>
        /// Blocks the current thread until it can enter the lock.
        /// </summary>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public void Enter()
        {
            CheckDisposed();
            _semaphore.Wait();
        }

        /// <summary>
        /// Blocks the current thread until it can enter the lock, observing a <see cref="CancellationToken"/>.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe.</param>
        /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public void Enter(CancellationToken cancellationToken)
        {
            CheckDisposed();
            _semaphore.Wait(cancellationToken);
        }

        /// <summary>
        /// Attempts to enter the lock immediately without blocking.
        /// </summary>
        /// <returns>true if the lock was successfully entered; otherwise, false.</returns>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public bool TryEnter()
        {
            CheckDisposed();
            return _semaphore.Wait(0);
        }

        /// <summary>
        /// Attempts to enter the lock within the specified timeout.
        /// </summary>
        /// <param name="timeout">A <see cref="TimeSpan"/> that represents the maximum time to wait, or -1 milliseconds to wait indefinitely.</param>
        /// <returns>true if the lock was successfully entered; otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="timeout"/> is a negative number other than -1 milliseconds, which represents an infinite timeout -or- timeout is greater than <see cref="int.MaxValue"/> milliseconds.</exception>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public bool TryEnter(TimeSpan timeout)
        {
            CheckDisposed();
            return _semaphore.Wait(timeout);
        }

        /// <summary>
        /// Attempts to enter the lock within the specified timeout.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, <see cref="Timeout.Infinite"/> (-1) to wait indefinitely.</param>
        /// <returns>true if the lock was successfully entered; otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="millisecondsTimeout"/> is a negative number other than -1, which represents an infinite timeout.</exception>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public bool TryEnter(int millisecondsTimeout)
        {
            CheckDisposed();
            return _semaphore.Wait(millisecondsTimeout);
        }

        /// <summary>
        /// Attempts to enter the lock within the specified timeout, while observing a <see cref="CancellationToken"/>.
        /// </summary>
        /// <param name="timeout">A <see cref="TimeSpan"/> that represents the maximum time to wait, or -1 milliseconds to wait indefinitely.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>true if the lock was successfully entered; otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="timeout"/> is a negative number other than -1 milliseconds, which represents an infinite timeout -or- timeout is greater than <see cref="int.MaxValue"/> milliseconds.</exception>
        /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public bool TryEnter(TimeSpan timeout, CancellationToken cancellationToken)
        {
            CheckDisposed();
            return _semaphore.Wait(timeout, cancellationToken);
        }

        /// <summary>
        /// Attempts to enter the lock within the specified timeout, while observing a <see cref="CancellationToken"/>.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, <see cref="Timeout.Infinite"/> (-1) to wait indefinitely.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>true if the lock was successfully entered; otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="millisecondsTimeout"/> is a negative number other than -1, which represents an infinite timeout.</exception>
        /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public bool TryEnter(int millisecondsTimeout, CancellationToken cancellationToken)
        {
            CheckDisposed();
            return _semaphore.Wait(millisecondsTimeout, cancellationToken);
        }

        /// <summary>
        /// Releases the lock.
        /// </summary>
        /// <exception cref="SemaphoreFullException">The semaphore has already reached its maximum size.</exception>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public void Exit()
        {
            CheckDisposed();
            try
            {
                _semaphore.Release();
            }
            catch (SemaphoreFullException ex)
            {
                // This indicates a logic error: Exit called more times than Enter/TryEnter succeeded.
                throw new InvalidOperationException($"Lock '{_name ?? "Unnamed"}' released more times than acquired.", ex);
            }
        }

        #endregion

        #region Core Asynchronous Methods (EnterAsync/TryEnterAsync)

        /// <summary>
        /// Asynchronously waits to enter the lock.
        /// </summary>
        /// <returns>A task that will complete when the lock has been entered.</returns>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public Task EnterAsync()
        {
            CheckDisposed();
            return _semaphore.WaitAsync();
        }

        /// <summary>
        /// Asynchronously waits to enter the lock, while observing a <see cref="CancellationToken"/>.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>A task that will complete when the lock has been entered.</returns>
        /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public Task EnterAsync(CancellationToken cancellationToken)
        {
            CheckDisposed();
            return _semaphore.WaitAsync(cancellationToken);
        }

        /// <summary>
        /// Asynchronously attempts to enter the lock immediately without blocking.
        /// </summary>
        /// <returns>A task that will complete with a result of true if the lock was successfully entered; otherwise, false.</returns>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public Task<bool> TryEnterAsync()
        {
            CheckDisposed();
            return _semaphore.WaitAsync(0);
        }

        /// <summary>
        /// Asynchronously attempts to enter the lock within the specified timeout.
        /// </summary>
        /// <param name="timeout">A <see cref="TimeSpan"/> that represents the maximum time to wait, or -1 milliseconds to wait indefinitely.</param>
        /// <returns>A task that will complete with a result of true if the lock was successfully entered; otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="timeout"/> is a negative number other than -1 milliseconds, which represents an infinite timeout -or- timeout is greater than <see cref="int.MaxValue"/> milliseconds.</exception>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public Task<bool> TryEnterAsync(TimeSpan timeout)
        {
            CheckDisposed();
            return _semaphore.WaitAsync(timeout);
        }

        /// <summary>
        /// Asynchronously attempts to enter the lock within the specified timeout.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, <see cref="Timeout.Infinite"/> (-1) to wait indefinitely.</param>
        /// <returns>A task that will complete with a result of true if the lock was successfully entered; otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="millisecondsTimeout"/> is a negative number other than -1, which represents an infinite timeout.</exception>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public Task<bool> TryEnterAsync(int millisecondsTimeout)
        {
            CheckDisposed();
            return _semaphore.WaitAsync(millisecondsTimeout);
        }

        /// <summary>
        /// Asynchronously attempts to enter the lock within the specified timeout, while observing a <see cref="CancellationToken"/>.
        /// </summary>
        /// <param name="timeout">A <see cref="TimeSpan"/> that represents the maximum time to wait, or -1 milliseconds to wait indefinitely.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>A task that will complete with a result of true if the lock was successfully entered; otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="timeout"/> is a negative number other than -1 milliseconds, which represents an infinite timeout -or- timeout is greater than <see cref="int.MaxValue"/> milliseconds.</exception>
        /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public Task<bool> TryEnterAsync(TimeSpan timeout, CancellationToken cancellationToken)
        {
            CheckDisposed();
            return _semaphore.WaitAsync(timeout, cancellationToken);
        }

         /// <summary>
        /// Asynchronously attempts to enter the lock within the specified timeout, while observing a <see cref="CancellationToken"/>.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, <see cref="Timeout.Infinite"/> (-1) to wait indefinitely.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>A task that will complete with a result of true if the lock was successfully entered; otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="millisecondsTimeout"/> is a negative number other than -1, which represents an infinite timeout.</exception>
        /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public Task<bool> TryEnterAsync(int millisecondsTimeout, CancellationToken cancellationToken)
        {
            CheckDisposed();
            return _semaphore.WaitAsync(millisecondsTimeout, cancellationToken);
        }

        // Note: No direct async Exit method is needed. Release is synchronous.

        #endregion

        #region Execute Methods (Action/Func Wrappers)

        /// <summary>
        /// Acquires the lock, executes the specified action, and releases the lock.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public void Execute(Action action)
        {
            ArgumentNullException.ThrowIfNull(action);
            Enter(); // Or Enter(cancellationToken) if you add an overload
            try
            {
                action();
            }
            finally
            {
                Exit();
            }
        }

        /// <summary>
        /// Acquires the lock, executes the specified function, releases the lock, and returns the result.
        /// </summary>
        /// <typeparam name="T">The type of the return value.</typeparam>
        /// <param name="func">The function to execute.</param>
        /// <returns>The result of the function.</returns>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public T Execute<T>(Func<T> func)
        {
            ArgumentNullException.ThrowIfNull(func);
            Enter(); // Or Enter(cancellationToken) if you add an overload
            try
            {
                return func();
            }
            finally
            {
                Exit();
            }
        }

        /// <summary>
        /// Asynchronously acquires the lock, executes the specified asynchronous action, and releases the lock.
        /// </summary>
        /// <param name="asyncAction">The asynchronous action to execute.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe during lock acquisition.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled during lock acquisition.</exception>
        public async Task ExecuteAsync(Func<Task> asyncAction, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(asyncAction);
            await EnterAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                await asyncAction().ConfigureAwait(false);
            }
            finally
            {
                Exit();
            }
        }

        /// <summary>
        /// Asynchronously acquires the lock, executes the specified asynchronous function, releases the lock, and returns the result.
        /// </summary>
        /// <typeparam name="T">The type of the return value.</typeparam>
        /// <param name="asyncFunc">The asynchronous function to execute.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe during lock acquisition.</param>
        /// <returns>A task representing the asynchronous operation, with the function's result.</returns>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled during lock acquisition.</exception>
        public async Task<T> ExecuteAsync<T>(Func<Task<T>> asyncFunc, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(asyncFunc);
            await EnterAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                return await asyncFunc().ConfigureAwait(false);
            }
            finally
            {
                Exit();
            }
        }

        #endregion

        #region RAII Support ('using' pattern) - Acquire Methods

        /// <summary>
        /// Acquires the lock and returns a releaser struct that releases the lock when disposed.
        /// Recommended pattern: using (myLock.Acquire()) { ... }
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token for acquiring the lock.</param>
        /// <returns>A <see cref="LockReleaser"/> that must be disposed to release the lock.</returns>
        /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public LockReleaser Acquire(CancellationToken cancellationToken = default)
        {
            Enter(cancellationToken);
            // If Enter throws, LockReleaser is never created, Exit is never called. Correct.
            return new LockReleaser(this);
        }

        /// <summary>
        /// Attempts to acquire the lock within the specified timeout and returns a releaser struct
        /// if successful.
        /// Recommended pattern: using (var releaser = myLock.TryAcquire(timeout)) { if (releaser.HasValue) { ... } }
        /// </summary>
        /// <param name="timeout">The timeout duration.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A nullable <see cref="LockReleaser"/>. If HasValue is true, the lock was acquired and the value must be disposed. If false, the lock was not acquired.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Invalid timeout.</exception>
        /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public LockReleaser? TryAcquire(TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            if (TryEnter(timeout, cancellationToken))
            {
                return new LockReleaser(this);
            }
            return null;
        }

        /// <summary>
        /// Attempts to acquire the lock within the specified timeout and returns a releaser struct
        /// if successful.
        /// Recommended pattern: using (var releaser = myLock.TryAcquire(timeout)) { if (releaser.HasValue) { ... } }
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout in milliseconds.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A nullable <see cref="LockReleaser"/>. If HasValue is true, the lock was acquired and the value must be disposed. If false, the lock was not acquired.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Invalid timeout.</exception>
        /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public LockReleaser? TryAcquire(int millisecondsTimeout, CancellationToken cancellationToken = default)
        {
             if (TryEnter(millisecondsTimeout, cancellationToken))
             {
                 return new LockReleaser(this);
             }
             return null;
        }


        /// <summary>
        /// Asynchronously acquires the lock and returns an async releaser struct that releases the lock when disposed.
        /// Recommended pattern: await using (await myLock.AcquireAsync()) { ... }
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token for acquiring the lock.</param>
        /// <returns>A ValueTask containing an <see cref="AsyncLockReleaser"/> that must be disposed to release the lock.</returns>
        /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public async ValueTask<AsyncLockReleaser> AcquireAsync(CancellationToken cancellationToken = default)
        {
            await EnterAsync(cancellationToken).ConfigureAwait(false);
            // If EnterAsync throws, AsyncLockReleaser is never created, Exit is never called. Correct.
            return new AsyncLockReleaser(this);
        }

        /// <summary>
        /// Asynchronously attempts to acquire the lock within the specified timeout and returns an async releaser struct
        /// if successful.
        /// Recommended pattern: await using (var releaser = await myLock.TryAcquireAsync(timeout)) { if (releaser.HasValue) { ... } }
        /// </summary>
        /// <param name="timeout">The timeout duration.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A ValueTask containing a nullable <see cref="AsyncLockReleaser"/>. If HasValue is true, the lock was acquired and the value must be disposed. If false, the lock was not acquired.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Invalid timeout.</exception>
        /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public async ValueTask<AsyncLockReleaser?> TryAcquireAsync(TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            if (await TryEnterAsync(timeout, cancellationToken).ConfigureAwait(false))
            {
                return new AsyncLockReleaser(this);
            }
            return null;
        }

        /// <summary>
        /// Asynchronously attempts to acquire the lock within the specified timeout and returns an async releaser struct
        /// if successful.
        /// Recommended pattern: await using (var releaser = await myLock.TryAcquireAsync(timeout)) { if (releaser.HasValue) { ... } }
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout in milliseconds.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A ValueTask containing a nullable <see cref="AsyncLockReleaser"/>. If HasValue is true, the lock was acquired and the value must be disposed. If false, the lock was not acquired.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Invalid timeout.</exception>
        /// <exception cref="OperationCanceledException"><paramref name="cancellationToken"/> was canceled.</exception>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        public async ValueTask<AsyncLockReleaser?> TryAcquireAsync(int millisecondsTimeout, CancellationToken cancellationToken = default)
        {
             if (await TryEnterAsync(millisecondsTimeout, cancellationToken).ConfigureAwait(false))
             {
                 return new AsyncLockReleaser(this);
             }
             return null;
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Releases the resources used by the <see cref="Lock"/> instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects).
                    _semaphore.Dispose();
                }

                // Free unmanaged resources (unmanaged objects) and override finalizer
                // Set large fields to null

                _isDisposed = true;
            }
        }

        /// <summary>
        /// Checks if the object has been disposed and throws an exception if it has.
        /// </summary>
        /// <exception cref="ObjectDisposedException">The current instance has already been disposed.</exception>
        [DebuggerStepThrough]
        private void CheckDisposed()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName, $"Lock '{_name ?? "Unnamed"}' has been disposed.");
            }
        }

        #endregion

        #region Releaser Structs

        /// <summary>
        /// A disposable struct returned by Acquire() and TryAcquire() to release the lock via a 'using' statement.
        /// </summary>
        public readonly struct LockReleaser : IDisposable
        {
            private readonly Lock? _lockToRelease;

            /// <summary>
            /// Gets a value indicating whether this instance represents an acquired lock.
            /// </summary>
            public bool IsAcquired => _lockToRelease != null;

            internal LockReleaser(Lock lockToRelease)
            {
                _lockToRelease = lockToRelease;
            }

            /// <summary>
            /// Releases the acquired lock.
            /// </summary>
            public void Dispose()
            {
                // Null check allows using(var releaser = TryAcquire(...)) { if(releaser.IsAcquired){...}} pattern
                _lockToRelease?.Exit();
            }
        }

        /// <summary>
        /// An asynchronously disposable struct returned by AcquireAsync() and TryAcquireAsync()
        /// to release the lock via an 'await using' statement.
        /// </summary>
        public readonly struct AsyncLockReleaser : IAsyncDisposable
        {
            private readonly Lock? _lockToRelease;

             /// <summary>
            /// Gets a value indicating whether this instance represents an acquired lock.
            /// </summary>
            public bool IsAcquired => _lockToRelease != null;

            internal AsyncLockReleaser(Lock lockToRelease)
            {
                _lockToRelease = lockToRelease;
            }

            /// <summary>
            /// Releases the acquired lock.
            /// </summary>
            public ValueTask DisposeAsync()
            {
                 // Null check allows using(var releaser = await TryAcquireAsync(...)) { if(releaser.IsAcquired){...}} pattern
                _lockToRelease?.Exit();
                // Exit() is synchronous, so ValueTask.CompletedTask is appropriate.
                return ValueTask.CompletedTask;
            }
        }

        #endregion
    }
}
