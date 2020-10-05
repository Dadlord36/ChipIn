using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Behaviours;
using JetBrains.Annotations;
using Tasking;
using UnityEngine.Events;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels.Cards
{
    
    
    
    [Binding]
    public abstract class SearchItemsList<TItemsType> : AsyncOperationsMonoBehaviour, INotifyPropertyChanged
    {
        private const string Tag = nameof(SearchItemsList<TItemsType>);

        private uint _selectedItemIndex;
        private string _inputText;

        public UnityEvent SelectedItemIndexChanged;

        [Binding]
        public uint SelectedItemIndex
        {
            get => _selectedItemIndex;
            set
            {
                _selectedItemIndex = value;
                OnPropertyChanged();
                OnSelectedItemIndexChanged();
            }
        }

        [Binding]
        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                try
                {
                    RefillListView(value);
                }
                catch (Exception e)
                {
                    LogUtility.PrintLogException(e);
                    throw;
                }
            }
        }

        private async void RefillListView(string value)
        {
            AsyncOperationCancellationController.CancelOngoingTask();
            try
            {
                AsyncOperationCancellationController.CancelOngoingTask();
                var retrievedItems = await RetrieveItems(value).ConfigureAwait(false);
                if (retrievedItems == null)
                {
                    LogUtility.PrintLog(Tag, "No items were found");
                    return;
                }

                RestItems(retrievedItems);
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

        protected abstract Task<IList<TItemsType>> RetrieveItems(string value);
        protected abstract void RestItems(IList<TItemsType> items);

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnSelectedItemIndexChanged()
        {
            TasksFactories.ExecuteOnMainThread(() => { SelectedItemIndexChanged.Invoke(); });
        }

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}