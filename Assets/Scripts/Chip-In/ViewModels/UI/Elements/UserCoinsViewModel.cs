using System.ComponentModel;
using Repositories.Remote;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.UI.Elements
{
    [Binding]
    public sealed class UserCoinsViewModel : BaseViewModel, INotifyPropertyChanged
    {
        [SerializeField] private UserCoinsAmountRepository userCoinsAmountRepository;
        
        [Binding]
        public uint CoinsAmount => userCoinsAmountRepository.CoinsAmount;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add => userCoinsAmountRepository.PropertyChanged += value;
            remove => userCoinsAmountRepository.PropertyChanged -= value;
        }


    }
}