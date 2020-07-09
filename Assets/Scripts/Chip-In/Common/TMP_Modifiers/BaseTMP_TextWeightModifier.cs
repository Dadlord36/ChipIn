using TMPro;
using UnityEngine;

namespace Common.TMP_Modifiers {

    [DisallowMultipleComponent]
    public abstract class BaseTMP_TextWeightModifier : MonoBehaviour {
        public abstract FontWeight FieldFontWeight { get; set; }

        private void Start () {
            Destroy (this);
        }
    }
}