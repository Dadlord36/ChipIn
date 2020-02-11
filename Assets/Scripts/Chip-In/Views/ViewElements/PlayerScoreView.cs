using TMPro;
using UnityEngine;

namespace Views.ViewElements
{
    public class PlayerScoreView : BaseView
    {
        [SerializeField] private TMP_Text scoreNumberTextField;

        public uint Score
        {
            set => scoreNumberTextField.text = value.ToString();
        }
    }
}