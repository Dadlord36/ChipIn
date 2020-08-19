using System;
using System.Threading.Tasks;

namespace Behaviours.Games
{
    public interface IGame
    {
        event Action GameComplete;
        Task InitializeCoinsGame();
    }
}