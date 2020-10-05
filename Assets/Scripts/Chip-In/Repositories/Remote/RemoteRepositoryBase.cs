using System;
using System.Threading.Tasks;
using Common.AsyncTasksManagement;
using DataModels.HttpRequestsHeadersModels;
using Factories.ReferencesContainers;
using Repositories.Local;

namespace Repositories.Remote
{
    public interface IRemoteRepositoryBase
    {
        event Action DataWasLoaded;
        event Action DataWasSaved;
        Task LoadDataFromServer();
        bool DataIsLoaded { get; }
        void OnDataWasLoaded();
        void OnDataWasSaved();
    }

    public abstract class RemoteRepositoryBase : AsyncOperationsBase, IRemoteRepositoryBase
    {
        #region EventsDeclaration

        public event Action DataWasLoaded;
        public event Action DataWasSaved;

        #endregion

        protected readonly string Tag;

        public bool DataIsLoaded { get; private set; }
        protected bool _dataWasLoaded;

        protected static IUserAuthorisationDataRepository AuthorisationDataRepository =>
            MainObjectsReferencesContainer.GetObjectInstance<IUserAuthorisationDataRepository>();
        
        protected static IDownloadedSpritesRepository DownloadedSpritesRepository =>
            MainObjectsReferencesContainer.GetObjectInstance<IDownloadedSpritesRepository>();

        public RemoteRepositoryBase(string tag)
        {
            Tag = tag;
        }

        protected virtual void ConfirmDataLoading()
        {
            DataIsLoaded = true;
            OnDataWasLoaded();
        }

        protected virtual void ConfirmDataSaved()
        {
            OnDataWasSaved();
        }

        public virtual Task LoadDataFromServer()
        {
            return Task.CompletedTask;
        }

        #region EventsInvokation

        public void OnDataWasLoaded()
        {
            DataWasLoaded?.Invoke();
        }

        public void OnDataWasSaved()
        {
            DataWasSaved?.Invoke();
        }

        #endregion
    }
}