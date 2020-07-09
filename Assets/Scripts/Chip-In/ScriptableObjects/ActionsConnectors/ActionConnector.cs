using System;
using UnityEngine;

namespace ScriptableObjects.ActionsConnectors
{
    public interface IActionConnector
    {
        event Action ActionHappened;
        void InvokeAction();
    }

    [CreateAssetMenu(fileName = nameof(ActionConnector),
        menuName = nameof(ActionsConnectors) + "/" + nameof(ActionConnector))]
    public class ActionConnector : ScriptableObject, IActionConnector
    {
        public event Action ActionHappened;

        public void InvokeAction()
        {
            ActionHappened?.Invoke();
        }
    }
}