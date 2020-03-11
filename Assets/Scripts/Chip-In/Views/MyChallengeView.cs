using System;
using System.Collections.Generic;
using Common;
using UnityEngine;
using Views.InteractiveWindows;
using Views.InteractiveWindows.Interfaces;
using Views.ViewElements;

namespace Views
{
    public class MyChallengeView : BaseView, IDropdownList, IRelatedItemsSelection, IInfoPanelView
    {
        [SerializeField] private ItemsDropdownList dropdownList;
        [SerializeField] private InfoPanelView offerInfoCard;

        public event Action<int> RelatedItemSelected
        {
            add => dropdownList.RelatedItemSelected += value;
            remove => dropdownList.RelatedItemSelected -= value;
        }

        public event Action ItemsListUpdated
        {
            add => dropdownList.ItemsListUpdated += value;
            remove => dropdownList.ItemsListUpdated -= value;
        }

        public void FillDropdownList(Dictionary<int, string> itemsDictionary)
        {
            dropdownList.FillDropdownList(itemsDictionary);
        }

        public void FillCardWithData(IInfoPanelData data)
        {
            offerInfoCard.FillCardWithData(data);
        }

        public void ShowInfoCard()
        {
            offerInfoCard.ShowInfoCard();
        }

        public void HideInfoCard()
        {
            offerInfoCard.HideInfoCard();
        }
    }
}