using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;
using RectTransformUtility = Utilities.RectTransformUtility;

#if UNITY_EDITOR
using EasyButtons;
#endif

namespace Controllers
{
    [RequireComponent(typeof(RectTransform))]
    public class ScreenSizeFitter : UIBehaviour
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            FillTheScreen();
        }

#if UNITY_EDITOR
        [Button]
#endif
        private void FillTheScreen()
        {
            var rectTransform = transform as RectTransform;
            if (!rectTransform) return;
            
            RectTransformUtility.Centralize(rectTransform);
            rectTransform.sizeDelta = ScreenUtility.GetScreenSize();
        }
    }
}