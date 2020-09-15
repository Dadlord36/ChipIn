using UnityEngine;
using ViewModels.UI.Interfaces;

namespace ScriptableObjects.CardsControllers
{
    public class AwaitingProcessVisualizerControllerBehaviour : MonoBehaviour, IViewable
    {
        [SerializeField] private AwaitingProcessVisualizerController awaitingProcessVisualizerController;

        public void Show()
        {
            awaitingProcessVisualizerController.Show();
        }

        public void Hide()
        {
            awaitingProcessVisualizerController.Hide();
        }
    }
}