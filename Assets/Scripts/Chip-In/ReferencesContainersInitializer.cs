using Controllers;
using Factories;
using Repositories.Local;
using Repositories.Remote;
using ScriptableObjects.CardsControllers;
using UnityEngine;

public class ReferencesContainersInitializer : MonoBehaviour
{
    [SerializeField] private SessionController sessionController;
    [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
    [SerializeField] private AlertCardController alertCardController;
    [SerializeField] private UserAuthorisationDataRepository userAuthorisationDataRepository;
    
    private void Start()
    {
        SimpleAutofac.AddObjectInstance(sessionController);
        SimpleAutofac.AddObjectInstance(downloadedSpritesRepository);
        SimpleAutofac.AddObjectInstance(alertCardController);
        SimpleAutofac.AddObjectInstance(userAuthorisationDataRepository);
        SimpleAutofac.AddObjectInstance<LastViewedInterestsRepository>();
        
        Destroy(this);
    }
}