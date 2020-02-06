using DataModels;
using DataModels.Interfaces;
using UnityEngine;

namespace ScriptableObjects.DataSets
{
    [CreateAssetMenu(fileName = nameof(SlotGameIconsSetScriptableObject),
        menuName = nameof(DataSets) + "/" + nameof(SlotGameIconsSetScriptableObject),
        order = 0)]
    public class SlotGameIconsSetScriptableObject : ScriptableObject, ISlotGameIconsSet
    {
        [SerializeField] private SlotGameIconsSet slotGameIconsSet;

        public Sprite First
        {
            get => slotGameIconsSet.First;
            set => slotGameIconsSet.First = value;
        }

        public Sprite Second
        {
            get => slotGameIconsSet.Second;
            set => slotGameIconsSet.Second = value;
        }

        public Sprite Third
        {
            get => slotGameIconsSet.Third;
            set => slotGameIconsSet.Third = value;
        }

        public Sprite Fourth
        {
            get => slotGameIconsSet.Fourth;
            set => slotGameIconsSet.Fourth = value;
        }
    }
}