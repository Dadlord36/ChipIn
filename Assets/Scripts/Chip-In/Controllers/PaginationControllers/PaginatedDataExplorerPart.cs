using System.Collections.Generic;
using System.Threading.Tasks;
using Repositories.Interfaces;

namespace Controllers.PaginationControllers
{
    public partial class PaginatedDataExplorer<TPaginatedDataListRepository, TItemsDataModel>
    {
        private class PaginatedDataController<TDataType> : IPaginatedItemsListInfo where TDataType : class
        {
            private readonly IPaginatedItemsListRepository<TDataType> _itemsListRepository;


            #region Implementation of IPaginatedItemsListInfo

            public bool IsInitialized => _itemsListRepository.IsInitialized;
            public bool IsBusy => _itemsListRepository.IsBusy;
            public uint GetCorrespondingToIndexPage(uint pageItemIndex) => _itemsListRepository.GetCorrespondingToIndexPage(pageItemIndex);
            public int ItemsPerPage => _itemsListRepository.ItemsPerPage;
            public int TotalPages => _itemsListRepository.TotalPages;
            public uint TotalItemsNumber => _itemsListRepository.TotalItemsNumber;
            public uint LastPageItemsNumber => _itemsListRepository.LastPageItemsNumber;

            #endregion


            public bool PageIsExists(uint pageNumber) => pageNumber >= PaginationConstants.MinPageNumber && pageNumber <= TotalPages;

            public PaginatedDataController(IPaginatedItemsListRepository<TDataType> itemsListRepository)
            {
                _itemsListRepository = itemsListRepository;
            }

            public Task<IReadOnlyList<TDataType>> CreateGetItemsRangeTask(uint startIndex, uint length)
            {
                return _itemsListRepository.CreateGetItemsRangeTask(startIndex, length);
            }

            public Task<IReadOnlyList<TDataType>> CreateGetPageItemsTask(uint pageNumber)
            {
                return _itemsListRepository.CreateGetPageItemsTask(pageNumber);
            }
        }
    }
}