using System;

namespace Game.TwitchSettingsMenu.Common.Interfaces
{
    public interface ISelectableObject
    {
        event Action Selected;
        event Action Deselected;
    }
}