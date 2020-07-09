using Views;

namespace ViewModels.Basic
{
    public abstract class CorrespondingViewModel<TViewType> : BaseViewModel where TViewType : BaseView
    {
        protected TViewType RelatedView => View as TViewType;

        public CorrespondingViewModel(string tag) : base(tag)
        {
        }
    }
}