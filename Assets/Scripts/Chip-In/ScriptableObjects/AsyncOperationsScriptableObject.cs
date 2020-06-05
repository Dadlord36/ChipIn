using Common;
using Controllers;
using UnityEngine;

namespace ScriptableObjects
{
    public abstract class AsyncOperationsScriptableObject : ScriptableObject
    {
        private readonly AsyncOperationCancellationController _asyncOperationCancellationController = new AsyncOperationCancellationController();
        protected ref DisposableCancellationTokenSource TasksCancellationTokenSource => ref _asyncOperationCancellationController.TasksCancellationTokenSource;

        protected virtual void OnDisable()
        {
            _asyncOperationCancellationController.CancelOngoingTask();
        }
    }
}