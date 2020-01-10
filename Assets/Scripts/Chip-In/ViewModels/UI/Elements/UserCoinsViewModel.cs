using System.ComponentModel;
using Repositories;
using Repositories.Remote;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.UI.Elements
{
    [Binding]
    public sealed class UserCoinsViewModel : BaseViewModel, INotifyPropertyChanged, IUserCoinsAmount
    {
        [SerializeField] private UserCoinsAmountRepository userCoinsAmountRepository;
        
        [Binding]
        public uint CoinsAmount
        {
            get => userCoinsAmountRepository.CoinsAmount;
            set => userCoinsAmountRepository.CoinsAmount = value;
        }
        
        public event PropertyChangedEventHandler PropertyChanged
        {
            add => userCoinsAmountRepository.PropertyChanged += value;
            remove => userCoinsAmountRepository.PropertyChanged -= value;
        }


    }
}