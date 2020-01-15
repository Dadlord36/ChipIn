using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ViewModels.UI
{
    public class ScrollViewController : UIBehaviour
    {
        public event Action DoneScrolling;
        private ScrollRect _scrollRect;
        
        [SerializeField] private AnimationCurve speedCurve;
        [SerializeField] private float scrollDuration;
        [SerializeField] private float minScrollSpeed;
        [SerializeField] private float maxScrollSpeed;
        
        protected override void Awake()
        {
            base.Awake();
            enabled = false;
        }

        protected override void Start()
        {
            base.Start();
            _scrollRect = GetComponent<ScrollRect>();
            Assert.IsNotNull(_scrollRect);
        }

        public void BeginScrollForward()
        {
            ResetCounters();
            enabled = true;
        }

        private float _currentTime, _pathPercentage;
        private float _speed;

        private void Update()
        {
            _pathPercentage = _currentTime / scrollDuration;
            _speed = Mathf.Lerp(minScrollSpeed,maxScrollSpeed, speedCurve.Evaluate(_pathPercentage));
            _currentTime += Time.deltaTime *  _speed;
            
            _scrollRect.horizontalScrollbar.value = _pathPercentage;
            
            if (_pathPercentage >= 1.0f)
            {
                StopScrolling();
            }
        }

        private void ResetCounters()
        {
            _pathPercentage = _currentTime = 0.0f;
        }

        public void StopScrolling()
        {
            enabled = false;
            OnDoneScrolling();
            
        }

        protected void OnDoneScrolling()
        {
            DoneScrolling?.Invoke();
        }
    }
}