using TMPro;
using UnityEngine;

namespace Views.ViewElements
{
    public class UserCoinsView : BaseView
    {
        [SerializeField] private TMP_Text amountTextField;

        public uint CoinsAmount
        {
            get => uint.Parse(amountTextField.text);
            set => amountTextField.text = value.ToString();
        }
        
    }
}
