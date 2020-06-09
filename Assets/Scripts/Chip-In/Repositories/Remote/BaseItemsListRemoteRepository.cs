using System.Collections.Generic;

namespace Repositories.Remote
{
    public abstract class BaseItemsListRemoteRepository<TDataType> : RemoteRepositoryBase
    {
        public abstract IReadOnlyList<TDataType> ItemsData { get; }

        public  TDataType this[int index] => ItemsData[index];
        
    }
}