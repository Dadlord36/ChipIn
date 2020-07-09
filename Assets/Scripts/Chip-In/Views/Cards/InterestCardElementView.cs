using TMPro;
using UnityEngine;

namespace Views.Cards
{
    public class InterestCardElementView : BaseView
    {
        [SerializeField] private TMP_Text numberTextField;

        public InterestCardElementView() : base(nameof(InterestCardElementView))
        {
        }

        public int Number
        {
            get => int.Parse(numberTextField.text);
            set => numberTextField.text = value.ToString();
        }
    }
}