using ScriptableObjects.DataSets;
using Shapes2D;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Views.Bars.BarItems
{
    public class BaseScrollBarItem : BaseView, IScrollBarItemBackground, IPointerClickHandler
    {
        [SerializeField] private Shape backgroundShape;
        
        public Color BackgroundGradientColor1
        {
            get => backgroundShape.settings.fillColor;
            set => backgroundShape.settings.fillColor = value;
        }

        public Color BackgroundGradientColor2
        {
            get => backgroundShape.settings.fillColor2;
            set => backgroundShape.settings.fillColor2 = value;
        }

        private void SetBackground(IScrollBarItemBackground barItemBackground)
        {
            BackgroundGradientColor1 = barItemBackground.BackgroundGradientColor1;
            BackgroundGradientColor2 = barItemBackground.BackgroundGradientColor2;
        }

        public virtual void Set(IScrollBarItem scrollBarItemData)
        {
            SetBackground(scrollBarItemData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // OnNewItemSelected();
        }
    }
}