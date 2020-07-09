using System;
using Repositories.Local;
using UnityWeld.Binding;
using ViewModels.Basic;
using Views.ViewElements;

namespace ViewModels
{
    [Binding]
    public sealed class ChallengeCardViewModel : BaseViewModel
    {
        public event Action<uint> ConeButtonClicked;

        public ChallengeCardViewModel() : base(nameof(ChallengeCardViewModel))
        {
        }
        
        [Binding]
        public void CloneButton_Click()
        {
            OnConeButtonClicked();
        }

        private void OnConeButtonClicked()
        {
            ConeButtonClicked?.Invoke(((ChallengeCardView) View).CoinsAmount);
        }

        public void SetCardViewElements(ChallengesCardsParametersRepository.ChallengeCardParameters parameters)
        {
            var cardView = (ChallengeCardView) View;
            cardView.SetupCardViewElements(parameters);
        }
    }
}