namespace Controllers.SlotsSpinningControllers.RecyclerView.Interfaces
{
    public interface IFillingView<in TDataModel> where TDataModel : class
    {
        void FillView(TDataModel dataModel, uint dataBaseIndex);
    }
}