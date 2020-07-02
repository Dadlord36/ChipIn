using System;
using System.Collections.Generic;
using ActionsTranslators;
using Common;
using Common.Interfaces;

namespace CustomAnimators
{
    public sealed class ProgressiveOperationsController : IUpdatableProgress
    {
        public event Action ProgressReachesEnd;

        private readonly OperationsCompletionTracker _operationsCompletionTracker;
        private bool _shouldUpdate;

        private List<IUpdatable> OngoingUpdatableOperation { get; } = new List<IUpdatable>();

        public ProgressiveOperationsController()
        {
            _operationsCompletionTracker = new OperationsCompletionTracker();
            _operationsCompletionTracker.WhenAllIsDone += OnProgressReachesEnd;
        }

        public void AddProgressiveOperation(IUpdatableProgress progressiveOperation)
        {
            OngoingUpdatableOperation.Add(progressiveOperation);
            InitializeProgressiveOperation(progressiveOperation);
            _shouldUpdate = true;
        }

        public void Update()
        {
            if (!_shouldUpdate) return;
            foreach (var updatable in OngoingUpdatableOperation)
            {
                updatable.Update();
            }
        }

        private void InitializeProgressiveOperation(INotifyProgressReachesEnd updatableProgress)
        {
            void ProcessReachingEnd()
            {
                updatableProgress.ProgressReachesEnd -= ProcessReachingEnd;
                _shouldUpdate = false;
                _operationsCompletionTracker.ConfirmActionCompletion();
            }

            updatableProgress.ProgressReachesEnd += ProcessReachingEnd;
            _operationsCompletionTracker.AddToCounter();
        }

        public void Clear()
        {
            OngoingUpdatableOperation.Clear();
        }

        private void OnProgressReachesEnd()
        {
            ProgressReachesEnd?.Invoke();
            ProgressReachesEnd = null;
        }
    }
}