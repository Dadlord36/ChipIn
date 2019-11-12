using Behaviours;
using HttpRequests;
using ScriptableObjects.ActionsConnectors;
using UnityEngine;
using ViewModels;
using Views;

public class GameManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Awake()
    {
        ApiHelper.InitializeClient();
    }

    private void Start()
    {
        ViewsCreator.PlaceInPreviousContainer<WelcomeView>();
    }

    private void OnDisable()
    {
        ApiHelper.Dispose();
    }
}