using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityWeld.Binding;

namespace Views.Cards
{
    [Binding]
    public sealed class InterestCardElementViewModel : BaseView, INotifyPropertyChanged
    {
        private int _count;

        [Binding]
        public int Count
        {
            get => _count;
            set
            {
                if (value == _count) return;
                _count = value;
                OnPropertyChanged();
            }
        }

        public InterestCardElementViewModel() : base(nameof(InterestCardElementViewModel))
        {
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}