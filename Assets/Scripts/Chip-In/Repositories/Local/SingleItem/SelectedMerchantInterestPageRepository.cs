using System.Threading.Tasks;
using DataModels;
using Repositories.Remote.Paginated;
using UnityEngine;

namespace Repositories.Local.SingleItem
{
    [CreateAssetMenu(fileName = nameof(SelectedMerchantInterestPageRepository), menuName = nameof(Repositories) + "/" + nameof(Local) + "/"
                                                                                           + nameof(SingleItem)
                                                                                           + "/" + nameof(SelectedMerchantInterestPageRepository), order = 0)]
    public class SelectedMerchantInterestPageRepository : ScriptableObject
    {
        [SerializeField] private MerchantInterestPagesPaginatedRepository merchantInterestPagesPaginatedRepository;
        public uint SelectedInterestPageRepositoryIndex { get; set; }
        
        public Task<int?> SelectedInterestPageId =>
            merchantInterestPagesPaginatedRepository.CreateGetItemWithIndexTask(SelectedInterestPageRepositoryIndex).ContinueWith(
                task => task.Result.Id, TaskContinuationOptions.OnlyOnRanToCompletion);
        public Task<MerchantInterestPageDataModel> CreateGetSelectedInterestPageDataTask()
        {
            return merchantInterestPagesPaginatedRepository.CreateGetItemWithIndexTask(SelectedInterestPageRepositoryIndex);
        }
    }
}