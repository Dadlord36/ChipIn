using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ViewModels.UI
{
    public class DiagramColumnView : UIBehaviour
    {
        [SerializeField] private Color color;
        [Space(10)] [SerializeField] private Image frameGraphics;
        [SerializeField] private Image fillerGraphics1;
        [SerializeField] private Image fillerGraphics2;
        [Space(10)] [SerializeField] private TMP_Text topText;
        [SerializeField] private TMP_Text bottomText;

        private float FillerYScale1
        {
            get => fillerGraphics1.rectTransform.localScale.y;
            set
            {
                var fillerTransform = fillerGraphics1.rectTransform;
                Vector2 scale = fillerTransform.localScale;
                scale.y = value;
                fillerTransform.localScale = scale;
            }
        }

        private float FillerYScale2
        {
            get => fillerGraphics2.rectTransform.localScale.y;
            set
            {
                var fillerTransform = fillerGraphics2.rectTransform;
                Vector2 scale = fillerTransform.localScale;
                scale.y = value;
                fillerTransform.localScale = scale;
            }
        }

        public float PercentageTop
        {
            get => FillerYScale1;
            set => FillerYScale1 = Mathf.Lerp(0f, 1f, value);
        }

        public float PercentageBottom
        {
            get => FillerYScale2;
            set => FillerYScale2 = Mathf.Lerp(0f, 1f, value);
        }

        public void ApplyColor()
        {
            fillerGraphics1.color = fillerGraphics2.color = frameGraphics.color = color;
            bottomText.color = topText.color = color;
        }

        public void SetValues(Vector2 topBottomValues)
        {
            FillerYScale1 = topBottomValues.x;
            FillerYScale2 = topBottomValues.y;
        }
    }
}