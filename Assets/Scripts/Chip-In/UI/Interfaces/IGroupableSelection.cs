using UnityEngine.Events;

namespace UI.Interfaces
{
    public interface IGroupableSelection
    {
        void OnOtherItemSelected();
        void SelectAsOneOfGroup();

        void SubscribeOnMainEvent(UnityAction onOtherItemInGroupSelected);
    }
}