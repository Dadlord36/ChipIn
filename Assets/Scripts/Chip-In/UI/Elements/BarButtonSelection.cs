using ScriptableObjects.Parameters;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Elements
{
    public sealed class BarButtonSelection : UIBehaviour
    {
        [SerializeField] private Image[] selectionViewElements;
        [SerializeField] private FloatParameter crossFadeColorTime;

        protected override void Awake()
        {
            base.Awake();
            Assert.IsNotNull(selectionViewElements);
        }

        private void ShowUpCrossFaded()
        {
            for (int i = 0; i < selectionViewElements.Length; i++)
            {
                CrossFadeImageColor(selectionViewElements[i], 1.0f);
            }
        }

        private void HideCrossFaded()
        {
            for (int i = 0; i < selectionViewElements.Length; i++)
            {
                CrossFadeImageColor(selectionViewElements[i], 0.0f);
            }
        }

        private void CrossFadeImageColor(Image image, float alpha)
        {
            var targetColor = image.color;
            targetColor.a = alpha;
            image.CrossFadeColor(targetColor, crossFadeColorTime.value, false, true);
        }

        public void Show()
        {
            ShowUpCrossFaded();
        }

        public void Hide()
        {
            HideCrossFaded();
        }
    }
}