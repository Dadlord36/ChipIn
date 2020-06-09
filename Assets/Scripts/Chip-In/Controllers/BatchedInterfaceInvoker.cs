using UnityEngine;

namespace Controllers
{
    public abstract class BatchedInterfaceInvoker<TInterface> : ScriptableObject where TInterface : class
    {
        [SerializeField] private Object[] restorableRepositories;

        public void InvokeInterfaceMainFunction()
        {
            for (int i = 0; i < restorableRepositories.Length; i++)
            {
                if (restorableRepositories[i] is TInterface correspondingInterface)
                    InvokeInterfaceMainFunction(correspondingInterface);
            }
        }

        protected abstract void InvokeInterfaceMainFunction(TInterface objectInterface);
    }
}