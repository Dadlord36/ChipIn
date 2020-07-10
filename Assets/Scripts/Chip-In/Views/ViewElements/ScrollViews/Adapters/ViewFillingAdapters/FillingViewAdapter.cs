namespace Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters
{
    public abstract class FillingViewAdapter<TDataType, TViewConsumableData>
    {
        public abstract TViewConsumableData Convert(TDataType data, uint dataIndexInRepository);
    }
}