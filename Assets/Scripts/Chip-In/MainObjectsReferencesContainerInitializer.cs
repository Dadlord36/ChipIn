using Controllers;
using Factories.ReferencesContainers;
using Repositories.Local;
using Repositories.Remote;
using UnityEngine;

public class MainObjectsReferencesContainerInitializer : MonoBehaviour
{
    [SerializeField] private SessionController sessionController;
    [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
    [SerializeField] private UserAuthorisationDataRepository userAuthorisationDataRepository;
    
    private void Awake()
    {
        MainObjectsReferencesContainer.AddObjectInstance(sessionController);
        MainObjectsReferencesContainer.AddObjectInstance(downloadedSpritesRepository);
        MainObjectsReferencesContainer.AddObjectInstance(userAuthorisationDataRepository);
        
        Destroy(this);
    }
}