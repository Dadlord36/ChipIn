using System;
using System.Collections.Generic;
using Common;
using UnityEngine;
using Utilities;
using Views.Bars.BarItems;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;
using Views.ViewElements.ScrollViews.Adapters.Parameters;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    public class DesignedListViewAdapter : SelectableListViewAdapter<DesignedListParams, DesignedScrollBarItemDefaultDataModel,
        DefaultFillingViewPageViewHolder<DesignedScrollBarItemBaseViewModel.FieldFillingData, uint>, uint,
        DesignedScrollBarItemBaseViewModel.FieldFillingData, DesignedListViewAdapter.FillingViewAdapterImplementation>
    {
        [SerializeField, Range(0f, 1f)] private float itemsBackgroundAlpha;
        private const int MINItemsToLoop = 10;

        public class FillingViewAdapterImplementation : FillingViewAdapter<DesignedScrollBarItemDefaultDataModel,
            DesignedScrollBarItemBaseViewModel.FieldFillingData>
        {
            public override DesignedScrollBarItemBaseViewModel.FieldFillingData Convert(DisposableCancellationTokenSource cancellationTokenSource,
                DesignedScrollBarItemDefaultDataModel data, uint dataIndexInRepository)
            {
                return new DesignedScrollBarItemBaseViewModel.FieldFillingData(data);
            }
        }

        protected override void OnScrollPositionChanged(double normPos)
        {
            base.OnScrollPositionChanged(normPos);
            if (!IsInitialized || VisibleItemsCount == 0)
                return;
            FindMiddleElementAndRefreshItemsOverlaying();
        }

        private void FindMiddleElementAndRefreshItemsOverlaying()
        {
            FindMiddleElement();
            ControlItemsOverlay();
        }

        public override void SetItems(IList<DesignedScrollBarItemDefaultDataModel> items)
        {
            SetColors(items);

            if (items.Count < MINItemsToLoop)
            {
                var times = Math.DivRem(MINItemsToLoop, items.Count, out int reminder);
                if (reminder != 0)
                    times++;

                var itemsToSet = new List<DesignedScrollBarItemDefaultDataModel>(times);
                for (int i = 0; i < times; i++)
                {
                    itemsToSet.AddRange(items);
                }

                Data.ResetItems(itemsToSet);
                return;
            }

            FindMiddleElementAndRefreshItemsOverlaying();
            base.SetItems(items);
        }

        private void SetColors(IList<DesignedScrollBarItemDefaultDataModel> linearGradientColors)
        {
            CalculateGradientColorsArrays(linearGradientColors.Count, out var startColors, out var endColors);

            for (var index = 0; index < linearGradientColors.Count; index++)
            {
                linearGradientColors[index].StartColor = startColors[index];
                linearGradientColors[index].EndColor = endColors[index];
            }
        }

        private void ControlItemsOverlay()
        {
            void SetVisibleItemsSiblingIndexAsLast(int index)
            {
                _VisibleItems[index].root.SetAsLastSibling();
            }

            for (int i = 0; i < MiddleElementNumber; i++)
            {
                SetVisibleItemsSiblingIndexAsLast(i);
            }

            for (int i = VisibleItemsCount - 1; i > MiddleElementNumber; i--)
            {
                SetVisibleItemsSiblingIndexAsLast(i);
            }

            _VisibleItems[MiddleElementNumber].root.SetAsLastSibling();
        }

        private void CalculateGradientColorsArrays(int shadesNumber, out Color[] startColors, out Color[] endColors)
        {
            startColors = ColorsUtility.GenerateColorsBetweenTwoColors(0f, 0.9f, itemsBackgroundAlpha, shadesNumber);
            endColors = ColorsUtility.GenerateColorsBetweenTwoColors(0.1f, 1f, itemsBackgroundAlpha, shadesNumber);
        }
    }
}