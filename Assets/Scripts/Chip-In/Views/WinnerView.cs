using System.Collections.Generic;
using System.Threading.Tasks;
using Repositories.Local;
using TMPro;
using UnityEngine;
using ViewModels.UI;

namespace Views
{
    public class WinnerView : BaseView
    {
        [SerializeField] private ImagesRoll imagesRoll;
        [SerializeField] private TMP_Text textField;

        public string UserNameFieldText
        {
            set => textField.text = value;
        }

        public void SetMainAvatarIconSprite(Sprite sprite)
        {
            imagesRoll.SetMainAvatarIconSprite(sprite);
        }

        public Task SetOtherAvatarsSprites(IReadOnlyList<string> avatarSprites)
        {
            return imagesRoll.SetOtherAvatarsIconSprites(avatarSprites);
        }
    }
}