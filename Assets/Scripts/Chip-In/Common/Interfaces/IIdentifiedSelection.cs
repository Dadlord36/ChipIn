using System;

namespace Common.Interfaces
{
    public interface IIdentifiedSelection
    {
        uint IndexInOrder { get; }
        event Action<uint> ItemSelected;
        void Select();
    }
}