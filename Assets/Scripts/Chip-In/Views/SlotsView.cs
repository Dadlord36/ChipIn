using System;
using UnityEngine;
using UnityEngine.EventSystems;
using ViewModels.UI.Elements.Icons;
using Views.Interfaces;

namespace Views
{
    public class SlotsView : UIBehaviour, ISlotsView
    {
        [SerializeField] private GameSlotIconView[] slotIconViews;

        public void SetSlotsIcons(Sprite[] sprites)
        {
            var length = sprites.Length;

            if (length != slotIconViews.Length)
                throw new Exception("There is not enough sprites in given array for this slots grid");

            for (int i = 0; i < length; i++)
            {
                slotIconViews[i].SlotIcon = sprites[i];
            }
        }
    }
}