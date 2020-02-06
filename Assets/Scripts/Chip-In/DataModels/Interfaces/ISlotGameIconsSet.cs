using UnityEngine;

namespace DataModels.Interfaces
{
    public interface ISlotGameIconsSet
    {
        Sprite First { get; set; }
        Sprite Second { get; set; }
        Sprite Third { get; set; }
        Sprite Fourth { get; set; }
    }
}