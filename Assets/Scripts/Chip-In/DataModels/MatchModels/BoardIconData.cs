using CustomAnimators;
using DataModels.Interfaces;

namespace DataModels.MatchModels
{
    public class BoardIconData : IIdentifier
    {
        public readonly SimpleImageAnimator.SpritesAnimatorResource AnimatedIconResource;
        public int? Id { get; set; }

        public BoardIconData(SimpleImageAnimator.SpritesAnimatorResource animatedIconResource, IIdentifier identifier)
        {
            Id = identifier.Id;
            AnimatedIconResource = animatedIconResource;
        }
    }
}