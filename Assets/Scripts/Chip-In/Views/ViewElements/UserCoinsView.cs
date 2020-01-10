using Repositories;
using Repositories.Remote;
using TMPro;
using UnityEngine;

namespace Views.ViewElements
{
    public class UserCoinsView : BaseView, IUserCoinsAmount
    {
        [SerializeField] private TMP_Text amountTextField;

        public uint CoinsAmount
        {
            get => uint.Parse(amountTextField.text);
            set => amountTextField.text = value.ToString();
        }
        
    }
}
