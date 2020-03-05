using System;
using InputDetection;
using ScriptableObjects.Parameters;
using UnityEngine;

namespace ActionsTranslators
{
    public interface IUpdatable
    {
        void Update();
    }
    
    [CreateAssetMenu(fileName = nameof(MainInputActionsTranslator), menuName = nameof(ActionsTranslators) + "/" + nameof(MainInputActionsTranslator),
        order = 0)]
    public sealed class MainInputActionsTranslator : ScriptableObject, IUpdatable
    {
        public event Action EscapeButtonPressed;
        public event Action<SwipeDetector.SwipeData> Swiped
        {
            add => _swipeDetector.Swiped+= value;
            remove => _swipeDetector.Swiped-= value;
        }

        [SerializeField] private SwipeDetectorParameters swipeDetectorParameters;
        private SwipeDetector _swipeDetector;
        
        private void OnEnable()
        {
            _swipeDetector = new SwipeDetector(swipeDetectorParameters);
        }

        void IUpdatable.Update()
        {
            _swipeDetector.Update();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnReturnButtonPressed();
            }
        }

        private void OnReturnButtonPressed()
        {
            EscapeButtonPressed?.Invoke();
        }
    }
}