using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Elements
{
    public class StateSwitchableButton : Button
    {
        public enum ButtonSelectionSate
        {
            Normal,
            Selected
        }

        public override void OnSelect(BaseEventData eventData)
        {
        }

        public override void OnDeselect(BaseEventData eventData)
        {
        }

        public void SwitchButtonState(ButtonSelectionSate selectionState)
        {
            switch (selectionState)
            {
                case ButtonSelectionSate.Normal:
                    DoStateTransition(SelectionState.Normal, false);
                    break;
                case ButtonSelectionSate.Selected:
                    DoStateTransition(SelectionState.Selected, false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(selectionState), selectionState, null);
            }
        }
    }
}