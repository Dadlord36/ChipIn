using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Views.InteractiveWindows
{
    public sealed class StepperView : BaseView, INotifyPropertyChanged
    {
        private int _number;

        public int Number
        {
            get => _number;
            set
            {
                if(_number==value) return;
                _number = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}