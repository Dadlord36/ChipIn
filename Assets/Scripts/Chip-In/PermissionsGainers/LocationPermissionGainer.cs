using System;
using UnityEngine;
using UnityEngine.Android;

namespace PermissionsGainers
{
    public sealed class LocationPermissionGainer : MonoBehaviour
    {
        public event Action RequestAccepted;
        public event Action RequestRejected;

        public void Request()
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (Input.location.isEnabledByUser)
            {
                OnRequestAccepted();
            }
            else
            {
                OnRequestRejected();
            }
            Destroy(gameObject);
        }

        private void OnRequestAccepted()
        {
            RequestAccepted?.Invoke();
        }

        private void OnRequestRejected()
        {
            RequestRejected?.Invoke();
        }
    }
}