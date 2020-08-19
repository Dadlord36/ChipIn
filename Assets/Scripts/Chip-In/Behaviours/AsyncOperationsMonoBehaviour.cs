using Common;
using Controllers;
using ScriptableObjects.CardsControllers;
using UnityEngine;
using UnityWeld.Binding;

namespace Behaviours
{
    [Binding]
    public abstract class AsyncOperationsMonoBehaviour : MonoBehaviour
    {
        private readonly AsyncOperationCancellationController _asyncOperationCancellationController = new AsyncOperationCancellationController();
        protected ref DisposableCancellationTokenSource TasksCancellationTokenSource =>
            ref _asyncOperationCancellationController.TasksCancellationTokenSource;
        
        private static AwaitingProcessVisualizerController MainAwaitingProcessVisualizerController => GameManager.MainAwaitingProcessVisualizerController;

        private bool _awaitingProcess;

        
        public bool IsAwaitingProcess
        {
            get => _awaitingProcess;
            set
            {
                if (value == _awaitingProcess) return;
                _awaitingProcess = value;
                if (value)
                    MainAwaitingProcessVisualizerController.Show();
                else
                    MainAwaitingProcessVisualizerController.Hide();
            }
        }
        
        private void OnDisable()
        {
            _asyncOperationCancellationController.CancelOngoingTask();
        }
    }
}