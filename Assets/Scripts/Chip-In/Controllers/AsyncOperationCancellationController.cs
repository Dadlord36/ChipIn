using System.Threading.Tasks;
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
            TasksCancellationTokenSource.Cancel();
            _cancellationTokenSource = null;
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