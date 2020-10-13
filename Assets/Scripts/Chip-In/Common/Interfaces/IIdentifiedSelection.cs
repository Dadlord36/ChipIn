using System;

namespace Common.Interfaces
{
    public interface IIdentifiedSelection
    {
        uint IndexInOrder { get; set; }
        event Action<uint> ItemSelected;
        void Select();
    }
}