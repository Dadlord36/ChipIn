using UnityEngine;
using Views.Cards;

namespace ScriptableObjects.CardsControllers
{
    [CreateAssetMenu(fileName = nameof(AlertCardController), menuName = nameof(CardsControllers) + "/" +
                                                                        nameof(AlertCardController), order = 0)]
    public class AlertCardController : ScriptableObject
    {
        private AlertsCardView _alertsCardView;

        public void SetAlertCardViewToControl(AlertsCardView alertsCardView)
        {
            _alertsCardView = alertsCardView;
        }

        public void ShowAlertWithText(string textToShow)
        {
            _alertsCardView.ShowUpAndFadeOut(textToShow);
        }
    }
}