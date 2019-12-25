using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ImagesRoll : UIBehaviour
    {
        
        
#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            
            if (Application.isPlaying) return;
        }
#endif
    }
    
}