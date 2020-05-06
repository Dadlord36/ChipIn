using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.Assertions;

namespace DataModels.Common
{
    public class PaginatedList<T> : LiveData<T>
    {
        private uint _initializedPages;
        private readonly uint _perPage;
        private uint _currentPage = 1;

        public PaginatedList()
        {
        }

        public PaginatedList(int totalPages, int perPage)
        {
            TotalPages = (uint) totalPages;
            _perPage = (uint) perPage;
        }

        public uint TotalPages { get; }

        public bool PageExists(uint pageNumber) => PageIsInitialized(pageNumber);

        private int CalculateStartingElementForPage(uint atPageNumber) => (int) (atPageNumber * _perPage - _perPage);
        private bool PageIsInitialized(uint pageNumber) => pageNumber <= _initializedPages;

        #region Input

        public void FillPageWithItems(uint pageNumber, IReadOnlyList<T> items)
        {
            Assert.IsTrue(pageNumber > 0);

            if (PageIsInitialized(pageNumber))
            {
                FillInitializedPageWithItems(pageNumber, items);
            }
            else
            {
                InitializeNextPageAndFillWithItems(items);
            }
        }

        #endregion

        #region Output

        public bool TryGetCurrentPageItems(out List<T> items)
        {
            return TryGetItemsPage(_currentPage, out items);
        }

        public bool TryGetNextPageItems(out List<T> items)
        {
            if (!TryGetItemsPage(_currentPage + 1, out items)) return false;
            _currentPage++;
            return true;
        }

        public bool TryGetPreviousPageItems(out List<T> items)
        {
            if (!TryGetItemsPage(_currentPage - 1, out items)) return false;
            _currentPage--;
            return true;
        }

        #endregion

        private void InitializeNextPageAndFillWithItems(IReadOnlyList<T> items)
        {
            Items.AddRange(items);
            _initializedPages++;
        }

        private void FillInitializedPageWithItems(uint pageNumber, IReadOnlyList<T> items)
        {
            var startingIndex = CalculateStartingElementForPage(pageNumber);
            int index = 0;
            for (int i = startingIndex; i < startingIndex + _perPage; i++)
            {
                Items.Insert(i, items[index]);
                index++;
            }
        }

        private bool TryGetItemsPage(uint atPageNumber, out List<T> pageItems)
        {
            pageItems = null;
            if (!PageIsInitialized(atPageNumber)) return false;
            var startingIndex = CalculateStartingElementForPage(atPageNumber);
            var itemsShouldBe = atPageNumber * _perPage;
            
            int count = (int) _perPage;
            if (itemsShouldBe > Items.Count)
            {
                count = (int) (_perPage-(itemsShouldBe-Items.Count));
            }

            pageItems = Items.GetRange(startingIndex, count);
            return true;
        }
    }
}