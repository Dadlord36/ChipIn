using UnityEngine;
using UnityEngine.EventSystems;

namespace ViewModels.UI
{
    [AddComponentMenu("UI/Adjusting/RectAutoAdjust")]
    public class RectAutoAdjust : UIBehaviour
    {
        [SerializeField, Range(1, byte.MaxValue)]
        private byte times = 1;

        [SerializeField] private RectTransform controlRectTransform;

        protected override void OnEnable()
        {
            base.OnEnable();
            Adjust();
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            Adjust();
        }
#endif

        private void Adjust()
        {
            controlRectTransform.gameObject.SetActive(true);
            
            var rectSize = controlRectTransform.rect;
            rectSize.width *= times;
            if (!TryGetComponent(out RectTransform rectTransform)) return;
            rectTransform.sizeDelta =  new Vector2(rectSize.width, rectSize.height);
            rectTransform.anchoredPosition = Vector2.zero;
            controlRectTransform.gameObject.SetActive(false);
        }
    }
}