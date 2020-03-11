using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Views.ViewElements.Interfaces;

namespace Views.ViewElements.ScrollableList
{
    public sealed class ScrollableItemsSelector : UIBehaviour, IContentItemUpdater
    {
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
                OnNewItemSelected(value);
            }
        }

        public void UpdateContentItem(Transform contentItem, float pathPercentage)
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