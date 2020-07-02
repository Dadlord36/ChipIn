using System;
using ScriptableObjects.SwitchBindings;

namespace Utilities
{
    public static class EnumUtilities
    {
        public static int GetSortingOrderFromSwitchingViewPosition(ViewAppearanceParameters.SwitchingViewPosition switchingViewPosition)
        {
            switch (switchingViewPosition)
            {
                case ViewAppearanceParameters.SwitchingViewPosition.Above:
                    return 1;
                case ViewAppearanceParameters.SwitchingViewPosition.Under:
                    return 0;
                default:
                    throw new ArgumentOutOfRangeException(nameof(switchingViewPosition), switchingViewPosition, null);
            }
        }
    }
}