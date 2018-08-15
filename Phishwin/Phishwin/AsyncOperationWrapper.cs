using System;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Phishwin
{
    public class AsyncOperationWrapper<T> : IDisposable
    {
        public event EventHandler<AsyncOperationEventArg> CompletedEvent;
        public class AsyncOperationEventArg : EventArgs
        {
            public AsyncOperationEventArg(IAsyncOperation<T> asyncOperation)
            {
                this.asyncOperation = asyncOperation;
            }
            private IAsyncOperation<T> asyncOperation;
            public IAsyncOperation<T> AsyncOperation { get { return this.asyncOperation; } }
        }

        private IAsyncOperation<T> _asyncOperation;
        private AsyncOperationEventArg _result = null;
        private bool _ready = false;

        public void Start()
        {
            SetResult(null, true);
        }

        private void SetResult(AsyncOperationEventArg typedEventArg, bool ready)
        {
            if (this._result == null)
            {
                this._result = typedEventArg;
            }
            if (!this._ready)
            {
                this._ready = ready;
            }

            if (this._ready && this._result != null)
            {
                this.CompletedEvent?.Invoke(this, this._result);
            }
        }

        public AsyncOperationWrapper(object asyncOperation)
        {
            if (asyncOperation == null) throw new ArgumentNullException("asyncOperation");

            _asyncOperation = (IAsyncOperation<T>)asyncOperation;
        }

        ~AsyncOperationWrapper()
        {
            GC.SuppressFinalize(this);
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (_asyncOperation == null) return;

            if (disposing)
            {
                _asyncOperation.Close();
                _asyncOperation = null;
            }
        }

        public AsyncStatus Status
        {
            get { return _asyncOperation.Status; }
        }

       /* public object AwaitResult()
        {
            return AwaitResult(-1);
        }*/

        /*public object AwaitResult(int millisecondsTimeout)
        {
            
            var task = _asyncOperation.AsTask();
            task.Wait(millisecondsTimeout);

            if (task.IsCompleted)
            {
                return task.Result;
            }
            else if (task.IsFaulted)
            {
                throw task.Exception;
            }
            else
            {
                throw new TaskCanceledException(task);
            }
        }*/
    }
}
