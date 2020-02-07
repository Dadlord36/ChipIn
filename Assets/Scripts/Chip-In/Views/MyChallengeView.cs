using System;
using Common;
using UnityEngine;
using Views.ViewElements;

namespace Views
{
    public class MyChallengeView : BaseView, IDropdownList
    {
        [SerializeField] private ItemsDropdownList dropdownList;

        public event Action<int> SelectedItemIndexChanged
        {
            add => dropdownList.SelectedItemIndexChanged += value;
            remove => dropdownList.SelectedItemIndexChanged -= value;
        }

        public event Action ItemsListUpdated
        {
            add => dropdownList.ItemsListUpdated += value;
            remove => dropdownList.ItemsListUpdated -= value;
        }


        public void FillDropdownList(string[] itemsList)
        {
            dropdownList.FillDropdownList(itemsList);
        }
    }
}