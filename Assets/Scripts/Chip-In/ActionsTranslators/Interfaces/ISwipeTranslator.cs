using System;
using InputDetection;

namespace ActionsTranslators.Interfaces
{
    public interface ISwipeTranslator
    {
        event Action<SwipeDetector.SwipeData> Swiped;
    }
}