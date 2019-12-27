using Repositories;
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
            ((WinnerView) View).SetMainAvatarIconSprite(GetUserAvatarSprite());
        }

        private Sprite GetUserAvatarSprite()
        {
            var texture = userProfileRemoteRepository.AvatarImage;
            return Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero);
        }
    }
}