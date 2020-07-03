using UnityEngine;
using Views.Cards;

namespace ScriptableObjects.CardsControllers
{
    [CreateAssetMenu(fileName = nameof(AlertCardController), menuName = nameof(CardsControllers) + "/" +
                                                                        nameof(AlertCardController), order = 0)]
    public class AlertCardController : BaseCardController<AlertsCardView>
    {
        public void ShowAlertWithText(string textToShow)
        {
            CardView.ShowUpAndFadeOut(textToShow);
        }
    }
}