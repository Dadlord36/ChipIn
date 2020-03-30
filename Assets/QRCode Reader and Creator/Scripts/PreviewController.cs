using UnityEngine;
using UnityEngine.UI;

public class PreviewController : MonoBehaviour
{
    private const string Tag = nameof(PreviewController);
    
    public DeviceCamera Devicecamera => mCamera;
    DeviceCamera mCamera;
    public RawImage rawimg;

    public void Initialize()
    {
        mCamera = new DeviceCamera();
    }
    
    public void StartWork()
    {
        if (mCamera != null)
        {
            mCamera.Play();
            rawimg.texture = mCamera.preview;
            InvokeRepeating(nameof(CheckRunningCamera), 0.1f, 0.05f);
        }
    }

    /// <summary>
    /// Checks the running camera.
    /// </summary>
    void CheckRunningCamera()
    {
        if (mCamera != null && mCamera.isPlaying() && mCamera.Width() > 100)
        {
            CancelInvoke(nameof(CheckRunningCamera));
            rawimg.transform.localEulerAngles = mCamera.GetRotation();
            rawimg.transform.localScale = mCamera.getVideoScale();
            var textureWidthAndHeight = mCamera.getSize();

            RectTransform component = rawimg.GetComponent<RectTransform>();
            component.sizeDelta = textureWidthAndHeight;
        }
    }

    /// <summary>
    /// Stops the work.
    /// </summary>
    public void StopWork()
    {
        if (mCamera != null)
        {
            mCamera.Stop();
            CancelInvoke(nameof(CheckRunningCamera));
            rawimg.texture = null;
        }
    }

    /// <summary>
    /// open the rear camera.
    /// </summary>
    /// <param name="val">If set to <c>true</c> value.</param>
    public void RearCamera(bool val = false)
    {
        if (val && mCamera != null)
        {
            mCamera.ActiveRearCamera();
            rawimg.texture = mCamera.preview;
            InvokeRepeating(nameof(CheckRunningCamera), 0.1f, 0.05f);
        }
    }

    /// <summary>
    /// open the front camera.
    /// </summary>
    /// <param name="val">If set to <c>true</c> value.</param>
    public void FrontCamera(bool val = false)
    {
        if (val && mCamera != null)
        {
            mCamera.ActiveFrontCamera();
            rawimg.texture = mCamera.preview;
            InvokeRepeating(nameof(CheckRunningCamera), 0.1f, 0.05f);
        }
    }


}