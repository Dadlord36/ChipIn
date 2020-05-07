using TMPro;
using UnityEngine;
using UnityWeld.Binding;

namespace Views.ViewElements
{
    [Binding]
    public class UnsignedNumberView : MonoBehaviour
    {
        [SerializeField] private TMP_Text numberTextField;

        [Binding]
        public uint NumberValue
        {
            get => uint.Parse(numberTextField.text);
            set => numberTextField.text = value.ToString();
        }

        [Binding]
        public int SignedNumberValue
        {
            get => (int) NumberValue;
            set => NumberValue = (uint) value;
        }
    }
}