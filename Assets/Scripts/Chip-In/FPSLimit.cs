using UnityEngine;

public class FpsLimit : MonoBehaviour
{
    [SerializeField] private int maxFPS=60;
    public void ChangeFrameRate()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = maxFPS;
    }

    private void Start()
    {
        ChangeFrameRate();
    }
}