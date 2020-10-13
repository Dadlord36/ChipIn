using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Tasking;
using UnityWeld.Binding;

namespace ViewModels.Cards
{
    [Binding]
    public abstract class SwitchableForm<TDataType> : SelectableListItemBase<TDataType>, INotifyPropertyChanged where TDataType : class
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

        protected SwitchableForm(string childClassName) : base(childClassName)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}