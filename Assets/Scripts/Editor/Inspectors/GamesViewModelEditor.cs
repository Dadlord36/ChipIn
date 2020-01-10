using UnityEditor;
using ViewModels;

namespace Inspectors
{
    [CustomEditor(typeof(GamesViewModel))]
    public class GamesViewModelEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            base.OnInspectorGUI();
        }
    }
}