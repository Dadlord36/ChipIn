using UnityEngine.Events;

namespace ViewModels.UI.Interfaces
{
    public interface ISelectableObject
    {
        void OnOtherItemSelected();
        void SelectAsOneOfGroup();

        void SubscribeOnMainEvent(UnityAction onOtherItemInGroupSelected);
    }
}