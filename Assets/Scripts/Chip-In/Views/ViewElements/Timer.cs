using System.Globalization;
using Common.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Views.ViewElements
{
    public class Timer : UIBehaviour
    {
        [SerializeField] private VariableBehaviourTimeline timeline;
        [SerializeField] private TMP_Text countdownText;
        private float _interval;

        private string CountdownText
        {
            get => countdownText.text;
            set => countdownText.text = value;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            timeline.Progressing += TimelineOnProgressing;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            timeline.Progressing -= TimelineOnProgressing;
        }

        private void TimelineOnProgressing(float percentage)
        {
            SetCountdownText(percentage);
        }

        private void SetCountdownText(float percentage)
        {
            // string.Format("{0:f1}", Mathf.Lerp(_interval, 0f,percentage)) 
            int reversedPercentage = (int) Mathf.Lerp(_interval, 0f, percentage);
            CountdownText = reversedPercentage.ToString();
        }

        public void SetAndStartTimer(float timeInterval)
        {
            timeline.StartTimer(_interval = timeInterval);
        }
    }
}