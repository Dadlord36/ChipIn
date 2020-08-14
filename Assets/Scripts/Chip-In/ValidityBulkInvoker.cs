using UnityEngine;
using Utilities;

public class ValidityBulkInvoker : MonoBehaviour
{
    private void Start()
    {
        ValidationHelper.CheckIfAllFieldsAreValid(this);
    }
}