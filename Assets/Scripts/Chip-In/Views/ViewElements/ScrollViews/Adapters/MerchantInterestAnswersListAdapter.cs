using Common;
using DataModels;
using Views.ViewElements.Fields;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;

namespace Views.ViewElements.ScrollViews.Adapters
{
    public class MerchantInterestAnswersListAdapter : NameAndNumberSelectableFieldListAdapter<InterestQuestionAnswer,
        MerchantInterestAnswersListAdapter.MerchantInterestDetailsDataAdapter>
    {
        public class MerchantInterestDetailsDataAdapter : FillingViewAdapter<InterestQuestionAnswer, NameAndNumberSelectableFieldFillingData>
        {
            public override NameAndNumberSelectableFieldFillingData Convert(DisposableCancellationTokenSource cancellationTokenSource,
                InterestQuestionAnswer data, uint dataIndexInRepository)
            {
                return new NameAndNumberSelectableFieldFillingData(data.Answer, dataIndexInRepository, data.Percent);
            }
        }
    }
}