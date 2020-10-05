using System.Threading.Tasks;
using DataModels;
using DataModels.Interfaces;
using HttpRequests.RequestsProcessors.GetRequests;
using UnityEngine;

namespace Repositories.Remote
{
    [CreateAssetMenu(fileName = nameof(ScriptableUserProductsRepository), menuName = nameof(Repositories) + "/" + nameof(Remote) + "/"
                                                                                     + nameof(ScriptableUserProductsRepository), order = 0)]
    public class ScriptableUserProductsRepository : ScriptablePaginatedItemsListRepository<ProductDataModel, UserProductsResponseDataModel,
        IUserProductsResponseModel, UserProductsRepository>
    {
        public int CurrentlySelectedIndex
        {
            get => RemoteRepository.CurrentlySelectedIndex;
            set => RemoteRepository.CurrentlySelectedIndex = value;
        }

        public Task<ProductDataModel> GetCurrentlySelectedProductAsync => RemoteRepository.GetCurrentlySelectedProductAsync;
    }
}