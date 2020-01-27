using UnityEngine.Events;

namespace ViewModels.UI.Interfaces
{
    public interface IOneOfAGroup
    {
        void OnOtherOnePerformGroupAction();
        event UnityAction GroupActionPerformed;
    }
}