using System;
using Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Views.ViewElements
{
    public class ItemsDropdownList : UIBehaviour, IDropdownList
    {
        
        [SerializeField] private TMP_DropdownController dropdownController;

        public event Action<int> SelectedItemIndexChanged
        {
            add => dropdownController.SelectedItemIndexChanged += value;
            remove => dropdownController.SelectedItemIndexChanged -= value;
        }

        public event Action ItemsListUpdated
        {
            add => dropdownController.ItemsListUpdated += value;
            remove => dropdownController.ItemsListUpdated -= value;
        }


        public void FillDropdownList(string[] itemsList)
        {
            dropdownController.FillDropdownList(itemsList);
        }
    }
}