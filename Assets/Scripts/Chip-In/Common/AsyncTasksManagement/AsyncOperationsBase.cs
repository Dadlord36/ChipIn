using Controllers;
using ScriptableObjects.CardsControllers;

namespace Common.AsyncTasksManagement
{
    public class AsyncOperationsBase
    {
        public readonly AsyncOperationCancellationController AsyncOperationCancellationController = new AsyncOperationCancellationController();

        public ref DisposableCancellationTokenSource TasksCancellationTokenSource =>
            ref AsyncOperationCancellationController.TasksCancellationTokenSource;

        public static AwaitingProcessVisualizerControllerScriptable MainAwaitingProcessVisualizerControllerScriptable =>
            GameManager.MainAwaitingProcessVisualizerControllerScriptable;

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

        protected virtual void CancelOngoingTask()
        {
            AsyncOperationCancellationController.CancelOngoingTask();
        }
    }
}