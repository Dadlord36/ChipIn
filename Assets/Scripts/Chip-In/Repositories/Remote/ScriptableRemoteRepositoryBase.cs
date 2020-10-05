using System;
using System.Threading.Tasks;
using ScriptableObjects;

namespace Repositories.Remote
{
    public abstract class ScriptableRemoteRepositoryBase<TRemoteRepository> : AsyncOperationsScriptableObject, IRemoteRepositoryBase 
        where TRemoteRepository : class, IRemoteRepositoryBase, new()
    {
        protected readonly TRemoteRepository RemoteRepository;
        private IRemoteRepositoryBase RemoteRepositoryBaseImplementation => RemoteRepository;
        public bool DataIsLoaded => RemoteRepositoryBaseImplementation.DataIsLoaded;

        public ScriptableRemoteRepositoryBase()
        {
            RemoteRepository = new TRemoteRepository();
        }

        public event Action DataWasLoaded
        {
            add => RemoteRepositoryBaseImplementation.DataWasLoaded += value;
            remove => RemoteRepositoryBaseImplementation.DataWasLoaded -= value;
        }

        public event Action DataWasSaved
        {
            add => RemoteRepositoryBaseImplementation.DataWasSaved += value;
            remove => RemoteRepositoryBaseImplementation.DataWasSaved -= value;
        }

        public Task LoadDataFromServer()
        {
            return RemoteRepositoryBaseImplementation.LoadDataFromServer();
        }

        public void OnDataWasLoaded()
        {
            RemoteRepositoryBaseImplementation.OnDataWasLoaded();
        }

        public void OnDataWasSaved()
        {
            RemoteRepositoryBaseImplementation.OnDataWasSaved();
        }
    }
}