using System.ComponentModel;
using System.Runtime.CompilerServices;
using EasyButtons;
using JetBrains.Annotations;
using Tasking;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels.UI.Elements.OptionsSelectors
{
    [Binding]
    public sealed class OptionsSelectorViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        public UnityEvent newItemSelected;
        [SerializeField] private bool triggerItemSelectionEventOnIndexChange;

        private int _selectedItemIndex;

        [Binding]
        public int SelectedItemIndex
        {
            get => _selectedItemIndex;
            set
            {
                if (value == _selectedItemIndex) return;
                _selectedItemIndex = value;
                OnPropertyChanged();
                if (triggerItemSelectionEventOnIndexChange)
                    OnNewItemSelected();
            }
        }

        [Binding]
        public void SelectButton_OnClick()
        {
            OnNewItemSelected();
        }


#if UNITY_EDITOR
        [SerializeField] private Object prefab;
        [SerializeField] private Transform container;
        [SerializeField] private int itemsAmount;

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
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}