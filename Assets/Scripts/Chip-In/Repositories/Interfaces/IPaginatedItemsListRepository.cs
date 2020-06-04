using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IPaginatedItemsListRepository<TDataType> : IPaginatedItemsListInfo where TDataType : class
    {
        Task<IReadOnlyList<TDataType>> TryGetPageItems(uint pageNumber);
        Task<List<TDataType>> TryGetItemsRange(uint startIndex, uint length);
        Task<TDataType> TryGetItemWithIndex(uint i);
    }
}