using UnityEngine;

namespace Views.InteractiveWindows.Interfaces
{
    public interface IInfoPanelData
    {
        Sprite ItemLabel { get; set; }
        string ItemName { get; set; }
        string ItemType { get; set; }
        string ItemDescription { get; set; }
    }
}