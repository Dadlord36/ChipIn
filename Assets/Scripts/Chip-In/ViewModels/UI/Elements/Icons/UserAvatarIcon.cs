using Repositories.Local;
using UnityEngine;
using UnityEngine.UI;

namespace ViewModels.UI.Elements.Icons
{
    public interface ISettableSprite
    {
        Sprite AvatarSprite { get; set; }
        void SetAvatarSprite(Sprite sprite);
    }
    
    public class UserAvatarIcon : BaseIconView, ISettableSprite
    {
        [SerializeField] private Image avatarEllipse;
        [SerializeField] private IconEllipsesRepository eliEllipsesRepository;

        public RectTransform AvatarRectTransform { get; private set; }
        public IconEllipsesRepository UsedEllipsesRepository => eliEllipsesRepository;

        public Sprite AvatarSprite
        {
            get => IconSprite;
            set => IconSprite = value;
        }

        public void Initialize()
        {
            AvatarRectTransform = GetComponent<RectTransform>();
        }
        
        public void SetIconEllipseSpriteFromItsIndex(int index)
        {
            SetIconEllipseSprite(eliEllipsesRepository[index]);
        }

        public void SetIconEllipseSprite(in string ellipseName)
        {
            SetIconEllipseSprite(eliEllipsesRepository.GetEllipse(ellipseName));
        }
        
        public void SetIconEllipseSprite(in IconEllipseData ellipseData)
        {
            SetIconEllipseSprite(ellipseData.sprite, ellipseData.scale);
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
            AvatarRectTransform.localScale = CreateScaleVectorFromSingleValue(scale);
        }

        private static void SetImageScale(Graphic image, float singleScale)
        {
            image.rectTransform.localScale = CreateScaleVectorFromSingleValue(singleScale);
        }

        public void SetAvatarSprite(Sprite sprite)
        {
            AvatarSprite = sprite;
        }
    }
}