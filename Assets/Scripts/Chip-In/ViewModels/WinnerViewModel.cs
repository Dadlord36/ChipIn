using System;
using ActionsTranslators;
using Repositories.Local;
using UnityEngine;
using Utilities;
using Views;

namespace ViewModels
{
    public class WinnerViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
        [SerializeField] private SelectedGameRepository slotsGameRepository;
        [SerializeField] private MainInputActionsTranslator inputActionsTranslator;

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            inputActionsTranslator.EscapeButtonPressed += OnEscapeButtonPressed;
        }

        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
            inputActionsTranslator.EscapeButtonPressed -= OnEscapeButtonPressed;
        }

        private void OnEscapeButtonPressed()
        {
            SwitchToView(nameof(MyChallengeView));
        }

        protected override async void OnEnable()
        {
            base.OnEnable();
            var winnerView = (WinnerView) View;
            var iconUrl = slotsGameRepository.GetWinnerUserData().AvatarUrl;

            try
            {
                await downloadedSpritesRepository.TryToLoadSpriteAsync(new DownloadedSpritesRepository.SpriteDownloadingTaskParameters(iconUrl,
                    winnerView.SetMainAvatarIconSprite));


                winnerView.UserNameFieldText = "Winner";
                await winnerView.SetOtherAvatarsSprites(slotsGameRepository.UsersAvatarImagesSprites);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}