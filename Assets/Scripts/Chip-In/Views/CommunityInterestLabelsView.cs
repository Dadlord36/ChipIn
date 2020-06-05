﻿using System;
using System.Collections.Generic;
using DataModels.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using ViewModels;
using Views.Base;

namespace Views
{
    public sealed class CommunityInterestLabelsView : SwipingBaseView
    {
        [SerializeField] private GridElementsViewModel gridElementsViewModel;

        public event Action SwipedLeft;
        public event Action SwipedRight;
        
        public CommunityInterestLabelsView() : base(nameof(CommunityInterestLabelsView))
        {
        }

        public void UpdateGridItemsContent(IReadOnlyList<IIndexedNamedPosterUrl> dataRepositoryItems)
        {
            gridElementsViewModel.UpdateGridContent(dataRepositoryItems);
        }

        protected override void OnSwiped(MoveDirection swipeDetector)
        {
            switch (swipeDetector)
            {
                case MoveDirection.Left:
                    OnSwipedLeft();
                    break;
                case MoveDirection.Right:
                    OnSwipedRight();
                    break;
            }
        }

        private void OnSwipedLeft()
        {
            SwipedLeft?.Invoke();
        }

        private void OnSwipedRight()
        {
            SwipedRight?.Invoke();
        }
    }
}