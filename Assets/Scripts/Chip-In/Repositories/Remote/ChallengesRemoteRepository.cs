using System.Threading.Tasks;
using UnityEngine;

namespace Repositories.Remote
{
    public struct ChallengeData
    {
        public readonly string ChallengeTypeName;
        public readonly uint CoinsPrice;

        public ChallengeData(string challengeTypeName, uint coinsPrice)
        {
            ChallengeTypeName = challengeTypeName;
            CoinsPrice = coinsPrice;
        }
    }

    [CreateAssetMenu(fileName = nameof(ChallengesRemoteRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(ChallengesRemoteRepository), order = 0)]
    public class ChallengesRemoteRepository : BaseNotPaginatedListRepository<ChallengeData>
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

        public override Task SaveDataToServer()
        {
            throw new System.NotImplementedException();
        }
    }
}