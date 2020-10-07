using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.Basic
{
    [Binding]
    public abstract class BaseOptionsSelectionViewModel<T> : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        [SerializeField] private bool autoReturnToPreviousView;
        private T _selectedIndex;
        private Action<T> _actionSetCorrespondingIndex;

        [Binding]
        public T SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
                _actionSetCorrespondingIndex?.Invoke(value);
                if(autoReturnToPreviousView)
                    SwitchToPreviousView();
            }
        }

        public BaseOptionsSelectionViewModel(in string name) : base(name)
        {
        }

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            _actionSetCorrespondingIndex = View.FormTransitionBundle.TransitionData as Action<T>;
        }


        [Binding]
        public void ConfirmButton_OnClick()
        {
            SwitchToPreviousView();
        }

        [Binding]
        public void CancelButton_OnClick()
        {
            SwitchToPreviousView();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}