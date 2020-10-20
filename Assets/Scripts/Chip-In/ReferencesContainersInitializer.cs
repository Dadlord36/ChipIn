using Controllers;
using Factories;
using Repositories.Interfaces;
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
    [SerializeField] private ChatBotsRepository chatBotsRepository;
    


    private void Start()
    {
        SetSimpleAutofacReferences();
        SetOtherReferences();
        
        Destroy(this);
    }

    private void SetSimpleAutofacReferences()
    {
        SimpleAutofac.AddObjectInstanceAs<ISessionController>(sessionController);
        SimpleAutofac.AddObjectInstanceAs<IDownloadedSpritesRepository>(downloadedSpritesRepository);
        SimpleAutofac.AddObjectInstanceAs<IAlertCardController>(alertCardController);
        SimpleAutofac.AddObjectInstanceAs<IUserAuthorisationDataRepository>(userAuthorisationDataRepository);
        SimpleAutofac.AddObjectInstanceAs<LastViewedInterestsRepository, ILastViewedInterestsRepository>();
    }

    private void SetOtherReferences()
    {
        SimpleAutofac.AddObjectInstanceAs<IChatBotsRepository>(chatBotsRepository);
    }
    
}