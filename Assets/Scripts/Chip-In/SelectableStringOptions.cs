using UnityEngine;

public class SelectableStringOptions : MonoBehaviour
{
    [SerializeField] protected string[] options;

    public string GetOptionAtIndex(int index)
    {
        return this[index];
    }

    public string this[int index] => options[index];
}