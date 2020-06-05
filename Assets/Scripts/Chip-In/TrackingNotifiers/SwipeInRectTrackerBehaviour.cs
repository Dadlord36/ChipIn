using UnityEngine;

namespace TrackingNotifiers
{
    public class SwipeInRectTrackerBehaviour : MonoBehaviour
    {
        [SerializeField] private SwipeInRectTracker swipeInRectTracker;
        private void OnEnable()
        {
            swipeInRectTracker.SubscribeOnInputTranslatorEvents();
        }

        private void OnDisable()
        {
            swipeInRectTracker.UnsubscribeFromInputTranslatorEvents();
        }
    }
}