using HttpRequests;
using UnityEngine;
using ViewModels.SwitchingControllers;
using Views;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BaseViewSwitchingController mainViewsSwitchingController;
    // Start is called before the first frame update
    void Awake()
    {
        ApiHelper.InitializeClient();
    }

    private void Start()
    {
        mainViewsSwitchingController.RequestSwitchToView(null,nameof(WelcomeView));
    }

    private void OnDisable()
    {
        ApiHelper.Dispose();
    }
}