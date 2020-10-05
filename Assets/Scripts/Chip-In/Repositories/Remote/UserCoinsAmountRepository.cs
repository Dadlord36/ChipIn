using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Controllers;
using Factories.ReferencesContainers;
using JetBrains.Annotations;
using UnityEngine;

namespace Repositories.Remote
{
    public interface IUserCoinsAmount
    {
        uint CoinsAmount { get; }
    }

    [CreateAssetMenu(fileName = nameof(UserCoinsAmountRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(UserCoinsAmountRepository), order = 0)]
    public sealed class UserCoinsAmountRepository : ScriptableObject, INotifyPropertyChanged, IUserCoinsAmount,
        IClearable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IUserProfileRemoteRepository userProfileRemoteRepository => DataRepositoriesReferencesContainer.GetObjectInstance<IUserProfileRemoteRepository>();
        private uint _amount;


        private int TokensBalance => userProfileRemoteRepository.TokensBalance;

        public uint CoinsAmount
        {
            get => _amount;
            private set
            {
                if (value == _amount) return;
                _amount = value;
                OnPropertyChanged();
            }
        }

        private void OnEnable()
        {
            userProfileRemoteRepository.DataWasLoaded += SyncRepositoryDataWithRemote;
        }

        private void OnDisable()
        {
            userProfileRemoteRepository.DataWasLoaded -= SyncRepositoryDataWithRemote;
        }

        private void SyncRepositoryDataWithRemote()
        {
            CoinsAmount = (uint) TokensBalance;
        }

        public Task UpdateRepositoryData()
        {
            return userProfileRemoteRepository.LoadDataFromServer();
        }

        public void Clear()
        {
            _amount = 0;
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}