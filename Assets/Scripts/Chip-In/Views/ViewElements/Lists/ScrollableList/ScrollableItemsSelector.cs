using System;
using DataModels.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Views.ViewElements.Interfaces;

namespace Views.ViewElements.Lists.ScrollableList
{
    public sealed class ScrollableItemsSelector : UIBehaviour, IContentItemUpdater
    {
        private const string Tag = nameof(ScrollableItemsSelector);

        public UnityEvent newItemSelected;

        [SerializeField] private RectTransform scrollCenter;
        [SerializeField] private float tolerance;

        private Transform _selectedItem;


        public Transform SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (ReferenceEquals(_selectedItem, value)) return;
                _selectedItem = value;
                OnNewItemSelected();
            }
        }

        public int SelectedItemIndex
        {
            get
            {
                var id = SelectedItem.GetComponent<IIdentifier>().Id;
                if (id != null) return (int) id;
                return 0;
            }
            set
            {
                if (SelectedItem)
                    SelectedItem.GetComponent<IIdentifier>().Id = value;
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

        private void OnNewItemSelected()
        {
            newItemSelected?.Invoke();
        }
    }
}