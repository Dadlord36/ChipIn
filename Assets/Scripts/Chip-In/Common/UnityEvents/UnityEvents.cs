using System;
using System.Collections.Generic;
using DataModels.Interfaces;
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
    public class StringUnityEvent : UnityEvent<string>
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
    public sealed class IntPointerUnityEvent : UnityEvent<int?>
    {
    }

    [Serializable]
    public sealed class IdentifierUnityEvent : UnityEvent<IIdentifier>
    {
    }

    [Serializable]
    public sealed class SwipeDataUnityEvent : UnityEvent<SwipeDetector.SwipeData>
    {
    }
}