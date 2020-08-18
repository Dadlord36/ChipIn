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
        private bool _isFaded;

        private float FadingDuration => fadingDurationParameter.value;

        [Binding]
        public bool IsFaded
        {
            get => _isFaded;
            set
            {
                if (value == _isFaded) return;
                _isFaded = value;
                FadeImage(value);
            }
        }

        private Image ControlledImage => GetComponent<Image>();

        protected override void OnEnable()
        {
            base.OnEnable();
            FadeInstantly();
        }

        private void FadeInstantly()
        {
            if (IsFaded)
            {
                FadeOutInstantly();
            }
            else
            {
                FadeInInstantly();
            }
        }

        protected override void Start()
        {
            base.Start();
            FadeInstantly();
        }

        private void FadeInInstantly()
        {
            ControlledImage.CrossFadeAlpha(1f, 0f, true);
        }

        private void FadeOutInstantly()
        {
            ControlledImage.CrossFadeAlpha(0f, 0f, true);
        }

        private bool _isInitialized;

        private void FadeImage(bool toInvisible)
        {
            ControlledImage.CrossFadeAlpha(toInvisible ? 0f : 1f, _isInitialized ? FadingDuration : 0f, true);
            _isInitialized = true;
        }
    }
}