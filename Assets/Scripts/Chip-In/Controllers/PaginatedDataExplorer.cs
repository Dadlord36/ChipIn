using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repositories.Interfaces;
using Utilities;

namespace Controllers
{
    public class PaginatedDataExplorer<TDataType> : IPaginatedItemsListInfo where TDataType : class
    {
        private readonly IPaginatedItemsListRepository<TDataType> _itemsListRepository;
        private uint _currentPage = 1;

        #region Implementation of IPaginatedItemsListInfo

        public bool IsBusy => _itemsListRepository.IsBusy;

        public int ItemsPerPage => _itemsListRepository.ItemsPerPage;

        public int TotalPages => _itemsListRepository.TotalPages;

        public uint TotalItemsNumber => _itemsListRepository.TotalItemsNumber;

        public uint LastPageItemsNumber => _itemsListRepository.LastPageItemsNumber;

        #endregion
        
        
        public PaginatedDataExplorer(IPaginatedItemsListRepository<TDataType> itemsListRepository)
        {
            _itemsListRepository = itemsListRepository;
        }

        public async Task<IReadOnlyList<TDataType>> TryToGetCurrentPageItems()
        {
            try
            {
                return await _itemsListRepository.TryGetPageItems(_currentPage);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
        
        public async Task<IReadOnlyList<TDataType>> TryToGetNextPageItems()
        {
            try
            {
                var result = await _itemsListRepository.TryGetPageItems(_currentPage + 1);
                if (result != null) _currentPage++;
                return result;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public async Task<IReadOnlyList<TDataType>> TryToGetPreviousPageItems()
        {
            try
            {
                var result = await _itemsListRepository.TryGetPageItems(_currentPage - 1);
                if (result != null) _currentPage--;
                return result;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }


    }
}