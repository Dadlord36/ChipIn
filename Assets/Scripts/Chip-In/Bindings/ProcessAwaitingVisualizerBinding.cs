using ScriptableObjects.CardsControllers;
using UnityEngine;

namespace Bindings
{
    public class ProcessAwaitingVisualizerBinding : MonoBehaviour
    {
        private static AwaitingProcessVisualizerControllerScriptable MainAwaitingProcessVisualizerControllerScriptable => GameManager.MainAwaitingProcessVisualizerControllerScriptable;
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
                MainAwaitingProcessVisualizerControllerScriptable.Show();
            else
            {
                MainAwaitingProcessVisualizerControllerScriptable.Hide();
            }
        }
    }
}