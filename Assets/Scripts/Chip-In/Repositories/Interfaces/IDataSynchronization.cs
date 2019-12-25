using System;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IDataSynchronization
    {
        event Action DataWasLoaded;
        event Action DataWasSaved;

        Task LoadDataFromServer();
        Task SaveDataToServer();
    }
}