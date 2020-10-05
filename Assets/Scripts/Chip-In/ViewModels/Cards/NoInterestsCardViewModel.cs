using UnityEngine;
using UnityEngine.Events;
using UnityWeld.Binding;

namespace ViewModels.Cards
{
    [Binding]
    public class NoInterestsCardViewModel : MonoBehaviour
    {
        public UnityEvent buttonClicked;

        [Binding]
        public void CreateInterestButton_OnClick()
        {
            OnButtonClicked();
        }

        private void OnButtonClicked()
        {
            buttonClicked?.Invoke();
        }
    }
}