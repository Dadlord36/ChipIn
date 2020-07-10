using System;
using UnityEngine;
using Object = UnityEngine.Object;
using RectTransformUtility = Utilities.RectTransformUtility;

namespace Controllers
{
    public class AppModesSwitcher : MonoBehaviour
    {
        [SerializeField] private SessionController sessionController;

        [SerializeField] private Transform bottomBarContainer;

        [Space(10)] [SerializeField] private Object userAppBottomBarPrefab;
        [SerializeField] private Object merchantAppBottomBarPrefab;
        [SerializeField] private Object guestBottomBarPrefab;

        private void OnEnable()
        {
            sessionController.SwitchingToMode += SessionControllerOnSwitchingToMode;
        }

        private void OnDisable()
        {
            sessionController.SwitchingToMode -= SessionControllerOnSwitchingToMode;
        }

        private void SessionControllerOnSwitchingToMode(SessionController.SessionMode mode)
        {
            switch (mode)
            {
                case SessionController.SessionMode.Guest:
                    SwitchToGuestMode();
                    break;
                case SessionController.SessionMode.User:
                    SwitchToUserMode();
                    break;
                case SessionController.SessionMode.Merchant:
                    SwitchToMerchantMode();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        private void SwitchToGuestMode()
        {
            ClearContainer();
            PlaceBarInstance(guestBottomBarPrefab);
        }

        private void SwitchToUserMode()
        {
            ClearContainer();
            PlaceBarInstance(userAppBottomBarPrefab);
        }

        private void SwitchToMerchantMode()
        {
            ClearContainer();
            PlaceBarInstance(merchantAppBottomBarPrefab);
        }

        private void PlaceBarInstance(Object barPrefab)
        {
            var instance =  Instantiate(barPrefab, bottomBarContainer);
            var rectTransform = (instance as GameObject)?.GetComponent<RectTransform>();
            RectTransformUtility.Stretch(rectTransform);
            RectTransformUtility.ResetSize(rectTransform);
        }

        private void ClearContainer()
        {
            if (bottomBarContainer.childCount > 0)
                Destroy(bottomBarContainer.GetChild(0).gameObject);
        }
    }
}