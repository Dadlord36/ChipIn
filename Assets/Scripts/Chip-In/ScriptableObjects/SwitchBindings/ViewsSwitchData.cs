using System;
using Views;

namespace ScriptableObjects.SwitchBindings
{
    public struct ViewsSwitchData
    {
        public enum AppearingSide
        {
            FromLeft,
            FromRight
        }

        public readonly BaseView ViewToSwitchTo;
        public readonly AppearingSide ScrollSide;

        public ViewsSwitchData(BaseView toView, AppearingSide sideToScrollTo = AppearingSide.FromRight)
        {
            ViewToSwitchTo = toView;
            ScrollSide = sideToScrollTo;
        }
    }
}