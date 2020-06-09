using Repositories.Local;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = nameof(DataRestorationController), menuName = nameof(Controllers) + "/" + nameof(DataRestorationController),
        order = 0)]
    public class DataRestorationController : BatchedInterfaceInvoker<IRestorable>
    {
        protected override void InvokeInterfaceMainFunction(IRestorable objectInterface)
        {
            objectInterface.Restore();
        }
    }
}