using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Common
{
    [Serializable]
    public sealed class ViewNameAnnouncerEvent : UnityEvent<string>
    {
    }

    public sealed class ViewNameAnnouncer : NameObjectSetterFromViewName
    {
        public ViewNameAnnouncerEvent onNameAnnounced;

        private void OnEnable()
        {
            SubscribeOnClickEvent();
        }

        private void OnDisable()
        {
            UnsubscribeFromClickEvent();
        }

        private void SubscribeOnClickEvent()
        {
            GetComponent<Button>().onClick.AddListener(OnNameAnnounced);
        }

        private void UnsubscribeFromClickEvent()
        {
            GetComponent<Button>().onClick.RemoveListener(OnNameAnnounced);
        }

        public void OnNameAnnounced()
        {
            onNameAnnounced.Invoke(Name);
        }
    }
}