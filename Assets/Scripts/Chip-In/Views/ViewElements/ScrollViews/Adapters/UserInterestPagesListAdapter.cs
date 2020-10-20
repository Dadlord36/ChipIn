using System;
using DataModels;
using Repositories.Remote.Paginated;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Cards;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    [Binding]
    public sealed class UserInterestPagesListAdapter : RepositoryBasedListAdapter<UserInterestPagesPaginatedRepository, UserInterestPageDataModel>
    {
        public event Action<int> OffersButtonClicked;

        protected override void AdditionItemProcessing(DefaultFillingViewPageViewHolder<UserInterestPageDataModel> viewHolder, int itemIndex)
        {
            base.AdditionItemProcessing(viewHolder, itemIndex);
            GameObjectsUtility.GetFromRootOrChildren<InterestCardViewModel>(viewHolder.root).OffersButtonClicked += OnOffersButtonClicked;
        }

        private void OnOffersButtonClicked(int interestId)
        {
            OffersButtonClicked?.Invoke(interestId);
        }
    }
}