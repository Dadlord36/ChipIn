using UI;
using UnityEngine;

namespace Views
{
    public class WinnerView : BaseView
    {
        [SerializeField] private ImagesRoll imagesRoll;

        public void SetMainAvatarIconSprite(Sprite sprite)
        {
            imagesRoll.SetMainAvatarIconSprite(sprite);
        }
    }
}