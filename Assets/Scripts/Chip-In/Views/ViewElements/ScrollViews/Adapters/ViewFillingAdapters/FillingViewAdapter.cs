using Common;
using Repositories.Local;

namespace Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters
{
    public abstract class FillingViewAdapter<TDataType, TViewConsumableData>
    {
        protected DownloadedSpritesRepository DownloadedSpritesRepository;

        public void SetDownloadingSpriteRepository(DownloadedSpritesRepository downloadedSpritesRepository)
        {
            DownloadedSpritesRepository = downloadedSpritesRepository;
        }

        public abstract TViewConsumableData Convert(DisposableCancellationTokenSource cancellationTokenSource,
            TDataType data, uint dataIndexInRepository);
    }
}