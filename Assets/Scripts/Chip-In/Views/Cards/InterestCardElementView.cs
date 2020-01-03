using TMPro;
using UnityEngine;

namespace Views.Cards
{
    public class InterestCardElementView : BaseView
    {
        [SerializeField] private TMP_Text numberTextField;
        
        public int Number
        {
            get => int.Parse(numberTextField.text);
            set => numberTextField.text = value.ToString();
        }
    }
}