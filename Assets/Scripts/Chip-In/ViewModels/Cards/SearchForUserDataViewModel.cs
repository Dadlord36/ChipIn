using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Behaviours;
using Controllers;
using JetBrains.Annotations;
using Repositories.Remote.Paginated;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class SearchForUserDataViewModel : AsyncOperationsMonoBehaviour, IClearable, INotifyPropertyChanged
    {
        [SerializeField] private UsersDataPaginatedListRepository usersDataPaginatedListRepository;
        [SerializeField] private UsersDataLabelsPaginatedListAdapter usersDataLabelsPaginatedListAdapter;

        private string _inputText;

        [Binding]
        public string InputText
        {
            get => _inputText;
            set
            {
                if(value == _inputText) return;
                _inputText = value;
                OnPropertyChanged();
                try
                {
                    if (string.IsNullOrEmpty(_inputText)) return;
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
            usersDataPaginatedListRepository.UserName = value;
            await usersDataLabelsPaginatedListAdapter.ResetAsync().ConfigureAwait(false);
        }

        public void Clear()
        {
            _inputText = string.Empty;
            usersDataLabelsPaginatedListAdapter.ClearRemainListItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}