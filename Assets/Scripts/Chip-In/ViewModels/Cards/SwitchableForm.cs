using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.Cards
{
    [Binding]
    public class SwitchableForm : MonoBehaviour,INotifyPropertyChanged
    {
        private bool _alternativeFormUsed;

        [Binding]
        public bool AlternativeFormUsed
        {
            get => _alternativeFormUsed;
            set
            {
                if (value == _alternativeFormUsed) return;
                _alternativeFormUsed = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}