using System;
using DataModels.Interfaces;
using UnityEngine;

namespace DataModels
{
    [Serializable]
    public struct SlotGameIconsSet : ISlotGameIconsSet
    {
        [SerializeField] private Sprite first;
        [SerializeField] private Sprite second;
        [SerializeField] private Sprite third;
        [SerializeField] private Sprite fourth;

        public Sprite First
        {
            get => first;
            set => first = value;
        }

        public Sprite Second
        {
            get => second;
            set => second = value;
        }

        public Sprite Third
        {
            get => third;
            set => third = value;
        }

        public Sprite Fourth
        {
            get => fourth;
            set => fourth = value;
        }
    }
}