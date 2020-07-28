using System;
using Common;
using ScriptableObjects.DataSets;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Bars.BarItems
{
    public sealed class ScrollBarItemWithTitleAndIconView : ScrollBarItemWithTextView, IScrollBarItem
    {
        public event Action<string> Selected;

        [SerializeField] private Image iconImage;
        [SerializeField] private int id;


        private PointClickRetranslator PointClickRetranslatorComponent => GetComponent<PointClickRetranslator>();

        public Sprite IconSprite
        {
            get => iconImage.sprite;
            set => iconImage.sprite = value;
        }

        public int? Id
        {
            get => id;
            set
            {
                if (value != null) id = (int) value;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            PointClickRetranslatorComponent.pointerClicked.AddListener(OnPointerClick);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            PointClickRetranslatorComponent.pointerClicked.RemoveListener(OnPointerClick);
        }

        private void OnPointerClick()
        {
            OnSelected();
        }

        public override void Set(IScrollBarItem scrollBarItemData)
        {
            base.Set(scrollBarItemData);
            IconSprite = scrollBarItemData.IconSprite;
            id = (int) scrollBarItemData.Id;
        }

        private void OnSelected()
        {
            Selected?.Invoke(Title);
        }


    }
}