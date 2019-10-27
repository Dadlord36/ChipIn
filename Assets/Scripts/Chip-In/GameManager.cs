using HttpRequests;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        ApiHelper.InitializeClient();
    }

    private void OnDisable()
    {
        ApiHelper.Dispose();
    }
}