using UnityEditor;
using Views;

namespace Inspectors
{
    [CustomEditor(typeof(GridElementsView))]
    public class CommunityInterestGridViewEditor : Editor
    {
        private GridElementsView _gridElementsView;
        private bool _started;

        private void OnEnable()
        {
            _gridElementsView = (GridElementsView) target;
            _started = true;
        }

        public override void OnInspectorGUI()
        {
            if(!_started) return;
            
            EditorGUI.BeginChangeCheck();
            _gridElementsView.rowsAmount = EditorGUILayout.IntField(_gridElementsView.rowsAmount);
            
            if (EditorGUI.EndChangeCheck())
            {
                _gridElementsView.AddEmptyItemsRows((uint) _gridElementsView.rowsAmount);
            }
            
            base.OnInspectorGUI();
        }
    }
}