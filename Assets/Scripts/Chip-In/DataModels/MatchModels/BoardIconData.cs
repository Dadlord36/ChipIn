using CustomAnimators;

namespace DataModels.MatchModels
{
    public struct BoardIconData
    {
        public readonly SimpleImageAnimator.SpritesAnimatorResource AnimatedIconResource;
        public readonly int Id;

        public BoardIconData(SimpleImageAnimator.SpritesAnimatorResource animatedIconResource, int id)
        {
            Id = id;
            AnimatedIconResource = animatedIconResource;
        }
    }
}