using System.Threading.Tasks;
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
    public class SoloGamesRemoteRepository : BaseNotPaginatedListRepository<SingleGameData>
    {
        protected override void ConfirmDataLoading()
        {
            throw new System.NotImplementedException();
        }

        protected override void ConfirmDataSaved()
        {
            throw new System.NotImplementedException();
        }

        public override Task LoadDataFromServer()
        {
            throw new System.NotImplementedException();
        }
    }
}