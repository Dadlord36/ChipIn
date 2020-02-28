using CustomAnimators;

namespace DataModels.MatchModels
{
    public class BoardIconData
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