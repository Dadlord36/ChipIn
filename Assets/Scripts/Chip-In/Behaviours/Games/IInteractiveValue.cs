using System;

namespace Behaviours.Games
{
    public interface IInteractiveValue
    {
        event Action<int> Collected;
    }
}