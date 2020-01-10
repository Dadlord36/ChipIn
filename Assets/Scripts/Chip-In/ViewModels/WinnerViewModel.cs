using Repositories;
using Repositories.Remote;
using UnityEngine;
using Views;

namespace ViewModels
{
    public class WinnerViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private UserProfileRemoteRepository userProfileRemoteRepository;

        protected override void OnEnable()
        {
            base.OnEnable();
            var winnerView =(WinnerView) View;
            winnerView.MainAvatarIconSprite = GetUserAvatarSprite();
            winnerView.UserNameFieldText = userProfileRemoteRepository.Name;
        }

        private Sprite GetUserAvatarSprite()
        {
            var texture = userProfileRemoteRepository.AvatarImage;
            return Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero);
        }
    }
}