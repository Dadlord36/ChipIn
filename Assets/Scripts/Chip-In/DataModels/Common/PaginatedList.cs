using System.Collections.Generic;
using UnityEngine.Assertions;

namespace DataModels.Common
{
    public class PaginatedList<T>
    {
        private readonly uint _perPage;
        private uint _currentPage = 1;

        private readonly List<List<T>> _pagesData = new List<List<T>>();

        /// <summary>
        /// Dictionary, that accepts page number as key and returns pagesData list index as value.
        /// </summary>
        private readonly Dictionary<uint, int> _pagesDataIndexes = new Dictionary<uint, int>();

        public PaginatedList()
        {
        }

        public PaginatedList(int totalPages, int perPage)
        {
            TotalPages = (uint) totalPages;
            _perPage = (uint) perPage;
        }

        public uint TotalPages { get; }

        public bool PageExists(uint pageNumber) => PageIsExists(pageNumber);

        private int CalculateStartingElementForPage(uint atPageNumber) => (int) (atPageNumber * _perPage - _perPage);
        private bool PageIsExists(uint pageNumber) => _pagesDataIndexes.ContainsKey(pageNumber);

        #region Input

        public void FillPageWithItems(uint pageNumber, List<T> items)
        {
            Assert.IsTrue(pageNumber > 0);

            if (PageIsExists(pageNumber))
            {
                SetItemsAtPage(pageNumber, items);
            }
            else
            {
                AddPageWithItems(pageNumber, items);
            }
        }

        public List<T> this[uint pageNumber] => GetItemsOfPage(pageNumber);

        #endregion

        #region Output

        public bool TryGetCurrentPageItems(out List<T> items)
        {
            return TryGetPageItems(_currentPage, out items);
        }

        public bool TryGetNextPageItems(out List<T> items)
        {
            if (!TryGetPageItems(_currentPage + 1, out items)) return false;
            _currentPage++;
            return true;
        }

        public bool TryGetPreviousPageItems(out List<T> items)
        {
            if (!TryGetPageItems(_currentPage - 1, out items)) return false;
            _currentPage--;
            return true;
        }

        #endregion

        private void AddPageWithItems(uint pageNumber, List<T> items)
        {
            _pagesData.Add(items);
            _pagesDataIndexes.Add(pageNumber, _pagesData.Count - 1);
        }

        private List<T> GetItemsOfPage(uint pageNumber)
        {
            return _pagesData[_pagesDataIndexes[pageNumber]];
        }

        private void SetItemsAtPage(uint pageNumber, List<T> items)
        {
            _pagesData[_pagesDataIndexes[pageNumber]] = items;
        }

        private bool TryGetPageItems(uint atPageNumber, out List<T> pageItems)
        {
            pageItems = null;
            if (!PageIsExists(atPageNumber)) return false;

            pageItems = GetItemsOfPage(atPageNumber);
            return true;
        }


    }
}