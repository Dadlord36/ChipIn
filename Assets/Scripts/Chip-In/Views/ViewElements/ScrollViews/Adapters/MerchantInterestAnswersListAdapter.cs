using Common;
using DataModels;
using Views.ViewElements.Fields;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;

namespace Views.ViewElements.ScrollViews.Adapters
{
    public class MerchantInterestAnswersListAdapter : NameAndNumberSelectableFieldListAdapter<AnswerData,
        MerchantInterestAnswersListAdapter.MerchantInterestDetailsDataAdapter>
    {
        public class MerchantInterestDetailsDataAdapter : FillingViewAdapter<AnswerData, NameAndNumberSelectableFieldFillingData>
        {
            public override NameAndNumberSelectableFieldFillingData Convert(DisposableCancellationTokenSource cancellationTokenSource,
                AnswerData data, uint dataIndexInRepository)
            {
                return new NameAndNumberSelectableFieldFillingData(data.Answer, (int) dataIndexInRepository, data.Percent);
            }
        }
    }
}