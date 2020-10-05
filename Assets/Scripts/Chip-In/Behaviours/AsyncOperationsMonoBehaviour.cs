using Common;
using Common.AsyncTasksManagement;
using Controllers;
using ScriptableObjects.CardsControllers;
using UnityEngine;
using UnityWeld.Binding;

namespace Behaviours
{
    [Binding]
    public abstract class AsyncOperationsMonoBehaviour : MonoBehaviour
    {
        private readonly AsyncOperationsBase _asyncOperationsBase = new AsyncOperationsBase();

        protected AsyncOperationCancellationController AsyncOperationCancellationController => _asyncOperationsBase.AsyncOperationCancellationController;

        protected ref DisposableCancellationTokenSource TasksCancellationTokenSource => ref AsyncOperationCancellationController.TasksCancellationTokenSource;

        protected bool IsAwaitingProcess
        {
            get => _asyncOperationsBase.IsAwaitingProcess;
            set => _asyncOperationsBase.IsAwaitingProcess = value;
        }

        private void OnDisable()
        {
            AsyncOperationCancellationController.CancelOngoingTask();
        }
    }
}