using UnityEngine.EventSystems;
using ViewModels.UI.Interfaces;

namespace ViewModels.UI.Elements
{
    public class GroupedItemsConnector : UIBehaviour
    {
        private ISelectableObject[] _groupableSelections;

        protected override void Start()
        {
            base.Start();
            _groupableSelections = GetComponentsInChildren<ISelectableObject>();
            
            for (int i = 0; i < _groupableSelections.Length; i++)
            {
                ConnectToOtherGroupedItems(i);
            }

            void ConnectToOtherGroupedItems(int indexInArray)
            {
                for (int i = 0; i < _groupableSelections.Length; i++)
                {
                    if (i == indexInArray) continue;
                    _groupableSelections[indexInArray].SubscribeOnMainEvent(_groupableSelections[i].OnOtherItemSelected);
                }
            }
        }
    }
}