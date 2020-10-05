using Factories.ReferencesContainers;
using Repositories.Local;
using Repositories.Remote;
using UnityEngine;

public class DataRepositoriesReferencesContainerInitializer : MonoBehaviour
{
    [SerializeField] private GeoLocationRepository geoLocationRepository;

    private void Start()
    {
        DataRepositoriesReferencesContainer.CreateObjectInstance<UserCoinsAmountRepository>();
        DataRepositoriesReferencesContainer.CreateObjectInstance<UserProfileRemoteRepository>();
        
        DataRepositoriesReferencesContainer.CreateObjectInstance<>();
        
        DataRepositoriesReferencesContainer.AddObjectInstance(geoLocationRepository);
    }
}