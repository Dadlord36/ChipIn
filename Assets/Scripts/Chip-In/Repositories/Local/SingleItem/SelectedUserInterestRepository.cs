using System.Threading.Tasks;
using Repositories.Remote.Paginated;
using UnityEngine;

namespace Repositories.Local.SingleItem
{
    [CreateAssetMenu(fileName = nameof(SelectedUserInterestRepository), menuName = nameof(Repositories) + "/" + nameof(Local) + "/" + nameof(SingleItem)
                                                                                   + "/" + nameof(SelectedUserInterestRepository), order = 0)]
    public class SelectedUserInterestRepository : ScriptableObject
    {
        [SerializeField] private InterestsBasicDataPaginatedListRepository basicDataPaginatedListRepository;
        public uint SelectedUserInterestRepositoryIndex { get; set; }

        public Task<int?> SelectedUserInterestId =>
            basicDataPaginatedListRepository.CreateGetItemWithIndexTask(SelectedUserInterestRepositoryIndex).ContinueWith(task =>
                task.Result.Id, TaskContinuationOptions.OnlyOnRanToCompletion);
    }
}