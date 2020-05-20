using CustomAnimators;
using UnityEditor;

namespace Inspectors
{
    [CustomEditor(typeof(SimpleImageAnimator))]
    public class SimpleImageAnimatorEditor : Editor
    {
        private SimpleImageAnimator _animator;

        private int AnimationSequenceIndex
        {
            get => _animator.SpriteIndex;
            set => _animator.SpriteIndex = value;
        }

        private int _value;
        
        private void OnEnable()
        {
            _animator = target as SimpleImageAnimator;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            _value = EditorGUILayout.IntField("Animation sprite index", _value);
            if (EditorGUI.EndChangeCheck())
            {
                AnimationSequenceIndex = _value;
            }
            base.OnInspectorGUI();
        }
    }
}