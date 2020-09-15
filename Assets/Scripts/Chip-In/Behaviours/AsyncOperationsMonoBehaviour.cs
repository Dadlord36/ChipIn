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
        protected readonly AsyncOperationCancellationController AsyncOperationCancellationController = new AsyncOperationCancellationController();
        protected ref DisposableCancellationTokenSource TasksCancellationTokenSource =>
            ref AsyncOperationCancellationController.TasksCancellationTokenSource;
        
        private static AwaitingProcessVisualizerControllerScriptable MainAwaitingProcessVisualizerControllerScriptable => GameManager.MainAwaitingProcessVisualizerControllerScriptable;

        private bool _awaitingProcess;

        
        public bool IsAwaitingProcess
        {
            get => _awaitingProcess;
            set
            {
                if (value == _awaitingProcess) return;
                _awaitingProcess = value;
                if (value)
                    MainAwaitingProcessVisualizerControllerScriptable.Show();
                else
                    MainAwaitingProcessVisualizerControllerScriptable.Hide();
            }
        }
        
        private void OnDisable()
        {
            AsyncOperationCancellationController.CancelOngoingTask();
        }
    }
}