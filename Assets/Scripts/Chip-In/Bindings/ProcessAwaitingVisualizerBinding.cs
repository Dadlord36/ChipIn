using ScriptableObjects.CardsControllers;
using UnityEngine;

namespace Bindings
{
    public class ProcessAwaitingVisualizerBinding : MonoBehaviour
    {
        private static AwaitingProcessVisualizerController MainAwaitingProcessVisualizerController => GameManager.MainAwaitingProcessVisualizerController;
        private bool _isAwaiting;

        public bool IsAwaiting
        {
            get => _isAwaiting;
            set
            {
                if (_isAwaiting == value) return;
                _isAwaiting = value;
                SwitchVisualizerActivity(value);
            }
        }

        private void SwitchVisualizerActivity(bool visible)
        {
            if (visible)
                MainAwaitingProcessVisualizerController.Show();
            else
            {
                MainAwaitingProcessVisualizerController.Hide();
            }
        }
    }
}