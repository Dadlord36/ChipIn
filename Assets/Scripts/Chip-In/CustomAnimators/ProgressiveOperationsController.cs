using System;
using ActionsTranslators;
using Common.Interfaces;

namespace CustomAnimators
{
    public class ProgressiveOperationsController : IUpdatableProgress
    {
        public event Action ProgressReachesEnd;

        private IUpdatable ongoingUpdatableOperation;
        private bool _shouldUpdate = true;

        private IUpdatable OngoingUpdatableOperation
        {
            get => ongoingUpdatableOperation;
            set => ongoingUpdatableOperation = value;
        }

        public void StartAnimation(IUpdatableProgress progressiveOperation)
        {
            ongoingUpdatableOperation = progressiveOperation;
            InitializeProgressiveOperation(progressiveOperation);
        }

        public void Update()
        {
            if(!_shouldUpdate) return;
            OngoingUpdatableOperation.Update();
        }

        private void InitializeProgressiveOperation(INotifyProgressReachesEnd updatableProgress)
        {
            void ProcessReachingEnd()
            {
                updatableProgress.ProgressReachesEnd -= ProcessReachingEnd;
                _shouldUpdate = false;
                OnProgressReachesEnd();
            }

            updatableProgress.ProgressReachesEnd += ProcessReachingEnd;
            _shouldUpdate = true;
        }


        protected virtual void OnProgressReachesEnd()
        {
            ProgressReachesEnd?.Invoke();
        }
    }
}