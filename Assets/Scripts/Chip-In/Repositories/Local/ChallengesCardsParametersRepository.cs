using System;
using UnityEngine;

namespace Repositories.Local
{
    [CreateAssetMenu(fileName = nameof(ChallengesCardsParametersRepository),
        menuName = nameof(Repositories) + "/" + nameof(Local) + "/" + nameof(ChallengesCardsParametersRepository), order = 0)]
    public class ChallengesCardsParametersRepository : ScriptableObject
    {
        [SerializeField] private ChallengeCardParameters[] challengesGameItemVisibleParameters;

        [Serializable]
        public struct ChallengeCardParameters
        {
            public Sprite icon;
            [HideInInspector] public uint coinsAmount;
            public string challengeTypeName;
        }

        public ChallengeCardParameters GetItemVisibleParameters(in string challengeTypeName)
        {
            for (int i = 0; i < challengesGameItemVisibleParameters.Length; i++)
            {
                if (string.Equals(challengesGameItemVisibleParameters[i].challengeTypeName, challengeTypeName,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return challengesGameItemVisibleParameters[i];
                }
            }

            throw new Exception($"There is no Item of type {challengeTypeName}");
        }
    }
}