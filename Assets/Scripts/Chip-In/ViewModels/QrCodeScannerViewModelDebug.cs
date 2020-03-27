using System;
using System.Collections;
using BarcodeScanner;
using BarcodeScanner.Scanner;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ViewModels
{
    public class QrCodeScannerViewModelDebug : ViewsSwitchingViewModel
    {
        [SerializeField] private RawImage cameraViewImage;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private TMP_Text textHeader;

        [SerializeField] private TMP_InputField rotationXText;
        [SerializeField] private TMP_InputField rotationYText;
        [SerializeField] private TMP_InputField rotationZText;

        [SerializeField] private TMP_InputField scaleXText;
        [SerializeField] private TMP_InputField scaleYText;
        [SerializeField] private TMP_InputField scaleZText;

        [SerializeField] private TMP_InputField widthText;
        [SerializeField] private TMP_InputField heightText;

        private float rotationX
        {
            get { return float.Parse(rotationXText.text); }
            set { rotationXText.text = value.ToString(); }
        }

        private float rotationY
        {
            get { return float.Parse(rotationYText.text); }
            set { rotationYText.text = value.ToString(); }
        }

        private float rotationZ
        {
            get { return float.Parse(rotationZText.text); }
            set { rotationZText.text = value.ToString(); }
        }

        private float scaleX
        {
            get { return float.Parse(scaleXText.text); }
            set { scaleXText.text = value.ToString(); }
        }

        private float scaleY
        {
            get { return float.Parse(scaleYText.text); }
            set { scaleYText.text = value.ToString(); }
        }

        private float scaleZ
        {
            get { return float.Parse(scaleZText.text); }
            set { scaleZText.text = value.ToString(); }
        }

        private float Width
        {
            get { return float.Parse(widthText.text); }
            set { widthText.text = value.ToString(); }
        }

        private float Height
        {
            get { return float.Parse(heightText.text); }
            set { heightText.text = value.ToString(); }
        }


        private IScanner _barcodeScanner;
        private float _restartTime;

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            ActivateQrScanning();
        }

        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
            DeactivateQrScanning();
        }

        private void Start()
        {
            ActivateQrScanning();
        }

        private void ActivateQrScanning()
        {
            if (_barcodeScanner == null)
                _barcodeScanner = new Scanner(new ScannerSettings
                {
                    WebcamRequestedWidth = Screen.width,
                    WebcamRequestedHeight = Screen.height
                });

            if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                StartCoroutine(AuthorizeWebCamera());
            }

            _barcodeScanner.Camera.Play();
            // Display the camera texture through a RawImage
            _barcodeScanner.OnReady += ProjectCameraOnScreen;
        }

        private void DeactivateQrScanning()
        {
            _barcodeScanner.Camera.Stop();
        }

        IEnumerator AuthorizeWebCamera()
        {
            // When the app start, ask for the authorization to use the webcam
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);

            if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                throw new Exception("This Webcam library can't work without the webcam authorization");
            }
        }


        private void ProjectCameraOnScreen(object sender, EventArgs eventArgs)
        {
            cameraViewImage.texture = _barcodeScanner.Camera.Texture;

            var imageTransform = cameraViewImage.transform;

            var angles = imageTransform.localEulerAngles = _barcodeScanner.Camera.GetEulerAngles();
            var aspectRation = (float) _barcodeScanner.Camera.Height / _barcodeScanner.Camera.Width;
            var scale = imageTransform.localScale = new Vector3(aspectRation, aspectRation, aspectRation);
            // scale.y = (float)  _barcodeScanner.Camera.Height / _barcodeScanner.Camera.Width;
            // imageTransform.localScale = scale;

            scaleX = scale.x;
            scaleY = scale.y;
            scaleZ = scale.z;

            rotationX = angles.x;
            rotationY = angles.y;
            rotationZ = angles.z;

            // fitter.aspectRatio = (float) _barcodeScanner.Camera.Width / _barcodeScanner.Camera.Height;
            var size = ((RectTransform) imageTransform).sizeDelta = new Vector2(_barcodeScanner.Camera.Width, _barcodeScanner.Camera.Height);
            textHeader.text = $"Width: {_barcodeScanner.Camera.Width.ToString()}; Height: {_barcodeScanner.Camera.Height.ToString()}";

            Width = size.x;
            Height = size.y;

            _restartTime = Time.realtimeSinceStartup;
        }

        /// <summary>
        /// Start a scan and wait for the callback (wait 1s after a scan success to avoid scanning multiple time the same element)
        /// </summary>
        private void StartScanner()
        {
            _barcodeScanner.Scan((barCodeType, barCodeValue) =>
            {
                _barcodeScanner.Stop();
                if (textHeader.text.Length > 250)
                {
                    textHeader.text = "";
                }

                textHeader.text += "Found: " + barCodeType + " / " + barCodeValue + "\n";
                _restartTime += Time.realtimeSinceStartup + 1f;

                // Feedback
                if (audioSource)
                    audioSource.Play();

#if UNITY_ANDROID || UNITY_IOS
                Handheld.Vibrate();
#endif
            });
        }

        /// <summary>
        /// The Update method from unity need to be propagated
        /// </summary>
        void Update()
        {
            if (_barcodeScanner == null) return;

            // ((RectTransform) cameraViewImage.transform).eulerAngles = new Vector3(rotationX, rotationY, rotationZ);
            // ((RectTransform) cameraViewImage.transform).localScale = new Vector3(scaleX, scaleY, scaleZ);
            // ((RectTransform) cameraViewImage.transform).sizeDelta = new Vector2(Width, Height);
            _barcodeScanner.Update();

            // Check if the Scanner need to be started or restarted
            if (_restartTime != 0 && _restartTime < Time.realtimeSinceStartup)
            {
                StartScanner();
                _restartTime = 0;
            }
        }
    }
}