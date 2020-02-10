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

        protected override void OnEnable()
        {
            base.OnEnable();
            var winnerView =(WinnerView) View;
            winnerView.MainAvatarIconSprite = slotsGameRepository.GetWinnerUserData().AvatarSprite;
            winnerView.UserNameFieldText = "Winner";
            winnerView.SetOtherAvatarsSprites(slotsGameRepository.UsersAvatarImagesSprites);
        }
    }
}