using DataModels.MatchModels;
using Repositories.Local;
using UnityEngine;
using UnityEngine.UI;

namespace ViewModels.UI.Elements.Icons
{
    public class UserAvatarIcon : BaseIconView
    {
        [SerializeField] private Image avatarEllipse;
        [SerializeField] private IconEllipsesRepository eliEllipsesRepository;

        public RectTransform AvatarRectTransform { get; private set; }

        public Sprite AvatarSprite
        {
            get => IconSprite;
            set => IconSprite = value;
        }

        public void Initialize()
        {
            AvatarRectTransform = GetComponent<RectTransform>();
        }

        public void SetIconEllipseSprite(IconEllipseType ellipseType)
        {
            var iconData = eliEllipsesRepository.GetEllipse(ellipseType);
            SetIconEllipseSprite(iconData.sprite, iconData.scale);
        }

        public void SetIconEllipseSprite(Sprite newEllipseSprite, float scale)
        {
            avatarEllipse.sprite = newEllipseSprite;
            SetImageScale(avatarEllipse, scale);
        }

        public void SetAvatarSprite(MatchUserData userData)
        {
            AvatarSprite = userData.AvatarSprite;
        }

        private static Vector3 CreateScaleVectorFromSingleValue(float singleScale)
        {
            return new Vector3(singleScale, singleScale, 1f);
        }

        public void SetScale(float scale)
        {
            AvatarRectTransform.localScale = CreateScaleVectorFromSingleValue(scale);
        }

        private static void SetImageScale(Graphic image, float singleScale)
        {
            image.rectTransform.localScale = CreateScaleVectorFromSingleValue(singleScale);
        }
    }
}