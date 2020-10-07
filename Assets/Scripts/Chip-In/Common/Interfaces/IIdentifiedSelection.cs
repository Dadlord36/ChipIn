using System;

namespace Common.Interfaces
{
    public interface IIdentifiedSelection<T>
    {
        uint IndexInOrder { get; }
        event Action<T> ItemSelected;
        void Select();
    }
}