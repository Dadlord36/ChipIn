using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private Object[] objectsToInitialize;

    private void Awake()
    {
        foreach (var o in objectsToInitialize)
        {
            if (o is IInitialize initializer)
            {
                initializer.Initialize();
            }
        }
    }
}

public interface IInitialize
{
    void Initialize();
}