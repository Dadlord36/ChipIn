using System.Threading.Tasks;

namespace Views.ViewElements.Interfaces
{
    public interface IFillingView<in TDataModel> where TDataModel : class
    {
        Task FillView(TDataModel data, uint dataBaseIndex);
    }
}