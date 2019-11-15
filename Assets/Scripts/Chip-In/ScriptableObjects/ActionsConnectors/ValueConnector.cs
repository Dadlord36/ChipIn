using System;
using UnityEngine;

namespace ScriptableObjects.ActionsConnectors
{
    [CreateAssetMenu(fileName = nameof(ValueConnector), menuName = "ActionsConnectors/" + nameof(ValueConnector),
        order = 0)]
    public sealed class ValueConnector : ScriptableObject
    {
        public event Action<int> ValueChanged;

        public void OnValueChanged(int newValue)
        {
            ValueChanged?.Invoke(newValue);
        }
    }
    
}