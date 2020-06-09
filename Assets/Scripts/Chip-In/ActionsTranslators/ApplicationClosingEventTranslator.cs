using Common.Interfaces;
using Controllers;
using UnityEngine;

namespace ActionsTranslators
{
    [CreateAssetMenu(fileName = nameof(ApplicationClosingEventTranslator),
        menuName = nameof(ActionsTranslators) + "/" + nameof(ApplicationClosingEventTranslator), order = 0)]
    public class ApplicationClosingEventTranslator : BatchedInterfaceInvoker<IApplicationClosingEventReceiver>
    {
        protected override void InvokeInterfaceMainFunction(IApplicationClosingEventReceiver objectInterface)
        {
            objectInterface.OnApplicationClosing();
        }
    }
}