using System.Collections.Generic;

namespace Repositories.Remote
{
    public abstract class BaseItemsListRemoteRepository<TDataType, TRepository> : ScriptableRemoteRepositoryBase<TRepository> 
        where TRepository : class, IRemoteRepositoryBase, new()
    {
        public abstract IReadOnlyList<TDataType> ItemsData { get; }

        public  TDataType this[int index] => ItemsData[index];
    }
}