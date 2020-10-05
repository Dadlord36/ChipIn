using Factories.ReferencesContainers;
using Repositories.Local;
using Repositories.Remote;
using UnityEngine;

public class DataRepositoriesReferencesContainerInitializer : MonoBehaviour
{
    [SerializeField] private ScriptableUserProfileRemoteRepository userProfileRemoteRepository;
    [SerializeField] private GeoLocationRepository geoLocationRepository;

    private void Start()
    {
        DataRepositoriesReferencesContainer.AddObjectInstance(userProfileRemoteRepository);
        DataRepositoriesReferencesContainer.AddObjectInstance(geoLocationRepository);
    }
}