using Tasking;
using UnityEngine;
using Views.Cards;

namespace ScriptableObjects.CardsControllers
{
    public interface IAlertCardController
    {
        void ShowAlertWithText(string textToShow);
        void SetCardViewToControl(AlertsCardView cardView);
    }

    [CreateAssetMenu(fileName = nameof(AlertCardController), menuName = nameof(CardsControllers) + "/" + nameof(AlertCardController), order = 0)]
    public class AlertCardController : BaseCardController<AlertsCardView>, IAlertCardController
    {
        public void ShowAlertWithText(string textToShow)
        {
            TasksFactories.ExecuteOnMainThread(delegate { CardView.ShowUpAndFadeOut(textToShow); });
        }
    }
}