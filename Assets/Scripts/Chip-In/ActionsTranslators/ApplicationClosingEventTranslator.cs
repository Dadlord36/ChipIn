using System.Threading.Tasks;
using Common.Interfaces;
using Controllers;
using UnityEngine;

namespace ActionsTranslators
{
    [CreateAssetMenu(fileName = nameof(ApplicationClosingEventTranslator),
        menuName = nameof(ActionsTranslators) + "/" + nameof(ApplicationClosingEventTranslator), order = 0)]
    public class ApplicationClosingEventTranslator : BatchedInterfaceInvoker<IApplicationClosingEventReceiver>
    {
        protected override Task InvokeInterfaceMainFunction(IApplicationClosingEventReceiver objectInterface)
        {
            return objectInterface.OnApplicationClosing();
        }
    }
}