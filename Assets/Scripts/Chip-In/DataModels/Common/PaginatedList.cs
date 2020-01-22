using System.Collections.Generic;
using Common;

namespace DataModels.Common
{
    public class PaginatedList<T> : LiveData<T>
    {
        public  PaginationData Pagination { get;  set; }

        public PaginatedList()
        {
        }
        
        public PaginatedList(PaginationData pagination, IEnumerable<T> itemsList) : base(itemsList)
        {
            Pagination = pagination;
        }
    }
}