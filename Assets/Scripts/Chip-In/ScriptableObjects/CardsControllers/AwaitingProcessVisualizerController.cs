using System;
using Tasking;
using UnityEngine;
using ViewModels.UI.Interfaces;
using Object = UnityEngine.Object;

namespace ScriptableObjects.CardsControllers
{
    [Serializable]
    public class AwaitingProcessVisualizerController : IViewable
    {
        [SerializeField] private Object visualizerPrefab;
        private GameObject _progressBarObject;
        private static Transform MainCanvasTransform => GameManager.MainCanvas.transform;

        public void Show()
        {
            TasksFactories.ExecuteOnMainThread(delegate
            {
                if (!_progressBarObject)
                    _progressBarObject = Object.Instantiate(visualizerPrefab, MainCanvasTransform) as GameObject;
            });
        }

        public void Hide()
        {
            TasksFactories.ExecuteOnMainThread(delegate
            {
                if (_progressBarObject)
                    Object.Destroy(_progressBarObject);
            });
        }
    }
}