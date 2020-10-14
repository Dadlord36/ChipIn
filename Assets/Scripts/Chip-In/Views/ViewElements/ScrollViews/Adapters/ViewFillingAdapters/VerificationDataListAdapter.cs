using DataModels;
using Repositories.Remote;
using UnityWeld.Binding;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;

namespace Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters
{
    [Binding]
    public class VerificationDataListAdapter : RepositoryBasedListAdapter<MerchantVerificationRepository, VerificationDataModel>
    {
    }
}