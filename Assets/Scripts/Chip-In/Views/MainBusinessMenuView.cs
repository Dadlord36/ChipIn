using TMPro;
using UnityEngine;

namespace Views
{
    public class MainBusinessMenuView : BaseView
    {
        [SerializeField] private TMP_InputField offerTitleInputField;
        [SerializeField] private TMP_InputField gameStartTimeDelayInputField;
        
        public string OfferTitle
        {
            get => offerTitleInputField.text;
            set => offerTitleInputField.text = value;
        }

        public int GameStartingTimeDelay
        {
            get => int.Parse(gameStartTimeDelayInputField.text);
            set => gameStartTimeDelayInputField.text = value.ToString();
        }
    }
}