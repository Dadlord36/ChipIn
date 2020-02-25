using System.Collections.Generic;
using CustomAnimators;
using DataModels.MatchModels;
using UnityEngine;
using Views.Interfaces;

namespace Views
{
    public class SlotsGameView : BaseView, ISlotsView
    {
        [SerializeField] private SlotsView slotsView;

        public void SetSlotsActivity(IReadOnlyList<IActive> iconsActivity)
        {
            slotsView.SetSlotsActivity(iconsActivity);
        }

        public void SetSlotsIcons(List<BoardIconData> boardIconsData)
        {
            slotsView.SetSlotsIcons(boardIconsData);
        }

        public void StartSlotsAnimation()
        {
            slotsView.StartSlotsAnimation();
        }
    }
}