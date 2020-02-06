﻿using UnityEngine;
using Views.Interfaces;

namespace Views
{
    public class SlotsGameView : BaseView, ISlotsView
    {
        [SerializeField] private SlotsView slotsView;
        public void SetSlotsIcons(Sprite[] sprites)
        {
            slotsView.SetSlotsIcons(sprites);
        }
    }
}