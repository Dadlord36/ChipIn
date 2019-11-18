using Factories;
using Views;

namespace Behaviours
{
    public static class ViewsCreator 
    {
        public static void PlaceInPreviousContainer<T>() where T : BaseView
        {
            Factory.CreateViewSwitchingHelper().RequestSwitchToView(typeof(T).Name);
        }
    }
}