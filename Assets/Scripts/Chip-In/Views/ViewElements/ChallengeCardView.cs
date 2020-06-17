﻿using Repositories.Local;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.ViewElements
{
    public sealed class ChallengeCardView : BaseView
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

        public ChallengeCardView() : base(nameof(ChallengeCardView))
        {
        }

        public void SetupCardViewElements(in ChallengesCardsParametersRepository.ChallengeCardParameters parameters)
        {
            challengeTypeTextField.text = parameters.challengeTypeName;
            CoinsAmount = parameters.coinsAmount;
            Icon = parameters.icon;
        }
    }
}