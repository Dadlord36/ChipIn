using UnityEngine;

namespace DataModels.MatchModels
{
    public struct BoardIcon
    {
        public readonly Sprite IconSprite;
        public readonly int Id;

        public BoardIcon(Sprite iconSprite, int id)
        {
            Id = id;
            IconSprite = iconSprite;
        }
    }
}