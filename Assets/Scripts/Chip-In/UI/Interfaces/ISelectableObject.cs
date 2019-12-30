using UnityEngine.Events;

namespace UI.Interfaces
{
    public interface ISelectableObject
    {
        void OnOtherItemSelected();
        void SelectAsOneOfGroup();

        void SubscribeOnMainEvent(UnityAction onOtherItemInGroupSelected);
    }
}