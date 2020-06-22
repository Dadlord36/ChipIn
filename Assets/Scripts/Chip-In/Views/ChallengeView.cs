using Views.ViewElements;

namespace Views
{
    public sealed class ChallengeView : ContainerView<ChallengeCardView>
    {
        public ChallengeView() : base(nameof(ChallengeView))
        {
        }
    }
}