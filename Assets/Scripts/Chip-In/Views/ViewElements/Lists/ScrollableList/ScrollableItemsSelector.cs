using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;
using Views.Bars.BarItems;
using Views.ViewElements.Interfaces;

namespace Views.ViewElements.Lists.ScrollableList
{
    public sealed class ScrollableItemsSelector : UIBehaviour, IContentItemUpdater
    {
        private const string Tag = nameof(ScrollableItemsSelector);
        public event Action<Transform> NewItemSelected;

        [SerializeField] private RectTransform scrollCenter;
        [SerializeField] private float tolerance;

        private Transform _selectedItem;


        public Transform SelectedItem
        {
            get => _selectedItem;
            private set
            {
                if (ReferenceEquals(_selectedItem, value)) return;
                _selectedItem = value;
                LogUtility.PrintLog(Tag,$"Selected item name: {_selectedItem.GetComponent<ITitled>().Title}");
                // _selectedItem.SetAsLastSibling();
                OnNewItemSelected(value);
            }
        }

        public void UpdateContentItem(Transform contentItem, float pathPercentage)
        {
            TryToFigureOutWhatItemIsInCenter(contentItem);
        }

        private void TryToFigureOutWhatItemIsInCenter(Transform contentItem)
        {
            if (Math.Abs(scrollCenter.position.x - contentItem.position.x) < tolerance)
            {
                SelectedItem = contentItem;
            }
        }

        private void OnNewItemSelected(Transform obj)
        {
            NewItemSelected?.Invoke(obj);
        }
    }
}