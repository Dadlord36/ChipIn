using ScriptableObjects.CardsControllers;
using TMPro;
using UnityEngine;

namespace Views.Cards
{
    public class AlertsCardView : BaseView
    {
        [SerializeField] private TMP_Text textBlock;
        [SerializeField] private CanvasGroup cardCanvasGroup;
        [SerializeField] private float fadingTime;
        [SerializeField] private AlertCardController alertCardController;

        [SerializeField, Tooltip("Card transparency evolution over show-time. Curve time should be from 0 to 1")]
        private AnimationCurve transparencyCurve;

        private float _time;
        private float _progress;

        private string TextInBlock
        {
            get => textBlock.text;
            set => textBlock.text = value;
        }

        protected override void Start()
        {
            base.Start();

            alertCardController.SetAlertCardViewToControl(this);
            StopAnimating();
        }


        public void ShowUpAndFadeOut(string textToShow)
        {
            TextInBlock = textToShow;
            StartAnimating();
        }

        private void StartAnimating()
        {
            enabled = true;
        }

        private void ResetTrackingVariables()
        {
            _time = _progress = 0f;
        }

        private void StopAnimating()
        {
            enabled = false;
            ResetTrackingVariables();
        }

        private void Update()
        {
            _progress = Mathf.InverseLerp(0, fadingTime, _time);
            SetCardTransparency(_progress);
            _time += Time.deltaTime;
            if (!(_progress >= 1.0f)) return;
            StopAnimating();
        }

        private void SetCardTransparency(float progress)
        {
            cardCanvasGroup.alpha = transparencyCurve.Evaluate(progress);
        }
    }
}