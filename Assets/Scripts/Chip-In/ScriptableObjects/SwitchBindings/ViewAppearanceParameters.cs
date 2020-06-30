using UnityEngine.EventSystems;

namespace ScriptableObjects.SwitchBindings
{
    public readonly struct ViewsSwitchingParameters
    {
        public readonly ViewAppearanceParameters PreviousViewAppearanceParameters;
        public readonly ViewAppearanceParameters NextViewAppearanceParameters;

        public ViewsSwitchingParameters(ViewAppearanceParameters previousViewAppearanceParameters,
            ViewAppearanceParameters nextViewAppearanceParameters)
        {
            PreviousViewAppearanceParameters = previousViewAppearanceParameters;
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

        public readonly string 
        public readonly Appearance AppearanceType;
        public readonly SwitchingViewPosition ViewPosition;
        public readonly MoveDirection Direction;
        public readonly bool ShouldFade;

        public bool IsIdle => !ShouldFade && AppearanceType == Appearance.Stays;


        private ViewAppearanceParameters(Appearance appearanceType, bool shouldFade)
        {
            AppearanceType = appearanceType;
            ViewPosition = SwitchingViewPosition.Above;
            ShouldFade = shouldFade;
            Direction = MoveDirection.Down;
        }

        public ViewAppearanceParameters(Appearance appearanceType, bool shouldFade, SwitchingViewPosition viewPosition, MoveDirection direction)
        {
            AppearanceType = appearanceType;
            ViewPosition = viewPosition;
            ShouldFade = shouldFade;
            Direction = direction;
        }


        public bool Equals(ViewAppearanceParameters other)
        {
            return AppearanceType == other.AppearanceType && ShouldFade == other.ShouldFade;
        }

        public override bool Equals(object obj)
        {
            return obj is ViewAppearanceParameters other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) AppearanceType * 397) ^ ShouldFade.GetHashCode();
            }
        }
    }
}