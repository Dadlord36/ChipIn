using Controllers;
using HttpRequests;
using Repositories.Remote;
using UnityEngine;
using ViewModels.SwitchingControllers;
using Views;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SessionController sessionController;


    // Start is called before the first frame update

    private void Start()
    {
        ApiHelper.InitializeClient();
        sessionController.ProcessAppLaunching();
    }

    private void OnApplicationQuit()
    {
        ApiHelper.Dispose();
    }
}