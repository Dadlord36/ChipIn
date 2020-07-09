using TMPro;
using UnityEngine;

namespace Views.ViewElements
{
    public sealed class PlayerScoreView : BaseView
    {
        [SerializeField] private TMP_Text scoreNumberTextField;

        public uint Score
        {
            set => scoreNumberTextField.text = value.ToString();
        }

        public PlayerScoreView() : base(nameof(PlayerScoreView))
        {
        }
    }
}