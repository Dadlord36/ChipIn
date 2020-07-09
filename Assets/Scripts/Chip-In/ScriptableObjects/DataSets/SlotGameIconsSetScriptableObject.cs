using System;
using DataModels.Interfaces;
using UnityEngine;

namespace ScriptableObjects.DataSets
{
    [Serializable]
    public struct IndexedSprite : IIdentifier
    {
        [SerializeField] private int id;
        [SerializeField] private Sprite iconSprite;

        public int? Id
        {
            get => id;
            set => id = (int) value;
        }

        public Sprite IconSprite
        {
            get => iconSprite;
            set => iconSprite = value;
        }
    }

    [CreateAssetMenu(fileName = nameof(SlotGameIconsSetScriptableObject),
        menuName = nameof(DataSets) + "/" + nameof(SlotGameIconsSetScriptableObject),
        order = 0)]
    public class SlotGameIconsSetScriptableObject : ScriptableObject
    {
        [SerializeField] private IndexedSprite[] slotGameIconsSet;


        public IndexedSprite[] SlotGameIconsSet
        {
            get => slotGameIconsSet;
            set => slotGameIconsSet = value;
        }
    }
}