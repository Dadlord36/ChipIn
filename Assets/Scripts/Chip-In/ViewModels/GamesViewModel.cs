using Repositories;
using UnityEngine;
using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding]
    public class GamesViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private SoloGameItemParametersRepository itemParametersRepository;

        public struct SoloGameParameters
        {
            public string GameTypeName;
        }

        public void ProcessGameLaunch(SoloGameParameters parameters)
        {
        }

        public void AddSoloGameItem(SoloGameParameters soloGameParameters)
        {
            var gameView = (GamesView) View;
            var soloGameItem = gameView.AddSoloGameItem();

            var gameItemVisibleParameters =
                itemParametersRepository.GetItemVisibleParameters(soloGameParameters.GameTypeName);

            soloGameItem.GameTypeIcon = gameItemVisibleParameters.gameTypeSprite;
        }
    }
}