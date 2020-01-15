using System;

namespace Behaviours.Games.Interfaces
{
    public interface IInteractiveUintValue
    {
        event Action<uint> Collected;
        void GenerateValue();
    }
}