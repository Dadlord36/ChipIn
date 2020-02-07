using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utilities;

namespace Common
{
    public interface IDropdownList
    {
        event Action<int> SelectedItemIndexChanged;
        event Action ItemsListUpdated;
        void FillDropdownList(string[] itemsList);
    }

    [Serializable]
    public class TMP_DropdownController : IDropdownList
    {
        public event Action<int> SelectedItemIndexChanged;
        public event Action ItemsListUpdated;

        private const string Tag = nameof(TMP_DropdownController);
        [SerializeField] private TMP_Dropdown tmp_Dropdown;

        public void OnEnabled()
        {
            SubscribeOnEvents();
        }

        public void OnDisabled()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeOnEvents()
        {
            tmp_Dropdown.onValueChanged.AddListener(OnValueChanged);
        }

        private void UnsubscribeFromEvents()
        {
            tmp_Dropdown.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(int value)
        {
            OnSelectedItemIndexChanged(value);
        }
        
        public void FillDropdownList(List<TMP_Dropdown.OptionData> options)
        {
            tmp_Dropdown.ClearOptions();
            tmp_Dropdown.AddOptions(options);
            OnItemsListUpdated();
            LogUtility.PrintLog(Tag, $"Dropdown list \"{tmp_Dropdown.name}\" was refilled");
        }

        public void FillDropdownList(string[] itemsList)
        {
            var optionsList = new List<TMP_Dropdown.OptionData>(itemsList.Length);

            for (int i = 0; i < itemsList.Length; i++)
            {
                optionsList.Add(new TMP_Dropdown.OptionData(itemsList[i]));
            }

            FillDropdownList(optionsList);
        }

        protected virtual void OnSelectedItemIndexChanged(int index)
        {
            SelectedItemIndexChanged?.Invoke(index);
        }

        protected virtual void OnItemsListUpdated()
        {
            ItemsListUpdated?.Invoke();
        }
    }
}