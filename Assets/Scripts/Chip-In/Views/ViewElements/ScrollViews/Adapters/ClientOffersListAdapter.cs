using DataModels;
using Repositories.Remote;
using UnityEngine;
using ViewModels.Cards;
using Views.InteractiveWindows;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    public sealed class ClientOffersListAdapter : RepositoryBasedListAdapter<ClientOffersRemoteRepository, ClientOfferDataModel>
    {
        [SerializeField] private TokensDedicationViewModel tokensDedicationViewModel;


        protected override void AdditionItemProcessing(DefaultFillingViewPageViewHolder<ClientOfferDataModel> viewHolder, int itemIndex)
        {
            base.AdditionItemProcessing(viewHolder, itemIndex);
            var offerCard = viewHolder.root.GetComponent<OfferCardViewModel>();
            
            offerCard.BuyButtonPressed += delegate
            {
                tokensDedicationViewModel.NumberAsInt = (int) offerCard.Price;
                tokensDedicationViewModel.gameObject.SetActive(true);
            };

            tokensDedicationViewModel.amountConfirmed.AddListener(_ => { offerCard.OnTokensDedicationConfirmation(); });
        }
    }
}