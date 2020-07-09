using System;
using ScriptableObjects.Interfaces;
using UnityEngine;

namespace ScriptableObjects.ActionsConnectors
{
    [CreateAssetMenu(fileName = nameof(ActivityConnector), menuName = "ActionsConnectors/" + nameof(ActivityConnector),
        order = 0)]
    public sealed class ActivityConnector : ScriptableObject, IActivityConnector
    {
        public event Action Activating;
        public event Action Deactivating;

        public void OnActivating()
        {
            Activating?.Invoke();
        }

        public void OnDeactivating()
        {
            Deactivating?.Invoke();
        }
    }
}