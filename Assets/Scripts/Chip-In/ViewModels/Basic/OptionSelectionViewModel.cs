using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityWeld.Binding;

namespace ViewModels.Basic
{
    [Binding]
    public sealed class OptionSelectionViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        private int _selectedIndex;
        private Action<int> _actionSetCorrespondingIndex;

        [Binding]
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
                _actionSetCorrespondingIndex?.Invoke(value);
            }
        }

        public OptionSelectionViewModel() : base(nameof(OptionSelectionViewModel))
        {
        }

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            _actionSetCorrespondingIndex = View.FormTransitionBundle.TransitionData as Action<int>;
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
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}