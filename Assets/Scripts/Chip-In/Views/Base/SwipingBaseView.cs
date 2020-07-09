using Common.UnityEvents;
using InputDetection;
using TrackingNotifiers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Views.Base
{
    public abstract class SwipingBaseView : BaseView
    {
        [SerializeField] private SwipeInRectTracker swipeInRectTracker;


        #region Untiy Events

        public UnityEvent swipedRight;
        public UnityEvent swipedLeft;
        public UnityEvent swipedUp;
        public UnityEvent swipedDown;
        public SwipeDataUnityEvent swiped;

        #endregion


        public SwipingBaseView(string tag) : base(tag)
        {
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            swipeInRectTracker.SubscribeOnInputTranslatorEvents();
            swipeInRectTracker.SwipedInRect += ReceiveSwipe;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            swipeInRectTracker.UnsubscribeFromInputTranslatorEvents();
            swipeInRectTracker.SwipedInRect -= ReceiveSwipe;
        }

        private void ReceiveSwipe(SwipeDetector.SwipeData swipeData)
        {
            OnSwiped(swipeData);
            OnSwiped(swipeData.Direction);
        }

        private void OnSwiped(in MoveDirection swipeDetector)
        {
            switch (swipeDetector)
            {
                case MoveDirection.Left:
                    OnSwipedLeft();
                    break;
                case MoveDirection.Right:
                    OnSwipedRight();
                    break;
                case MoveDirection.Up:
                    OnSwipedUp();
                    break;
                case MoveDirection.Down:
                    OnSwipedDown();
                    break;
            }
        }

        private void OnSwipedDown()
        {
            swipedDown.Invoke();
        }

        private void OnSwipedUp()
        {
            swipedUp.Invoke();
        }

        private void OnSwipedLeft()
        {
            swipedLeft.Invoke();
        }

        private void OnSwipedRight()
        {
            swipedRight.Invoke();
        }

        private void OnSwiped(in SwipeDetector.SwipeData swipeData)
        {
            swiped.Invoke(swipeData);
        }
    }
}