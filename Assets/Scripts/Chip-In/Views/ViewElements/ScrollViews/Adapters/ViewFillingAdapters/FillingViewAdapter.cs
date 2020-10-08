using Common;
using Factories;
using Repositories.Local;

namespace Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters
{
    public abstract class FillingViewAdapter<TDataType, TViewConsumableData>
    {
        protected IDownloadedSpritesRepository DownloadedSpritesRepository => SimpleAutofac.GetInstance<IDownloadedSpritesRepository>();

        public abstract TViewConsumableData Convert(DisposableCancellationTokenSource cancellationTokenSource,
            TDataType data, uint dataIndexInRepository);
    }
}