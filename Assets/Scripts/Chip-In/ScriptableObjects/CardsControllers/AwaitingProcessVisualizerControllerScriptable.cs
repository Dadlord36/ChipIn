using UnityEngine;
using ViewModels.UI.Interfaces;

namespace ScriptableObjects.CardsControllers
{
    [CreateAssetMenu(fileName = nameof(AwaitingProcessVisualizerControllerScriptable), menuName = nameof(CardsControllers) + "/" +
                                                                                        nameof(AwaitingProcessVisualizerControllerScriptable), order = 0)]
    public class AwaitingProcessVisualizerControllerScriptable : ScriptableObject, IViewable
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