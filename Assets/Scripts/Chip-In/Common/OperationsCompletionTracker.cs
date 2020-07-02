using System;

namespace Common
{
    public sealed class OperationsCompletionTracker
    {
        public event Action WhenAllIsDone;

        private uint _counter;

        public void ResetCounter()
        {
            _counter = 0;
        }

        public void AddToCounter()
        {
            _counter++;
        }

        public void ConfirmActionCompletion()
        {
            _counter--;
            CheckIfIsDone();
        }

        private void CheckIfIsDone()
        {
            if (_counter == 0)
            {
                OnWhenAllIsDone();
            }
        }

        private void OnWhenAllIsDone()
        {
            WhenAllIsDone?.Invoke();
        }
    }
}