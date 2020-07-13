using UnityEngine;

namespace Common.ReversiveActions
{
    public abstract class DoUndoBehaviour : MonoBehaviour
    {
        public abstract void Do();
        public abstract void Undo();
    }
}