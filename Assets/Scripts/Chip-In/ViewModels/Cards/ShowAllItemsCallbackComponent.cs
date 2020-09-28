using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ViewModels.Cards
{
    public sealed class ShowAllItemsCallbackComponent : MonoBehaviour, IPointerClickHandler, INotifyPropertyChanged
    {
        public event Action ShowAllItemsClicked;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnShowAllItemsClicked();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnShowAllItemsClicked()
        {
            ShowAllItemsClicked?.Invoke();
        }
    }
}