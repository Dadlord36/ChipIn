using Views;

namespace ViewModels.Basic
{
    public abstract class CorrespondingViewsSwitchingViewModel<TViewType> : ViewsSwitchingViewModel where TViewType : BaseView
    {
        protected TViewType RelatedView => View as TViewType;
    }
}