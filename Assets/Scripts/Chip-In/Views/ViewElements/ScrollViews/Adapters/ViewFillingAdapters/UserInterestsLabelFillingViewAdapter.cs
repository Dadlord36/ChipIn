using Common;
using DataModels;
using ViewModels.Cards;

namespace Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters
{
    public class UserInterestsLabelFillingViewAdapter : FillingViewAdapter<InterestBasicDataModel, InterestItemViewModel.FieldFillingData>
    {
        public override InterestItemViewModel.FieldFillingData Convert(DisposableCancellationTokenSource cancellationTokenSource, InterestBasicDataModel data,
            uint dataIndexInRepository)
        {
            return new InterestItemViewModel.FieldFillingData(DownloadedSpritesRepository
                .CreateLoadSpriteTask(data.PosterUri, cancellationTokenSource.Token), data.Name, (int) data.Id);
        }
    }
}