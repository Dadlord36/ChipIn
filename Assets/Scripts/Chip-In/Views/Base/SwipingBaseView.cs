using TrackingNotifiers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Views.Base
{
    public abstract class SwipingBaseView : BaseView
    {
        protected readonly string Tag;

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
            swipeInRectTracker.SwipedInRect += OnSwiped;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            swipeInRectTracker.UnsubscribeFromInputTranslatorEvents();
            swipeInRectTracker.SwipedInRect -= OnSwiped;
        }

        protected abstract void OnSwiped(MoveDirection swipeDetector);
    }
}