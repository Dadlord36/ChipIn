using System.Threading;

namespace Controllers
{
    public class AsyncOperationCancellationController
    {
        private CancellationTokenSource _cancellationTokenSource;
        public ref CancellationTokenSource TasksCancellationTokenSource => ref _cancellationTokenSource;

        public void CancelOngoingTask()
        {
            if (_cancellationTokenSource != null)
                TasksCancellationTokenSource.Cancel();
        }
    }
}