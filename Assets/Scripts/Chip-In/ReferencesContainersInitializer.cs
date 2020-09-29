using Controllers;
using Factories;
using Repositories.Local;
using UnityEngine;

public class ReferencesContainersInitializer : MonoBehaviour
{
    [SerializeField] private SessionController sessionController;
    [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
    
    private void Start()
    {
        SimpleAutofac.AddObjectInstance(sessionController);
        SimpleAutofac.AddObjectInstance(downloadedSpritesRepository);
    }
}