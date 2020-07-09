using System;

namespace Common.Interfaces
{
    public interface IIdentifiedSelection
    {
        event Action<uint> ItemSelected;
    }
}