using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Views.ViewElements
{
    public interface IRelatedItemsSelection
    {
        event Action<int> RelatedItemSelected;
    }

    public class ItemsDropdownList : UIBehaviour, IDropdownList, IRelatedItemsSelection
    {
        [SerializeField] private TMP_DropdownController dropdownController;
        public event Action<int> RelatedItemSelected;

        private int _currentlySelectedDropdownItemNum; 
        private int[] _relatedItemsIndexes;

        public event Action ItemsListUpdated
        {
            add => dropdownController.ItemsListUpdated += value;
            remove => dropdownController.ItemsListUpdated -= value;
        }

        public int CurrentlySelectedOfferId
        {
            get
            {
                if (_relatedItemsIndexes == null || _relatedItemsIndexes.Length == 0)
                {
                    return int.MinValue;
                }

                return _relatedItemsIndexes[_currentlySelectedDropdownItemNum];
            }
        }


        protected override void OnEnable()
        {
            base.OnEnable();
            dropdownController.OnEnabled();
            dropdownController.SelectedItemIndexChanged += OnRelatedItemSelected;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            dropdownController.OnDisabled();
            dropdownController.SelectedItemIndexChanged -= OnRelatedItemSelected;
        }

        private void OnRelatedItemSelected(int index)
        {
            _currentlySelectedDropdownItemNum = index;
            RelatedItemSelected?.Invoke(CurrentlySelectedOfferId);
        }

        /// <summary>
        /// Fills dropdown menu with string values of dictionary, while use keys, that are represents indexes in related array,
        /// to send them via event RelatedItemSelected, so that it was easy to get corresponding to selection item form related array.
        /// </summary>
        /// <param name="itemsDictionary">Dictionary of items, consisted from related array indexes and some string values.
        /// String values will be used to fill the dropdown menu. </param>
        public void FillDropdownList(Dictionary<int, string> itemsDictionary)
        {
            _relatedItemsIndexes = itemsDictionary.Keys.ToArray();
            dropdownController.FillDropdownList(itemsDictionary);
        }
    }
}