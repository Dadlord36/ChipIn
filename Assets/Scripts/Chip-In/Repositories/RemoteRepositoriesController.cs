using DataModels;
using Repositories.Remote;
using UnityEngine;

namespace Repositories
{
    [CreateAssetMenu(fileName = nameof(RemoteRepositoriesController),
        menuName = nameof(Repositories) + "/" + "Controllers/" + nameof(RemoteRepositoriesController), order = 0)]
    public class RemoteRepositoriesController : ScriptableObject
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private RemoteRepositoryBase[] remoteRepositories;

        public void SetAuthorisationDataAndInvokeRepositoriesLoading(IAuthorisationModel authorisationModel)
        {
            authorisationDataRepository.Set(authorisationModel);
            InvokeRepositoriesLoading();
        }
        
        private void InvokeRepositoriesLoading()
        {
            for (int i = 0; i < remoteRepositories.Length; i++)
            {
                remoteRepositories[i].LoadDataFromServer();
            }
        }
    }
}