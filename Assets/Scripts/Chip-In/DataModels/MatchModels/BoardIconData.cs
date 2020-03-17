using CustomAnimators;
using DataModels.Interfaces;

namespace DataModels.MatchModels
{
    public class BoardIconData
    {
        public readonly SimpleImageAnimator.SpritesAnimatorResource AnimatedIconResource;
        public readonly int? Id;

        public BoardIconData(SimpleImageAnimator.SpritesAnimatorResource animatedIconResource, IIdentifier identifier)
        {
            Id = identifier.Id;
            AnimatedIconResource = animatedIconResource;
        }
    }
}