using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Controllers
{
    [RequireComponent(typeof(Image))]
    public class IconSizeFitterController : UIBehaviour
    {
        private RectTransform _parentRectTransform;
        private Image _image;


        protected override void Awake()
        {
            base.Awake();
            FindControlledComponent();
            ResetElementSize();
        }
        
        public void FitImage()
        {
            ResetElementSize();

            var preferredWidth = _image.preferredWidth;
            var preferredHeight = _image.preferredHeight;
            var aspectRatio = preferredWidth / preferredHeight;


            var scale = Mathf.Abs(preferredWidth / _parentRectTransform.rect.x);
            var rectTransform = _image.rectTransform;

            var sizeDelta = new Vector2(preferredWidth, preferredHeight) / scale;

            var difference = _parentRectTransform.rect.size - sizeDelta;

            if (preferredWidth > preferredHeight)
            {
                sizeDelta.y += difference.y;
                sizeDelta.x = sizeDelta.y * aspectRatio;
            }
            else
            {
                sizeDelta.x += difference.x;
                sizeDelta.y = sizeDelta.x / aspectRatio;
            }

            rectTransform.sizeDelta = sizeDelta;
        }

        private void ResetElementSize()
        {
            _image.rectTransform.sizeDelta = Vector2.zero;
        }
        
        private void FindControlledComponent()
        {
            _image = GetComponent<Image>();
            _parentRectTransform = _image.transform.parent.GetComponent<RectTransform>();
        }
    }
}