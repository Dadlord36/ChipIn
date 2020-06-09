using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Controllers
{
    public abstract class BatchedInterfaceInvoker<TInterface> : ScriptableObject where TInterface : class
    {
        [SerializeField] private Object[] restorableRepositories;

        public Task InvokeInterfaceMainFunction()
        {
            var tasks = new List<Task>(restorableRepositories.Length);
            for (int i = 0; i < restorableRepositories.Length; i++)
            {
                if (restorableRepositories[i] is TInterface correspondingInterface)
                {
                    tasks.Add(InvokeInterfaceMainFunction(correspondingInterface));
                }
            }
            return Task.WhenAll(tasks); 
        }

        protected abstract Task InvokeInterfaceMainFunction(TInterface objectInterface);
    }
}