using System;

namespace Common.Interfaces
{
    public interface ISelectableObject
    {
        event Action Selected;
        event Action Deselected;
    }
}