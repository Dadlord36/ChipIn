using Behaviours;
using HttpRequests;
using UnityEngine;
using ViewModels;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ViewsCreator viewsReplacer;

    // Start is called before the first frame update
    void Awake()
    {
        ApiHelper.InitializeClient();
    }

    private void Start()
    {
        viewsReplacer.PlaceInPreviousContainer<WelcomeViewModel>();
    }

    private void OnDisable()
    {
        ApiHelper.Dispose();
    }
}