using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace ViewModels.UI.Elements
{
    public class GraphicsFadeSwitcher : BaseAnimatedToggle, IProgress<float>
    {
        [SerializeField] private Graphic mainGraphic, alternativeGraphic;
        private Color _mainColor = Color.white, _alternativeColor = Color.white;

        protected override void Awake()
        {
            Assert.IsNotNull(mainGraphic);
            Assert.IsNotNull(alternativeGraphic);
            base.Awake();
        }

        protected override void SetHandlePositionAlongSlide(float percentage)
        {
            CrossFadeGraphics(percentage);
        }

        public void Report(float value)
        {
            CrossFadeGraphics(InversePercentage(value));
        }

        private void CrossFadeGraphics(float percentage)
        {
            _mainColor.a = percentage;
            _alternativeColor.a = 1f -_mainColor.a;
            
            // _mainColor.a = percentage;
            // _alternativeColor.a = percentage;


            alternativeGraphic.color = _alternativeColor;
            mainGraphic.color = _mainColor;
        }
    }
}