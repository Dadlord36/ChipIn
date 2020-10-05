using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Repositories.Local;
using Repositories.Local.SingleItem;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects.CardsControllers;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;
using Views.ViewElements;

namespace ViewModels
{
    [Binding]
    public sealed class ChallengeViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        [SerializeField] private ChallengesCardsParametersRepository challengesCardsParametersRepository;
        [SerializeField] private ChallengesRemoteRepository challengesRemoteRepository;
        [SerializeField] private SelectedGameRepository selectedGameRepository;
        [SerializeField] private AlertCardController alertCardController;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private Timer timer;
        private bool _canStartTheGame;
        

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}