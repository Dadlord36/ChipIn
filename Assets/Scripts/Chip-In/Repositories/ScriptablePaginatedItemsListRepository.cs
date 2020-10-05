using System.Collections.Generic;
using System.Threading.Tasks;
using Repositories.Remote;

namespace Repositories
{
    public abstract class ScriptablePaginatedItemsListRepository<TDataType, TRequestResponseDataModel, TRequestResponseModelInterface,
        TPaginatedListRepository> : ScriptableRemoteRepositoryBase<TPaginatedListRepository>, IPaginatedItemsListRepositoryBase<TDataType>
        where TDataType : class
        where TRequestResponseDataModel : class, TRequestResponseModelInterface
        where TRequestResponseModelInterface : class
        where TPaginatedListRepository : PaginatedItemsListRepository<TDataType, TRequestResponseDataModel, TRequestResponseModelInterface>, new()
    {
        public bool IsInitialized => RemoteRepository.IsInitialized;

        public uint GetCorrespondingToIndexPage(uint pageItemIndex)
        {
            return RemoteRepository.GetCorrespondingToIndexPage(pageItemIndex);
        }

        public int ItemsPerPage => RemoteRepository.ItemsPerPage;

        public int TotalPages => RemoteRepository.TotalPages;

        public uint TotalItemsNumber => RemoteRepository.TotalItemsNumber;

        public uint LastPageItemsNumber => RemoteRepository.LastPageItemsNumber;

        public Task<IReadOnlyList<TDataType>> CreateGetPageItemsTask(uint pageNumber)
        {
            return RemoteRepository.CreateGetPageItemsTask(pageNumber);
        }

        public Task<IReadOnlyList<TDataType>> GetItemsRangeAsync(uint startIndex, uint length)
        {
            return RemoteRepository.GetItemsRangeAsync(startIndex, length);
        }

        public Task<TDataType> GetItemWithIndexAsync(uint itemIndex)
        {
            return RemoteRepository.GetItemWithIndexAsync(itemIndex);
        }

        public void Clear()
        {
            RemoteRepository.Clear();
        }

        public void CancelAllTasks()
        {
            RemoteRepository.CancelAllTasks();
        }
    }
}