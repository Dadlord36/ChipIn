using System.Threading.Tasks;
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

        Task IApplicationClosingEventReceiver.OnApplicationClosing()
        {
            CancelOngoingTask();
            return Task.CompletedTask;
        }

        protected virtual void CancelOngoingTask()
        {
            _asyncOperationCancellationController.CancelOngoingTask();
        }
    }
}