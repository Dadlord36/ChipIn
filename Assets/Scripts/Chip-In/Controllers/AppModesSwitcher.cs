using System;
using UnityEngine;
using Object = UnityEngine.Object;

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
            Instantiate(guestBottomBarPrefab, bottomBarContainer);
        }

        private void SwitchToUserMode()
        {
            ClearContainer();
            Instantiate(userAppBottomBarPrefab, bottomBarContainer);
        }

        private void SwitchToMerchantMode()
        {
            ClearContainer();
            Instantiate(merchantAppBottomBarPrefab, bottomBarContainer);
        }

        private void ClearContainer()
        {
            if (bottomBarContainer.childCount > 0)
                Destroy(bottomBarContainer.GetChild(0).gameObject);
        }
    }
}