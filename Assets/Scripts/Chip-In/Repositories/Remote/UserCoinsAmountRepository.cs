using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace Repositories.Remote
{
    public interface IUserCoinsAmount
    {
        uint CoinsAmount { get; set; }
    }

    [CreateAssetMenu(fileName = nameof(UserCoinsAmountRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(UserCoinsAmountRepository), order = 0)]
    public sealed class UserCoinsAmountRepository : ScriptableObject, INotifyPropertyChanged, IUserCoinsAmount
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<uint> AmountChanged;
        
        private uint _amount;
        public uint CoinsAmount
        {
            get => _amount;
            set
            {
                if (value == _amount) return;
                _amount = value;
                OnPropertyChanged();
                OnAmountChanged();
            }
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