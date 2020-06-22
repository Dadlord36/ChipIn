using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Repositories.Interfaces;
using Repositories.Remote;
using UnityEngine;

namespace Controllers.PaginationControllers
{
    [Serializable]
    public partial class PaginatedDataExplorer<TPaginatedDataListRepository, TItemsDataModel>
        where TItemsDataModel : class
        where TPaginatedDataListRepository : RemoteRepositoryBase,
        IPaginatedItemsListRepository<TItemsDataModel>
    {
        private readonly string _tag;


        [SerializeField] private TPaginatedDataListRepository dataRepository;
        [SerializeField, Min(1)] private uint retrievingItemsAmount = 1;

        private PaginatedDataController<TItemsDataModel> _paginatedDataController;
        private uint _startItemToGetDataIndex;
        private uint _currentPage = PaginationConstants.MinPageNumber;


        public bool CanListForward => _currentPage < _paginatedDataController.TotalPages;
        public bool CanListBackward => _currentPage > PaginationConstants.MinPageNumber;


        public PaginatedDataExplorer(string tag)
        {
            _tag = tag;
        }

        public void Initialize()
        {
            _paginatedDataController = new PaginatedDataController<TItemsDataModel>(dataRepository);
        }

        public Task<IReadOnlyList<TItemsDataModel>> CreateGetCurrentPageItemsTask()
        {
            return _paginatedDataController.CreateGetItemsRangeTask(_startItemToGetDataIndex, retrievingItemsAmount);
        }


        /// <summary>
        /// Retrieved Task requested a list of next items if they are exists. If requested amount is more then it is left
        /// - returns all items left. 
        /// </summary>
        /// <returns></returns>
        public Task<IReadOnlyList<TItemsDataModel>> CreateListForwardTask()
        {
            AdjustStartingIndexOnOnePortionForward();
            return CreateGetRangeTask();
        }

        /// <summary>
        /// Retrieved Task requested a list of previous items if they are exists. If requested amount is more then it is left
        /// - returns all items left. 
        /// </summary>
        /// <returns></returns>
        public Task<IReadOnlyList<TItemsDataModel>> CreateListBackwardTask()
        {
            AdjustStartingIndexOnOnePortionBackward();
            return CreateGetRangeTask();
        }

        private void AdjustStartingIndexOnOnePortionForward()
        {
            _startItemToGetDataIndex += retrievingItemsAmount;
            _currentPage = _paginatedDataController.GetCorrespondingToIndexPage(_startItemToGetDataIndex);
        }

        private void AdjustStartingIndexOnOnePortionBackward()
        {
            _startItemToGetDataIndex -= retrievingItemsAmount;
            _currentPage = _paginatedDataController.GetCorrespondingToIndexPage(_startItemToGetDataIndex);
        }

        private Task<IReadOnlyList<TItemsDataModel>> CreateGetRangeTask()
        {
            return CreateGetRangeTask(_startItemToGetDataIndex, retrievingItemsAmount);
        }

        private Task<IReadOnlyList<TItemsDataModel>> CreateGetRangeTask(uint startItemToGetDataIndex, uint itemsAmount)
        {
            return _paginatedDataController.CreateGetItemsRangeTask(startItemToGetDataIndex, itemsAmount);
        }
        
        private static PlacementBoundary FigureOutPlacementBoundary(int controlValue)
        {
            return controlValue > 0 ? PlacementBoundary.Start : PlacementBoundary.End;
        }
    }
}