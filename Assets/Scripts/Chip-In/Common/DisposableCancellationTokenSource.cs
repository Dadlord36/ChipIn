using System.Threading;

namespace Common
{
    public class DisposableCancellationTokenSource : CancellationTokenSource
    {
        public bool IsDisposed { get; private set; }
        protected override void Dispose(bool disposing)
        {
            IsDisposed = true;
            base.Dispose(disposing);
        }
    }
}