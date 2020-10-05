using System;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ISyncData
    {
        Task LoadDataFromServer();
        event Action DataWasLoaded;
        event Action DataWasSaved;
    }
}