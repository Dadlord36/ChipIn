using Tasking;
using UnityEngine;

namespace ScriptableObjects.CardsControllers
{
    [CreateAssetMenu(fileName = nameof(AwaitingProcessVisualizerController), menuName = nameof(CardsControllers) + "/" +
                                                                                        nameof(AwaitingProcessVisualizerController), order = 0)]
    public class AwaitingProcessVisualizerController : ScriptableObject
    {
        [SerializeField] private Object visualizerPrefab;
        private GameObject _progressBarObject;
        private static Transform MainCanvasTransform => GameManager.MainCanvas.transform;

        public void Show()
        {
            TasksFactories.ExecuteOnMainThread(delegate
            {
                if (!_progressBarObject)
                    _progressBarObject = Instantiate(visualizerPrefab, MainCanvasTransform) as GameObject;
            });
        }

        public void Hide()
        {
            TasksFactories.ExecuteOnMainThread(delegate
            {
                if (_progressBarObject)
                    Destroy(_progressBarObject);
            });
        }
    }
}