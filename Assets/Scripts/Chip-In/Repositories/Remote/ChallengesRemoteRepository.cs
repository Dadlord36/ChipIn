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
    public class ChallengesRemoteRepository : ScriptableObject
    {
    }
}