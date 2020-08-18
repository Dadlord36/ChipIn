using System.ComponentModel;
using System.Runtime.CompilerServices;
using EasyButtons;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityWeld.Binding;
using Utilities;
using Views.ViewElements.ListItems;

namespace ViewModels.UI.Elements.OptionsSelectors
{
    [Binding]
    public sealed class OptionsSelectorViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        public UnityEvent newItemSelected;

        [SerializeField] private Transform container;
        [SerializeField] private OptionItemView prefab;


        [Binding]
        public int SelectedItemIndex
        {
            get => _selectedItemIndex;
            set
            {
                if (value == _selectedItemIndex) return;
                _selectedItemIndex = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public void SelectButton_OnClick()
        {
            OnNewItemSelected();
        }


#if UNITY_EDITOR
        [SerializeField] private int itemsAmount;
        private int _selectedItemIndex;

        [Button]
        public void RecreateRequiredItems()
        {
            GameObjectsUtility.DestroyTransformAttachments(container, true);
            for (int i = 0; i < itemsAmount; i++)
            {
                PrefabUtility.InstantiatePrefab(prefab, container);
            }
        }
#endif
        
        private void OnNewItemSelected()
        {
            newItemSelected?.Invoke();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}