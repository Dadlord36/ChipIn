using UnityEngine.EventSystems;

namespace UI.Elements
{
    public class GroupedItemsConnector : UIBehaviour
    {
        private IGroupableSelection[] _groupableSelections;

        protected override void Start()
        {
            base.Start();
            _groupableSelections = GetComponentsInChildren<IGroupableSelection>();
            
            for (int i = 0; i < _groupableSelections.Length; i++)
            {
                ConnectToOtherGroupedItems(i);
            }

            void ConnectToOtherGroupedItems(int indexInArray)
            {
                for (int i = 0; i < _groupableSelections.Length; i++)
                {
                    if (i == indexInArray) continue;
                    _groupableSelections[indexInArray].SubscribeOnMainEvent(_groupableSelections[i]);
                }
            }
        }
    }
}