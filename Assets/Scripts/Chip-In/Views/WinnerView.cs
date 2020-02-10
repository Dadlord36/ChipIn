using System.Collections.Generic;
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

        public Sprite MainAvatarIconSprite
        {
            set => imagesRoll.SetMainAvatarIconSprite(value);
        }

        public void SetOtherAvatarsSprites(IReadOnlyList<Sprite> avatarSprites)
        {
            imagesRoll.SetOtherAvatarsIconSprites(avatarSprites);
        }
    }
}