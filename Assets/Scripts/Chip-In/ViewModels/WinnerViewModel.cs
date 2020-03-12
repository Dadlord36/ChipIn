using ActionsTranslators;
using Repositories.Local;
using Repositories.Remote;
using UnityEngine;
using Views;

namespace ViewModels
{
    public class WinnerViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private UserProfileRemoteRepository userProfileRemoteRepository;
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
            winnerView.MainAvatarIconSprite = slotsGameRepository.GetWinnerUserData().AvatarSprite;
            winnerView.UserNameFieldText = "Winner";
            winnerView.SetOtherAvatarsSprites(slotsGameRepository.UsersAvatarImagesSprites);
        }
    }
}