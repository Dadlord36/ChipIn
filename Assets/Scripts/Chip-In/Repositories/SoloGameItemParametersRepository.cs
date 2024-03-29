﻿using System;
using UnityEngine;

namespace Repositories
{
    [CreateAssetMenu(fileName = nameof(SoloGameItemParametersRepository),
        menuName = nameof(Repositories) + "/" + nameof(SoloGameItemParametersRepository), order = 0)]
    public class SoloGameItemParametersRepository : ScriptableObject
    {
        [SerializeField] private SoloGameItemVisibleParameters[] soloGameItemVisibleParameters;

        [Serializable]
        public struct SoloGameItemVisibleParameters
        {
            public string gameTypeName;
            public Sprite gameTypeSprite;
        }

        public SoloGameItemVisibleParameters GetItemVisibleParameters(in string gameTypeName)
        {
            for (int i = 0; i < soloGameItemVisibleParameters.Length; i++)
            {
                if (string.Equals(soloGameItemVisibleParameters[i].gameTypeName, gameTypeName,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return soloGameItemVisibleParameters[i];
                }
            }
            throw new Exception($"There is no Item of type {gameTypeName}");
        }
    }
}