using System.Collections.Generic;

namespace Repositories.Remote
{
    public abstract class BseItemsListRemoteRepository<TDataType> : RemoteRepositoryBase
    {
        public abstract IReadOnlyList<TDataType> ItemsData { get; }

        public  TDataType this[int index] => ItemsData[index];
    }
}