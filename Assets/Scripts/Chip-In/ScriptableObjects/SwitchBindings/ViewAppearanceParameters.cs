using UnityEngine.EventSystems;
using Views;

namespace ScriptableObjects.SwitchBindings
{
    public struct ViewsPair
    {
        public readonly BaseView ViewToSwitchTo;
        public readonly BaseView ViewToSwitchFrom;

        public ViewsPair(ViewsPairInfo pairInfo, ViewsContainer viewsContainer)
        {
            ViewToSwitchTo =  viewsContainer.GetViewByName(pairInfo.ViewToSwitchToName);
            ViewToSwitchFrom =  viewsContainer.GetViewByName(pairInfo.ViewToSwitchFromName);
        }
    }

    public readonly struct ViewsSwitchingParameters
    {
        public readonly ViewAppearanceParameters? PreviousViewAppearanceParameters;
        public readonly ViewAppearanceParameters NextViewAppearanceParameters;

        public ViewsSwitchingParameters( ViewAppearanceParameters previousViewAppearanceParameters,
            ViewAppearanceParameters nextViewAppearanceParameters)
        {
            PreviousViewAppearanceParameters = previousViewAppearanceParameters;
            NextViewAppearanceParameters = nextViewAppearanceParameters;
        }

        public ViewsSwitchingParameters(ViewAppearanceParameters nextViewAppearanceParameters)
        {
            PreviousViewAppearanceParameters = null;
            NextViewAppearanceParameters = nextViewAppearanceParameters;
        }
    }

    public readonly struct ViewAppearanceParameters
    {
        public enum Appearance
        {
            MoveOut,
            MoveIn,
            Stays
        }

        public enum SwitchingViewPosition
        {
            Above,
            Under
        }

        public readonly Appearance AppearanceType;
        public readonly SwitchingViewPosition SortingPosition;
        public readonly MoveDirection Direction;
        public readonly float MaxPathPercentage;
        public readonly bool ShouldFade;

        public static ViewAppearanceParameters JustFading(SwitchingViewPosition switchingViewPosition)
        {
           return new ViewAppearanceParameters(Appearance.Stays,true, switchingViewPosition, MoveDirection.Down);
        } 
        
        public static ViewAppearanceParameters Idle(SwitchingViewPosition switchingViewPosition)
        {
            return new ViewAppearanceParameters(Appearance.Stays, false, switchingViewPosition, MoveDirection.Down);
        }


        public ViewAppearanceParameters(Appearance appearanceType, bool shouldFade, SwitchingViewPosition sortingPosition, MoveDirection direction,
            float maxPathPercentage = 1f)
        {
            AppearanceType = appearanceType;
            SortingPosition = sortingPosition;
            ShouldFade = shouldFade;
            Direction = direction;
            MaxPathPercentage = maxPathPercentage;
        }
    }
}