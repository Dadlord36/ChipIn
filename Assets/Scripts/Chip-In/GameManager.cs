using ActionsTranslators;
using Controllers;
using HttpRequests;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SessionController sessionController;
    [SerializeField] private MainInputActionsTranslator inputActionsTranslator;

    private IUpdatable _updatable;

    // Start is called before the first frame update

    private void Start()
    {
        _updatable = inputActionsTranslator;
        ApiHelper.InitializeClient();
        sessionController.ProcessAppLaunching();
    }

    private void OnApplicationQuit()
    {
        ApiHelper.Dispose();
    }

    private void Update()
    {
        _updatable.Update();
    }
}