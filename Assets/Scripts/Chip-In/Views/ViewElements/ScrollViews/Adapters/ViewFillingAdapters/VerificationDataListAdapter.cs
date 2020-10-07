using Common;
using DataModels;
using Repositories.Remote;
using UnityWeld.Binding;
using ViewModels.Cards;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters
{
    [Binding]
    public class VerificationDataListAdapter : RepositoryBasedListAdapter<MerchantVerificationRepository, VerificationDataModel,
        DefaultFillingViewPageViewHolder<VerificationItemViewModel.FieldFillingData, uint>, VerificationItemViewModel.FieldFillingData, 
        VerificationDataListAdapter.VerificationDataFillingViewAdapter>
    {
        public class VerificationDataFillingViewAdapter : FillingViewAdapter<VerificationDataModel, VerificationItemViewModel.FieldFillingData>
        {
            private const string Tag = nameof(VerificationDataFillingViewAdapter);

            public override VerificationItemViewModel.FieldFillingData Convert(DisposableCancellationTokenSource cancellationTokenSource,
                VerificationDataModel data, uint dataIndexInRepository)
            {
                return new VerificationItemViewModel.FieldFillingData(data);
            }
        }
    }
}