using ScriptableObjects.Parameters;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityWeld.Binding;

namespace Controllers
{
    [RequireComponent(typeof(Image))]
    [Binding]
    public sealed class ImageFadingController : UIBehaviour
    {
        [SerializeField] private FloatParameter fadingDurationParameter;
        private bool _isShown;

        private float FadingDuration => fadingDurationParameter.value;

        [Binding]
        public bool IsShown
        {
            get => _isShown;
            set
            {
                if (value == _isShown) return;
                _isShown = value;
                FadeImage(value);
            }
        }

        [Binding]
        public bool InitialState
        {
            get => _isShown;
            set
            {
                if (value == _isShown) return;
                _isShown = value;
                FadeInstantly(value);
            }
        }

        private Image ControlledImage => GetComponent<Image>();

        protected override void OnEnable()
        {
            base.OnEnable();
            FadeInstantly(IsShown);
        }

        private void FadeInstantly(bool state)
        {
            if (state)
            {
                FadeInInstantly();
            }
            else
            {
                FadeOutInstantly();
            }
        }

        private void FadeInInstantly()
        {
            ControlledImage.CrossFadeAlpha(1f, 0f, true);
        }

        private void FadeOutInstantly()
        {
            ControlledImage.CrossFadeAlpha(0f, 0f, true);
        }

        private void FadeImage(bool toVisible)
        {
            ControlledImage.CrossFadeAlpha(toVisible ? 1f : 0f, FadingDuration, true);
        }
    }
}