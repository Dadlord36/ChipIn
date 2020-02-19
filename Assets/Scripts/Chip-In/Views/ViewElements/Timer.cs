﻿using System;
using Common.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Views.ViewElements
{
    
    public class Timer : UIBehaviour, IInitialize
    {
        [SerializeField] private VariableBehaviourTimeline timeline;
        [SerializeField] private TMP_Text countdownText;
        private float _interval;

        public event Action OnElapsed
        {
            add => timeline.OnElapsed += value;
            remove => timeline.OnElapsed -= value;
        }

        private string CountdownText
        {
            get => countdownText.text;
            set => countdownText.text = value;
        }
        
        public void Initialize()
        {
            timeline.Initialize();
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
            CountdownText = ((int) Mathf.Lerp(_interval, 0f, percentage)).ToString();
        }

        public void SetAndStartTimer(float timeInterval)
        {
            timeline.StartTimer(_interval = timeInterval);
        }


    }
}