using Repositories.Local;
using Repositories.Remote;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.ViewElements
{
    public class ChallengeCardView : BaseView, IUserCoinsAmount
    {
        [SerializeField] private TMP_Text challengeTypeTextField;
        [SerializeField] private UserCoinsView userCoinsView;
        [SerializeField] private Image icon;

        public uint CoinsAmount
        {
            get => userCoinsView.CoinsAmount;
            set => userCoinsView.CoinsAmount = value;
        }

        private Sprite Icon
        {
            get => icon.sprite;
            set => icon.sprite = value;
        }

        public void SetupCardViewElements(in ChallengesCardsParametersRepository.ChallengeCardParameters parameters)
        {
            challengeTypeTextField.text = parameters.challengeTypeName;
            userCoinsView.CoinsAmount = parameters.coinsAmount;
            Icon = parameters.icon;
        }
    }
}