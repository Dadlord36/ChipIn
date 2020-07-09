using System;
using ActionsTranslators;
using InputDetection;
using UnityEngine;

namespace TrackingNotifiers
{
    [Serializable]
    public sealed class SwipeInRectTracker
    {
        public event Action<SwipeDetector.SwipeData> SwipedInRect;
        [SerializeField] private RectTransform controlRectTransform;
        [SerializeField] private MainInputActionsTranslator inputActionsTranslator;

        public SwipeInRectTracker(RectTransform controlRectTransform, MainInputActionsTranslator inputActionsTranslator)
        {
            this.controlRectTransform = controlRectTransform;
            this.inputActionsTranslator = inputActionsTranslator;
        }

        public void SubscribeOnInputTranslatorEvents()
        {
            inputActionsTranslator.Swiped += InputActionsTranslatorOnSwiped;
        }

        public void UnsubscribeFromInputTranslatorEvents()
        {
            inputActionsTranslator.Swiped -= InputActionsTranslatorOnSwiped;
        }

        private void InputActionsTranslatorOnSwiped(SwipeDetector.SwipeData swipeData)
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(controlRectTransform, swipeData.TouchDownPoint)) return;
            OnSwipedInRect(swipeData);
        }

        private void OnSwipedInRect(in SwipeDetector.SwipeData moveDirection)
        {
            SwipedInRect?.Invoke(moveDirection);
        }
    }
}