using System.Threading.Tasks;
using DataModels;
using Repositories.Remote.Paginated;
using UnityEngine;

namespace Repositories.Local.SingleItem
{
    [CreateAssetMenu(fileName = nameof(SelectedMerchantInterestRepository), menuName = nameof(Repositories) + "/" + nameof(Local) + "/"
                                                                                       + nameof(SingleItem)
                                                                                       + "/" + nameof(SelectedMerchantInterestRepository), order = 0)]
    public class SelectedMerchantInterestRepository : ScriptableObject
    {
        [SerializeField] private MarketInterestsPaginatedListRepository marketInterestsPaginatedListRepository;

        public Task<int?> SelectedInterestId =>
            marketInterestsPaginatedListRepository.CreateGetItemWithIndexTask(SelectedInterestRepositoryIndex).ContinueWith(
                task => task.Result.Id, TaskContinuationOptions.OnlyOnRanToCompletion);
        
        public uint SelectedInterestRepositoryIndex { get; set; }
        
        public Task<MarketInterestDetailsDataModel> CreateGetSelectedInterestDataTask()
        {
            return marketInterestsPaginatedListRepository.CreateGetItemWithIndexTask(SelectedInterestRepositoryIndex);
        }

        
    }
}