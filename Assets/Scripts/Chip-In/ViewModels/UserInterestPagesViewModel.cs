using ViewModels.Basic;
using Views;

namespace ViewModels
{

    public class UserInterestPagesViewModel : CorrespondingViewsSwitchingViewModel<UserInterestPagesView>
    {
        public UserInterestPagesViewModel() : base(nameof(UserInterestPagesViewModel))
        {
        }

        /*protected override void OnEnable()
        {
            base.OnEnable();
            SubscribeOnEvents();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeFromEvents();
        }*/


 
    }
}