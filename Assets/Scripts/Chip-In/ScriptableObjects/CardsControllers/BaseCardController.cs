using UnityEngine;
using Views;

namespace ScriptableObjects.CardsControllers
{
    public abstract class BaseCardController<T> : ScriptableObject where T : BaseView
    {
        protected T CardView;

        public void SetCardViewToControl(T cardView)
        {
            CardView = cardView;
        }
    }
}