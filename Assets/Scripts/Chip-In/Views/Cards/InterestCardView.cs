using TMPro;
using UI.Elements;
using UI.Elements.Icons;
using UnityEngine;

namespace Views.Cards
{
    public class InterestCardView : BaseView
    {
        [SerializeField] private UserAvatarIcon avatarIcon;
        
        [SerializeField] private TMP_Text cardNameTextField;
        [SerializeField] private TMP_Text cardDescriptionTextField;
        [SerializeField] private TMP_Text authorNameTextField;
        [SerializeField] private TMP_Text daysPassedTextField;
        
        [SerializeField] private InterestCardElementView congratulationsNumber;
        [SerializeField] private InterestCardElementView joiningInNumber;
        [SerializeField] private InterestCardElementView hoursLeftNumber;
        [SerializeField] private InterestCardElementView usersNumber;
        
        [SerializeField] private PercentageView percentageView;


        public Sprite CardIcon
        {
            get => avatarIcon.AvatarSprite;
            set => avatarIcon.AvatarSprite = value;
        }

        public string AuthorName
        {
            get => authorNameTextField.text;
            set => authorNameTextField.text = value;
        }

        public int DaysPassed
        {
            set => daysPassedTextField.text = $"{ value.ToString()} {GetCorrespondingEndText(value)}";
        }

        public string CardName
        {
            get => cardNameTextField.text;
            set => cardNameTextField.text = value;
        }

        public string CardDescription
        {
            get => cardDescriptionTextField.text;
            set => cardDescriptionTextField.text = value;
        }

        public int CongratulationsNumber
        {
            get => congratulationsNumber.Number;
            set => congratulationsNumber.Number = value;
        }

        public int JoiningInNumber
        {
            get => joiningInNumber.Number;
            set => joiningInNumber.Number = value;
        }

        public int HoursLeftNumber
        {
            get => hoursLeftNumber.Number;
            set => hoursLeftNumber.Number = value;
        }

        public int UsersNumber
        {
            get => usersNumber.Number;
            set => usersNumber.Number = value;
        }

        public float Percentage
        {
            get => percentageView.Percentage;
            set => percentageView.Percentage = value;
        }

        private string GetCorrespondingEndText(int days)
        {
            return days == 1 ? "Day" : "Days";
        }
    }
}