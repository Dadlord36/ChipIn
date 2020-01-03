using Repositories;
using UI.Elements.Icons;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Inspectors
{
    [CustomEditor(typeof(UserAvatarIcon))]
    public class UserAvatarImageEditor : Editor
    {
        private IconEllipseType _ellipseType;
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

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            _ellipseType = (IconEllipseType) EditorGUILayout.EnumPopup(_ellipseType);
            AvatarImageSprite = (Sprite) EditorGUILayout.ObjectField(AvatarImageSprite, typeof(Sprite), false);
            if (EditorGUI.EndChangeCheck())
            {
                _avatarIcon.SetIconEllipseSprite(_ellipseType);
                SceneView.RepaintAll();
            }
        }
    }
}