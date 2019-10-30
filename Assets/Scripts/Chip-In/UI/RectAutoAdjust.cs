using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    [AddComponentMenu("UI/Adjusting/RectAutoAdjust")]
    public class RectAutoAdjust : UIBehaviour
    {
        [SerializeField, Range(1, byte.MaxValue)]
        private byte times = 1;

        [SerializeField]
        private Vector2Int targetResolution;

        protected override void OnEnable()
        {
#if !UNITY_EDITOR
            targetResolution = new Vector2Int(Screen.width,Screen.height
#endif
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
            if (!TryGetComponent(out RectTransform rectTransform)) return;
            var sizeDelta = rectTransform.sizeDelta;
            sizeDelta.x = targetResolution.x * times;
            rectTransform.sizeDelta = sizeDelta;
        }
    }
}