using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Repositories.Interfaces;
using UnityEngine;

namespace Repositories.Remote
{
    public abstract class RemoteRepositoryBase : ScriptableObject, IDataSynchronization
    {
        #region EventsDeclaration
        public event Action DataWasLoaded;
        public event Action DataWasSaved;
        #endregion
        
        protected virtual void ConfirmDataLoading()
        {
            OnDataWasLoaded();
        }

        protected virtual void ConfirmDataSaved()
        {
            OnDataWasSaved();
        }

        public abstract Task LoadDataFromServer();
        public abstract Task SaveDataToServer();
        
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