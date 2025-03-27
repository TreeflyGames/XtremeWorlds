using System.Threading;
using System;

namespace Core
{
    public class Lock
    {
        public static readonly object Instance = new object();
        private readonly object _lock = Instance;

        public void Enter()
        {
            Monitor.Enter(_lock);
        }

        public void Exit()
        {
            Monitor.Exit(_lock);
        }

        public void Execute(Action action)
        {
            Enter();
            try
            {
                action();
            }
            finally
            {
                Exit();
            }
        }

        public T Execute<T>(Func<T> func)
        {
            Enter();
            try
            {
                return func();
            }
            finally
            {
                Exit();
            }
        }
    }
}
