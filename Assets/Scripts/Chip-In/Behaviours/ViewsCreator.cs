using Factories;
using Views;

namespace Behaviours
{
    public static class ViewsCreator 
    {
        public static void PlaceInPreviousContainer<T>() where T : BaseView
        {
            Factory.CreateMultiViewSwitchingHelper().RequestSwitchToView(typeof(T).Name);
        }
    }
}