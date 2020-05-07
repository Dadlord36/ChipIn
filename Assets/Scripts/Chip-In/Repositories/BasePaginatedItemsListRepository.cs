using System;
using System.Collections.Generic;
using DataModels.Common;
using Repositories.Remote;

namespace Repositories
{
    public abstract class BasePaginatedItemsListRepository<TDataType> : BseItemsListRemoteRepository<TDataType>
    {
        [NonSerialized] protected PaginatedList<TDataType> PaginatedData = new PaginatedList<TDataType>();
        public override IReadOnlyList<TDataType> ItemsData => PaginatedData.DataList;
    }
}