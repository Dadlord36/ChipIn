using Common;
using Common.Interfaces;
using Controllers;
using UnityEngine;

namespace ScriptableObjects
{
    public abstract class AsyncOperationsScriptableObject : ScriptableObject, IApplicationClosingEventReceiver
    {
        private readonly AsyncOperationCancellationController _asyncOperationCancellationController = new AsyncOperationCancellationController();

        protected ref DisposableCancellationTokenSource TasksCancellationTokenSource =>
            ref _asyncOperationCancellationController.TasksCancellationTokenSource;

        void IApplicationClosingEventReceiver.OnApplicationClosing()
        {
            CancelOngoingTask();
        }

        protected virtual void CancelOngoingTask()
        {
            _asyncOperationCancellationController.CancelOngoingTask();
        }
    }
}