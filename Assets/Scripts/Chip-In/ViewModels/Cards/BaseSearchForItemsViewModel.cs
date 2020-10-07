using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Behaviours;
using Controllers;
using JetBrains.Annotations;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels.Cards
{
    [Binding]
    public abstract class BaseSearchForItemsViewModel : AsyncOperationsMonoBehaviour, IClearable, INotifyPropertyChanged
    {
        private readonly string Tag; 
        
        private string _inputText;
        private readonly TimeSpan _delayTime = TimeSpan.FromSeconds(1);
        private readonly AsyncOperationCancellationController _asyncOperationCancellationController = new AsyncOperationCancellationController();

        public BaseSearchForItemsViewModel()
        {
            Tag = GetType().Name;
        }

        [Binding]
        public string InputText
        {
            get => _inputText;
            set
            {
                if (value == _inputText) return;
                _inputText = value;
                OnPropertyChanged();
                try
                {
                    if (string.IsNullOrEmpty(_inputText)) return;
                    _asyncOperationCancellationController.CancelOngoingTask();
                    RestartDelayedRefillingAsync(_inputText, _asyncOperationCancellationController.CancellationToken);
                }
                catch (Exception e)
                {
                    LogUtility.PrintLogException(e);
                    throw;
                }
            }
        }

        private async void RestartDelayedRefillingAsync(string userName, CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(_delayTime, cancellationToken).ConfigureAwait(false);
                await RefillListViewAsync(userName).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        protected abstract Task RefillListViewAsync(string nameToSearch);

        public virtual void Clear()
        {
            InputText = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}