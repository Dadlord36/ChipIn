using TMPro;
using UnityEngine;

namespace Common.TMP_Modifiers
{
    [RequireComponent(typeof(TMP_Text))]
    public class TMP_TextWeightModifier : BaseTMP_TextWeightModifier
    {
        public override FontWeight FieldFontWeight
        {
            get => GetComponent<TMP_Text>().fontWeight;
            set => GetComponent<TMP_Text>().fontWeight = value;
        }
    }
}