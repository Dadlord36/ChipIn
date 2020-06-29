using TMPro;
using UnityEngine;

namespace Common.TMP_Modifiers
{
    public abstract class BaseTMP_TextWeightModifier : MonoBehaviour
    {
        public abstract FontWeight FieldFontWeight { get; set; }
    }
}