using System;
using System.Collections.Generic;
using InputDetection;
using UnityEngine.Events;

namespace Common.UnityEvents
{
    [Serializable]
    public sealed class FloatUnityEvent : UnityEvent<float>
    {
    }

    [Serializable]
    public abstract class ReadOnlyListUnityEvent<T> : UnityEvent<IReadOnlyList<T>>
    {
    }

    [Serializable]
    public sealed class UintUnityEvent : UnityEvent<uint>
    {
    }

    [Serializable]
    public sealed class IntUnityEvent : UnityEvent<int>
    {
    }

    [Serializable]
    public sealed class SwipeDataUnityEvent : UnityEvent<SwipeDetector.SwipeData>
    {
    } 
}