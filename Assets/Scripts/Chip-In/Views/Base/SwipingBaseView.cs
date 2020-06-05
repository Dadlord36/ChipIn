using TrackingNotifiers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Views.Base
{
    public abstract class SwipingBaseView : BaseView
    {
        protected readonly string Tag;

        [SerializeField] private SwipeInRectTracker swipeInRectTracker;


        protected SwipingBaseView(string tag)
        {
            Tag = tag;
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