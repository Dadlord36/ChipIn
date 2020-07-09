using UnityEditor;
using UnityEngine;
using ViewModels.UI.Elements.Icons;

namespace Inspectors
{
    [CustomEditor(typeof(UserAvatarIcon))]
    public class UserAvatarImageEditor : Editor
    {
        // private IconEllipseType _ellipseType;
        private UserAvatarIcon _avatarIcon;


        private Sprite AvatarImageSprite
        {
            get => _avatarIcon.AvatarSprite;
            set => _avatarIcon.AvatarSprite = value;
        }

        private void OnEnable()
        {
            _avatarIcon = (UserAvatarIcon) target;
        }

        private int _selectedIndex;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUI.BeginChangeCheck();

            _selectedIndex = EditorGUILayout.Popup("Options", _selectedIndex,
                _avatarIcon.UsedEllipsesRepository.ElementsNames);

            AvatarImageSprite = (Sprite) EditorGUILayout.ObjectField(AvatarImageSprite, typeof(Sprite), false);
            if (EditorGUI.EndChangeCheck())
            {
                _avatarIcon.SetIconEllipseSpriteFromItsIndex(_selectedIndex);
                EditorUtility.SetDirty(target);
            }
        }
    }
}