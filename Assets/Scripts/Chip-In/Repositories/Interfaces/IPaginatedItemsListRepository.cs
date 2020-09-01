using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IPaginatedItemsListRepository<TDataType> : IPaginatedItemsListInfo 
        where TDataType : class
    {
        Task<IReadOnlyList<TDataType>> CreateGetPageItemsTask(uint pageNumber);
        Task<IReadOnlyList<TDataType>> CreateGetItemsRangeTask(uint startIndex, uint length);
        Task<TDataType> GetItemWithIndexAsync(uint itemIndex);
        void Clear();
    }
}