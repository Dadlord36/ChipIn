using System.Threading;
using Common;

namespace Controllers
{
    public class AsyncOperationCancellationController
    {
        private DisposableCancellationTokenSource _cancellationTokenSource = new DisposableCancellationTokenSource();
        public ref DisposableCancellationTokenSource TasksCancellationTokenSource => ref _cancellationTokenSource;
        public CancellationToken CancellationToken => _cancellationTokenSource.Token;


        public void CancelOngoingTask()
        {
            if (TasksCancellationTokenSource == null || TasksCancellationTokenSource.IsDisposed) return;
            TasksCancellationTokenSource.Cancel();
            DisposeTokenSource();
            _cancellationTokenSource = new DisposableCancellationTokenSource();
        }

        ~AsyncOperationCancellationController()
        {
            DisposeTokenSource();
        }

        private void DisposeTokenSource()
        {
            _cancellationTokenSource?.Dispose();
        }
    }
}