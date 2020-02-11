using DataModels.ResponsesModels;
using Repositories.Local;
using Repositories.Remote;
using UnityEngine;

namespace Repositories
{
    [CreateAssetMenu(fileName = nameof(RemoteRepositoriesController),
        menuName = nameof(Repositories) + "/" + "Controllers/" + nameof(RemoteRepositoriesController), order = 0)]
    public class RemoteRepositoriesController : ScriptableObject
    {
        [SerializeField] private SessionStateRepository sessionStateRepository;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private RemoteRepositoryBase[] remoteRepositories;

        public void SetAuthorisationDataAndInvokeRepositoriesLoading(ILoginResponseModel loginModel)
        {
            authorisationDataRepository.Set(loginModel.AuthorisationData);
            sessionStateRepository.SetLoginState(loginModel.UserProfileData.Role);
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