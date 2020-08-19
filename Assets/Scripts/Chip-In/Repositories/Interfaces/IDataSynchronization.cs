using System;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IDataSynchronization
    {
        event Action DataWasLoaded;

        Task LoadDataFromServer();
    }
}