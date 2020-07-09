using System;

namespace Common
{
    public sealed class WhenAllAction
    {
        public event Action WhenAllActionsHappened;

        private uint _counter;

        public void IterateActionCounter()
        {
            if (WhenAllActionsHappened == null)
            {
                return;
            }

            _counter++;
            if (_counter == WhenAllActionsHappened.GetInvocationList().Length)
            {
                OnWhenAllActionsHappened();
            }
        }

        public void ResetCounter()
        {
            _counter = 0;
        }

        private void OnWhenAllActionsHappened()
        {
            WhenAllActionsHappened?.Invoke();
            ResetCounter();
        }
    }
}