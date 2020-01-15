using UnityEditor;
using Views;

namespace Inspectors
{
    [CustomEditor(typeof(CommunityInterestGridView))]
    public class CommunityInterestGridViewEditor : Editor
    {
        private CommunityInterestGridView _gridView;
        private bool _started;

        private void OnEnable()
        {
            _gridView = (CommunityInterestGridView) target;
            _started = true;
        }

        public override void OnInspectorGUI()
        {
            if(!_started) return;
            
            EditorGUI.BeginChangeCheck();
            _gridView.rowsAmount = EditorGUILayout.IntField(_gridView.rowsAmount);
            
            if (EditorGUI.EndChangeCheck())
            {
                _gridView.AddEmptyItemsRows((uint) _gridView.rowsAmount);
            }
            
            base.OnInspectorGUI();
        }
    }
}