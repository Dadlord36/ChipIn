using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace Repositories.Remote
{
    public interface IUserCoinsAmount
    {
        uint CoinsAmount { get; }
        void Add(uint amount);
        void Subtract(uint amount);
    }

    [CreateAssetMenu(fileName = nameof(UserCoinsAmountRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(UserCoinsAmountRepository), order = 0)]
    public sealed class UserCoinsAmountRepository : ScriptableObject, INotifyPropertyChanged, IUserCoinsAmount
    {
        [SerializeField]
        private UserProfileRemoteRepository userProfileRemoteRepository;
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<uint> AmountChanged;
        private uint _amount;

        public uint CoinsAmount
        {
            get => _amount;
            private set
            {
                if (value == _amount) return;
                _amount = value;
                OnPropertyChanged();
                OnAmountChanged();
                SynchronizeDataWithServer();
            }
        }

        private void OnEnable()
        {
            userProfileRemoteRepository.DataWasLoaded += UpdateRepositoryData;
        }

        private void OnDisable()
        {
            userProfileRemoteRepository.DataWasLoaded -= UpdateRepositoryData;
        }

        private void UpdateRepositoryData()
        {
            CoinsAmount = (uint) userProfileRemoteRepository.TokensBalance;
        }

        public void Add(uint amount)
        {
            CoinsAmount += amount;
        }

        public void Subtract(uint amount)
        {
            CoinsAmount -= amount;
        }

        private void SynchronizeDataWithServer()
        {
            userProfileRemoteRepository.TokensBalance = (int)CoinsAmount;
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnAmountChanged()
        {
            AmountChanged?.Invoke(_amount);
        }
    }
}