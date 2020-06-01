using System;
using UnityEngine.Events;

namespace Common.UnityEvents
{
    [Serializable]
    public class FloatUnityEvent : UnityEvent<float>
    {
    }

    [Serializable]
    public class UintUnityEvent : UnityEvent<uint>
    {
    }

    [Serializable]
    public class IntUnityEvent : UnityEvent<int>
    {
    }
}