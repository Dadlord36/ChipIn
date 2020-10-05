using System;
using System.Threading.Tasks;
using ActionsTranslators;
using Common.Structures;
using Common.Timers;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using Factories.ReferencesContainers;
using GlobalVariables;
using PermissionsGainers;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects;
using UnityEngine;
using Utilities;

namespace Repositories.Local
{
    public interface IGeoLocationRepository : IUpdatable
    {
        event Action<bool> LocationServiceActivityChanged;
        bool UseGeoLocation { get; }
        void SetLocationServiceActivity(bool activity);
    }

    [CreateAssetMenu(fileName = nameof(GeoLocationRepository), menuName = nameof(Repositories) + "/" + nameof(Local) +
                                                                          "/" + nameof(GeoLocationRepository), order = 0)]
    public sealed class GeoLocationRepository : AsyncOperationsScriptableObject, IGeoLocationRepository
    {
        private const string Tag = nameof(GeoLocationRepository);

        #region Private classes

        private class UserGeoLocationDataModel : IUserGeoLocation
        {
            public GeoLocation UserLocation { get; set; }
        }

        #endregion

        #region Serialized fields

        [SerializeField] private float locationSaveInterval = 20f;

        #endregion

        private bool _shouldUpdateTimer;
        private bool _useGeoLocation;
        private readonly TimerController _timerController = new TimerController();
        private readonly UserGeoLocationDataModel _userGeoLocationData = new UserGeoLocationDataModel();
        private static bool UseOfDeviceLocationServiceIsAllowed => Input.location.isEnabledByUser;
        private static LocationInfo LastLocationData => Input.location.lastData;

        private static IRequestHeaders UserAuthorizationRequestHeaders =>
            MainObjectsReferencesContainer.GetObjectInstance<IUserAuthorisationDataRepository>();

        private static IUserProfileRemoteRepository UserProfileRemoteRepository =>
            DataRepositoriesReferencesContainer.GetObjectInstance<IUserProfileRemoteRepository>();

        private static ISessionStateRepository SessionStateRepository => DataRepositoriesReferencesContainer.GetObjectInstance<ISessionStateRepository>();

        public event Action<bool> LocationServiceActivityChanged;

        public bool UseGeoLocation
        {
            get => _useGeoLocation;
            private set
            {
                if (value == _useGeoLocation) return;
                _useGeoLocation = value;
                OnLocationServiceActivityChanged(value);
            }
        }

        public void SetLocationServiceActivity(bool activity)
        {
            if (activity)
            {
                InitializeLocationService();
            }
            else
            {
                DisableLocationService();
            }
        }

        private void OnUserProfileDataWasLoaded()
        {
            if (UserProfileRemoteRepository.Role == MainNames.UserRoles.BusinessOwner) return;
            if (!UserProfileRemoteRepository.UserRadarState) return;

            if (!UseOfDeviceLocationServiceIsAllowed)
            {
                UserProfileRemoteRepository.UserRadarState = false;
                return;
            }

            SetLocationServiceActivity(UserProfileRemoteRepository.UserRadarState);
        }

        private void InitializeLocationService()
        {
            _timerController.Interval = locationSaveInterval;
            _timerController.Initialize();
            TryToStartLocationService();
        }

        private void OnEnable()
        {
            Activate();
        }

        private void OnDisable()
        {
            Deactivate();
        }

        private void Activate()
        {
            SubscribeOnEvents();
        }

        private void Deactivate()
        {
            UnsubscribeFromEvents();
            DisableLocationService();
        }

        private void SubscribeOnEvents()
        {
            _timerController.Elapsed += OnTimerElapsed;
            SessionStateRepository.SigningOut += DisableLocationService;
            UserProfileRemoteRepository.DataWasLoaded += OnUserProfileDataWasLoaded;
        }

        private void UnsubscribeFromEvents()
        {
            _timerController.Elapsed -= OnTimerElapsed;
            SessionStateRepository.SigningOut -= DisableLocationService;
            UserProfileRemoteRepository.DataWasLoaded -= OnUserProfileDataWasLoaded;
        }


        private void TryToStartLocationService()
        {
            // First, check if user has location service enabled
            if (!UseOfDeviceLocationServiceIsAllowed)
            {
                LogUtility.PrintLog(Tag, "User gas not enabled location service");

#if UNITY_ANDROID
                RequestLocationPermissionOnAndroid();
#endif
                return;
            }

            StartLocationServiceAndTimer();
        }

        private void StartLocationServiceAndTimer()
        {
            Input.location.Start();
            AllowTimerUpdate();
            UseGeoLocation = true;
        }

        private void DisableLocationService()
        {
            PreventTimerUpdate();
            Input.location.Stop();
            UseGeoLocation = false;
        }

        private void OnPermissionGranted()
        {
            LogUtility.PrintLog(Tag, "Location services permission was granted");
            StartLocationServiceAndTimer();
        }

        private void RequestLocationPermissionOnAndroid()
        {
            var gameObject = new GameObject(nameof(LocationPermissionGainer));
            var locationPermissionGainer = gameObject.AddComponent<LocationPermissionGainer>();
            locationPermissionGainer.RequestAccepted += OnPermissionGranted;
            locationPermissionGainer.Request();
        }

        private void AllowTimerUpdate()
        {
            _shouldUpdateTimer = true;
        }

        private void PreventTimerUpdate()
        {
            _shouldUpdateTimer = false;
        }

        private void RestartTimer()
        {
            _timerController.RestartTimer();
            AllowTimerUpdate();
        }

        private async void OnTimerElapsed()
        {
            try
            {
                await UpdateLocationData();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }

            RestartTimer();
        }

        void IUpdatable.Update()
        {
            if (_shouldUpdateTimer)
                _timerController.Update();
        }

        private Task UpdateLocationData()
        {
            var lastLocationData = LastLocationData;
            _userGeoLocationData.UserLocation = new GeoLocation
            {
                latitude = lastLocationData.latitude,
                longitude = lastLocationData.longitude
            };

            return TryToSaveLocationToServer();
        }

        private async Task TryToSaveLocationToServer()
        {
            try
            {
                var result = await ProfileDataStaticRequestsProcessor.UpdateUserPosition(out TasksCancellationTokenSource,
                    UserAuthorizationRequestHeaders, _userGeoLocationData);

                if (result.Success)
                    LogUtility.PrintLog(Tag, $"New  geolocation {_userGeoLocationData.UserLocation} was saved to server");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void OnLocationServiceActivityChanged(bool activityState)
        {
            LocationServiceActivityChanged?.Invoke(activityState);
        }
    }
}