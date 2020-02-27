using System;
using System.Collections.Generic;
using CustomAnimators;
using DataModels.MatchModels;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using Utilities;
using ViewModels.UI.Elements.Icons;
using Views.Interfaces;

namespace Views
{
    public class SlotsView : UIBehaviour, ISlotsView
    {
        private const string Tag = nameof(SlotsView);

        [SerializeField] private GameSlotIconView[] slotIconViews;
        [SerializeField] private float slotsSpritesAnimationSwitchingInterval = 0.1f;

        public void SetSlotsIcons(List<BoardIconData> boardIconsData)
        {
            var length = boardIconsData.Count;

            if (length != slotIconViews.Length)
            {
                LogUtility.PrintLogError(Tag, "There is not enough sprites in given array for this slots grid", this);
                return;
            }

            for (int i = 0; i < length; i++)
            {
                slotIconViews[i].InitializeAnimator(boardIconsData[i].AnimatedIconResource,
                    slotsSpritesAnimationSwitchingInterval, true);
            }
        }

        public void StartSlotsAnimation()
        {
            for (int i = 0; i < slotIconViews.Length; i++)
            {
                slotIconViews[i].StartAnimating();
            }
        }

        public void SetSlotsActivity(IReadOnlyList<IActive> iconsActivity)
        {
            var length = iconsActivity.Count;

            Assert.IsTrue(length == slotIconViews.Length);

            for (int i = 0; i < length; i++)
            {
                slotIconViews[i].ActivityState = iconsActivity[i].Active;
            }
        }
    }
}