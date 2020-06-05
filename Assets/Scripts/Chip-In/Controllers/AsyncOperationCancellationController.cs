using Common;

namespace Controllers
{
    public class AsyncOperationCancellationController
    {
        private DisposableCancellationTokenSource _cancellationTokenSource;
        public ref DisposableCancellationTokenSource TasksCancellationTokenSource => ref _cancellationTokenSource;


        public void CancelOngoingTask()
        {
            if (TasksCancellationTokenSource == null || TasksCancellationTokenSource.IsDisposed) return;
            TasksCancellationTokenSource.Token.Register(DisposeTokenSource);
            TasksCancellationTokenSource.Cancel();
        }

        ~AsyncOperationCancellationController()
        {
            DisposeTokenSource();
        }

        private void DisposeTokenSource()
        {
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
    }
}