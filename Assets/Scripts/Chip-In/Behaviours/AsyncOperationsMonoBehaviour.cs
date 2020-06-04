using System.Threading;
using Controllers;
using UnityEngine;

namespace Behaviours
{
    public abstract class AsyncOperationsMonoBehaviour : MonoBehaviour
    {
        private readonly AsyncOperationCancellationController _asyncOperationCancellationController = new AsyncOperationCancellationController();
        protected ref CancellationTokenSource TasksCancellationTokenSource => ref _asyncOperationCancellationController.TasksCancellationTokenSource;
        
        private void OnDisable()
        {
            _asyncOperationCancellationController.CancelOngoingTask();
        }
    }
}