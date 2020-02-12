using Controllers;
using HttpRequests;
using UnityEngine;
using ViewModels.SwitchingControllers;
using Views;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BaseViewSwitchingController mainViewsSwitchingController;
    [SerializeField] private CachingController cachingController;

    // Start is called before the first frame update
    void Awake()
    {
        ApiHelper.InitializeClient();
    }

    private void Start()
    {
        mainViewsSwitchingController.RequestSwitchToView(null,nameof(WelcomeView));
        cachingController.ClearCache();
    }

    private void OnDisable()
    {
        ApiHelper.Dispose();
    }
}