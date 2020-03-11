using System;
using System.Collections.Generic;
using Common;
using UnityEngine;
using Views.ViewElements;

namespace Views
{
    public class MyChallengeView : BaseView, IDropdownList, IRelatedItemsSelection
    {
        [SerializeField] private ItemsDropdownList dropdownList;

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
    }
}