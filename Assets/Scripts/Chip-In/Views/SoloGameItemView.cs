using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class SoloGameItemView : BaseView
    {
        [SerializeField] private TMP_Text gameNameTextField;
        [SerializeField] private TMP_Text gameTypeTextField;
        [SerializeField] private Image gameTypeIcon;
        [SerializeField] private Image buttonImage;
        
        
        public string GameName
        {
            get => gameNameTextField.text;
            set => gameNameTextField.text = value;
        }

        public string GameTypeName
        {
            get => gameTypeTextField.text;
            set => gameTypeTextField.text = value;
        }
        
        public Sprite GameTypeIcon
        {
            get => gameTypeIcon.sprite;
            set => gameTypeIcon.sprite = value;
        }

        public bool ButtonImageVisible
        {
            get => buttonImage.color.a > 0f;
            set
            {
                var newColor = buttonImage.color;
                newColor.a = value ? 1f : 0f;
                buttonImage.color = newColor;
            }
        }

        public Vector2 ButtonSize
        {
            get => buttonImage.rectTransform.sizeDelta;
            set => buttonImage.rectTransform.sizeDelta = value;
        }
        
        public Vector2 ButtonImageSize
        {
            get => gameTypeIcon.rectTransform.sizeDelta;
            set => gameTypeIcon.rectTransform.sizeDelta = value;
        }

#if UNITY_EDITOR
        public bool AllUiElementsAreSets => gameNameTextField && gameTypeTextField && gameTypeIcon && buttonImage;
        
        public void RefreshUiElements()
        {
            EditorUtility.SetDirty(gameNameTextField);
            EditorUtility.SetDirty(gameTypeTextField);
            EditorUtility.SetDirty(gameTypeIcon);
            EditorUtility.SetDirty(buttonImage);
        }
#endif
    }
}