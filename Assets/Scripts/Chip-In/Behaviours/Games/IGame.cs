using System;

namespace Behaviours.Games
{
    public interface IGame
    {
        event Action GameComplete;
    }
}