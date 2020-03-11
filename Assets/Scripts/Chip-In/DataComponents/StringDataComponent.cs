using UnityEngine;

namespace DataComponents
{
    public class StringDataComponent : MonoBehaviour
    {
        [SerializeField] private string stringData;

        public string StringData => stringData;

        public static string GetStringDataFromComponent(Component component)
        {
            return component.GetComponent<StringDataComponent>().StringData;
        }
    }
}