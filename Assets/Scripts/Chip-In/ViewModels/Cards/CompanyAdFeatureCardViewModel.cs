using System.ComponentModel;
using System.Runtime.CompilerServices;
using Controllers;
using DataModels;
using DataModels.Extensions;
using DataModels.Interfaces;
using JetBrains.Annotations;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class CompanyAdFeatureCardViewModel : MonoBehaviour, INotifyPropertyChanged, IAdvertFeatureBaseModel, IClearable
    {
        private string _description = "Something";
        private uint _tokensAmount;
        private string _icon;

        [SerializeField] private int featureNumber;


        [Binding] public int FeatureNumber => featureNumber;

        [Binding]
        public uint TokensAmount
        {
            get => _tokensAmount;
            set
            {
                if (value == _tokensAmount) return;
                _tokensAmount = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Icon
        {
            get => _icon;
            set
            {
                if (value == _icon) return;
                _icon = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Description
        {
            get => _description;
            set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged();
            }
        }


        public void Clear()
        {
            this.Set(new AdvertFeatureDataModel {Description = "Something"});
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}