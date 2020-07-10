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
            public override NameAndNumberSelectableFieldFillingData Convert(InterestQuestionAnswer data, uint dataIndexInRepository)
            {
                //TODO: Implement correct click response data retrieving. 
                return new NameAndNumberSelectableFieldFillingData(data.Answer, dataIndexInRepository, data.Percent);
            }
        }
    }
}