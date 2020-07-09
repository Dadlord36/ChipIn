using UnityEngine.EventSystems;
using ViewModels.UI.Interfaces;

namespace ViewModels.UI.Elements
{
    public class GroupItemsConnector : UIBehaviour
    {
        protected override void Start()
        {
            base.Start();
            ConnectGroupItems(GetComponentsInChildren<IOneOfAGroup>());
            Destroy(this);
        }

        private static void ConnectGroupItems(IOneOfAGroup[] groupableSelections)
        {
            for (int i = 0; i < groupableSelections.Length; i++)
            {
                ConnectToOtherGroupItems(i);
            }

            void ConnectToOtherGroupItems(int indexInArray)
            {
                for (int i = 0; i < groupableSelections.Length; i++)
                {
                    if (i == indexInArray) continue;
                    groupableSelections[indexInArray].GroupActionPerformed +=
                        groupableSelections[i].OnOtherOnePerformGroupAction;
                }
            }
        }

        public static void ConnectGroupTo(IOneOfAGroup ofAGroup, IOneOfAGroup[] group)
        {
            for (int i = 0; i < group.Length; i++)
            {
                ofAGroup.GroupActionPerformed += group[i].OnOtherOnePerformGroupAction;
            }
        }

        public static void DisconnectGroupFrom(IOneOfAGroup ofAGroup, IOneOfAGroup[] group)
        {
            for (int i = 0; i < group.Length; i++)
            {
                ofAGroup.GroupActionPerformed -= group[i].OnOtherOnePerformGroupAction;
            }
        }

        private static void DisconnectGroupItems(IOneOfAGroup[] groupableSelections)
        {
            for (int i = 0; i < groupableSelections.Length; i++)
            {
                DisconnectFromOtherGroupItems(i);
            }

            void DisconnectFromOtherGroupItems(int indexInArray)
            {
                for (int i = 0; i < groupableSelections.Length; i++)
                {
                    if (i == indexInArray) continue;
                    groupableSelections[indexInArray].GroupActionPerformed -=
                        groupableSelections[i].OnOtherOnePerformGroupAction;
                }
            }
        }
    }
}