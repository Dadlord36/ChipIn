using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityWeld.Binding;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;

namespace ViewModels
{
    [Binding]
    public sealed class VerificationViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        public VerificationViewModel() : base(nameof(VerificationViewModel))
        {
        }

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();

            foreach (var resettableAsync in GetComponentsInChildren<IResettableAsync>())
            {
                resettableAsync.ResetAsync();
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