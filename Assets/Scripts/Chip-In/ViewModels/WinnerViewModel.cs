using ActionsTranslators;
using Repositories.Local;
using Repositories.Remote;
using UnityEngine;
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

        protected override void OnEnable()
        {
            base.OnEnable();
            var winnerView = (WinnerView) View;
            var iconUrl = slotsGameRepository.GetWinnerUserData().AvatarUrl;

            downloadedSpritesRepository.TryToLoadSpriteAsync(
                new DownloadedSpritesRepository.SpriteDownloadingTaskParameters(iconUrl,
                    winnerView.SetMainAvatarIconSprite));

            winnerView.UserNameFieldText = "Winner";
            winnerView.SetOtherAvatarsSprites(slotsGameRepository.UsersAvatarImagesSprites);
        }
    }
}