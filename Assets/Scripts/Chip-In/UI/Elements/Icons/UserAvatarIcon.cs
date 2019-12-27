using Repositories;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Elements.Icons
{
    public enum IconEllipseType
    {
        Golden,
        Silver
    }

    public class UserAvatarIcon : UIBehaviour
    {
        [SerializeField] private Image background, avatar, avatarEllipse;
        [SerializeField] private IconEllipsesRepository eliEllipsesRepository;

        private RectTransform _avatarRectTransform;

        public RectTransform AvatarRectTransform => _avatarRectTransform;

        public void Initialize()
        {
            _avatarRectTransform = GetComponent<RectTransform>();
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

        private static Vector3 CreateScaleVectorFromSingleValue(float singleScale)
        {
            return new Vector3(singleScale, singleScale, 1f);
        }

        public void SetScale(float scale)
        {
            _avatarRectTransform.localScale = CreateScaleVectorFromSingleValue(scale);
        }

        public void SetAvatarImageSprite(Sprite sprite)
        {
            avatar.sprite = sprite;
        }

        private static void SetImageScale(Graphic image, float singleScale)
        {
            image.rectTransform.localScale = CreateScaleVectorFromSingleValue(singleScale);
        }
    }
}