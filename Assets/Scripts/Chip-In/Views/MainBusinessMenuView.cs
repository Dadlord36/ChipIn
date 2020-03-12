using TMPro;
using UnityEngine;

namespace Views
{
    public class MainBusinessMenuView : BaseView
    {
        [SerializeField] private TMP_InputField offerTitleInputField;
        
        public string OfferTitle
        {
            get => offerTitleInputField.text;
            set => offerTitleInputField.text = value;
        }
    }
}