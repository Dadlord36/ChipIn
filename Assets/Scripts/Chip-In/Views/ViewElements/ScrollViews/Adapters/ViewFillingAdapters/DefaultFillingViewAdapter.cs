using Common;

namespace Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters
{
    public class DefaultFillingViewAdapter<TDataType> : FillingViewAdapter<TDataType, TDataType>
    {
        public override TDataType Convert(DisposableCancellationTokenSource cancellationTokenSource, TDataType data, uint dataIndexInRepository)
        {
            return data;
        }
    }
}