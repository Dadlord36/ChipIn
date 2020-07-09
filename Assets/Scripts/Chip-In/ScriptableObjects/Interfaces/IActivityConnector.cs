using System;

namespace ScriptableObjects.Interfaces
{
    public interface IActivityConnector
    {
        event Action Activating;
        event Action Deactivating;
        void OnActivating();
        void OnDeactivating();
    }
}