using System.ComponentModel;
using System.Runtime.CompilerServices;
using Controllers;
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
    public sealed class UserCoinsAmountRepository : ScriptableObject, INotifyPropertyChanged, IUserCoinsAmount,
        IClearable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [SerializeField] private UserProfileRemoteRepository userProfileRemoteRepository;
        private uint _amount;
        private int _tempAmount;
        private bool _userDataWasSynchronised;

        
        private int TokensBalance
        {
            get =>  userProfileRemoteRepository.TokensBalance;
            set => userProfileRemoteRepository.TokensBalance =  value;
        }

        public uint CoinsAmount
        {
            get => _amount;
            private set
            {
                if (value == _amount) return;
                _amount = value;
                OnPropertyChanged();
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
            _userDataWasSynchronised = true;
            CoinsAmount = (uint) (TokensBalance += _tempAmount);
        }

        public void Add(uint amount)
        {
            if (_userDataWasSynchronised)
            {
                TokensBalance += (int)amount;
            }
            else
            {
                _tempAmount += (int)amount;
            }
        }

        public void Subtract(uint amount)
        {
            if (_userDataWasSynchronised)
            {
                TokensBalance -= (int)amount;
            }
            else
            {
                _tempAmount -= (int)amount;
            }
        }

        private void SynchronizeDataWithServer()
        {
            userProfileRemoteRepository.TokensBalance = (int) CoinsAmount;
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        

        public void Clear()
        {
            _amount = 0;
        }
    }
}