using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Controllers
{
    [RequireComponent(typeof(Image))]
    public class IconSizeFitterController : UIBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            ResetElementSize();
        }

        public void FitImage()
        {
            ResetElementSize();

            var image = GetComponent<Image>();
            var parentRectTransform = GetComponent<Image>().transform.parent.GetComponent<RectTransform>();

            var preferredWidth = image.preferredWidth;
            var preferredHeight = image.preferredHeight;
            var aspectRatio = preferredWidth / preferredHeight;


            var scale = Mathf.Abs(preferredWidth / parentRectTransform.rect.x);
            var rectTransform = image.rectTransform;

            var sizeDelta = new Vector2(preferredWidth, preferredHeight) / scale;

            var difference = parentRectTransform.rect.size - sizeDelta;

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
            GetComponent<Image>().rectTransform.sizeDelta = Vector2.zero;
        }
    }
}