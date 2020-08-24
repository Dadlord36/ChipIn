using System.Threading.Tasks;

namespace Controllers.SlotsSpinningControllers.RecyclerView.Interfaces
{
    public interface IFillingView<in TDataModel> where TDataModel : class
    {
        Task FillView(TDataModel data, uint dataBaseIndex);
    }
}