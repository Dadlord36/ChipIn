using UnityEngine;

namespace Repositories.Remote
{
    public struct SingleGameData
    {
        public readonly string TypeName;

        public SingleGameData(string typeName)
        {
            TypeName = typeName;
        }
    }
    
    [CreateAssetMenu(fileName = nameof(SoloGamesRemoteRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(SoloGamesRemoteRepository), order = 0)]
    public class SoloGamesRemoteRepository : BaseItemsListRepository<SingleGameData>
    {
    }
}