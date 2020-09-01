using System;
using System.Threading.Tasks;
using Behaviours;
using Repositories.Interfaces;

namespace Repositories.Remote
{
    public abstract class RemoteRepositoryBase : AsyncOperationsMonoBehaviour, IDataSynchronization
    {
        protected readonly IUserAuthorisationDataRepository AuthorisationDataRepository;

        public RemoteRepositoryBase(IUserAuthorisationDataRepository authorisationDataRepositoryInterface)
        {
            AuthorisationDataRepository = authorisationDataRepositoryInterface;
        }
        
        #region EventsDeclaration
        public event Action DataWasLoaded;
        public event Action DataWasSaved;
        #endregion

        protected bool _dataWasLoaded;
        
        protected virtual void ConfirmDataLoading()
        {
            _dataWasLoaded = true;
            OnDataWasLoaded();
        }

        protected virtual void ConfirmDataSaved()
        {
            OnDataWasSaved();
        }

        public abstract Task LoadDataFromServer();

        #region EventsInvokation
        private void OnDataWasLoaded()
        {
            DataWasLoaded?.Invoke();
        }

        private  void OnDataWasSaved()
        {
            DataWasSaved?.Invoke();
        }
        #endregion
    }
}