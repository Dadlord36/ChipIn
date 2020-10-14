using System;
using DataModels.Interfaces;
using UnityEngine;

namespace DataModels
{
    [Serializable]
    public class InterestBasicDataModel : IInterestBasicModel
    {
        [SerializeField] private string name;
        [SerializeField] private Sprite logoSprite;
        [SerializeField] private int id;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string PosterUri { get; set; }

        public Sprite LogoSprite
        {
            get => logoSprite;
            set => logoSprite = value;
        }

        public int? Id
        {
            get => id;
            set => id = (int) value;
        }
    }
}