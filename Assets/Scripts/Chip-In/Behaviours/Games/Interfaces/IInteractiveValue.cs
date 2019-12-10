using System;

namespace Behaviours.Games.Interfaces
{
    public interface IInteractiveValue
    {
        event Action<int> Collected;
        void GenerateValue();
    }
}