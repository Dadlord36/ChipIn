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

        private void Start()
        {
            sessionController.SwitchingToMode += SessionControllerOnSwitchingToMode;
        }

        private void SessionControllerOnSwitchingToMode(SessionController.SessionMode mode)
        {
            switch (mode)
            {
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