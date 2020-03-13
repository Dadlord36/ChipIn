using Repositories.Local;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = nameof(DataRestorationController), menuName = nameof(Controllers) + "/" + nameof(DataRestorationController), order = 0)]
    public class DataRestorationController : ScriptableObject, IRestorable
    {
        [SerializeField] private Object[] restorableRepositories;


        public void Restore()
        {
            for (int i = 0; i < restorableRepositories.Length; i++)
            {
                (restorableRepositories[i] as IRestorable)?.Restore();
            }
        }
    }
}