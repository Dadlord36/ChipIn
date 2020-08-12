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
        private Image _image;

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

        protected override void Awake()
        {
            base.Awake();
            _image = GetComponent<Image>();
        }

        protected override void Start()
        {
            base.Start();
            FadeInstantly();
        }

        private void FadeInInstantly()
        {
            _image.CrossFadeAlpha(1f, 0f, true);
        }

        private void FadeOutInstantly()
        {
            _image.CrossFadeAlpha(0f, 0f, true);
        }

        private bool _isInitialized;
        private void FadeImage(bool toInvisible)
        {
            _image.CrossFadeAlpha(toInvisible ? 0f : 1f,_isInitialized? FadingDuration : 0f, true);
            _isInitialized = true;
        }
    }
}