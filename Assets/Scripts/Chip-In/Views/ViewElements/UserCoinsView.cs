using TMPro;
using UnityEngine;

namespace Views.ViewElements
{
    public sealed class UserCoinsView : BaseView
    {
        [SerializeField] private TMP_Text amountTextField;

        public UserCoinsView() : base(nameof(UserCoinsView))
        {
        }

        public uint CoinsAmount
        {
            get => uint.Parse(amountTextField.text);
            set => amountTextField.text = value.ToString();
        }
    }
}