using System;
using System.Collections;
using BarcodeScanner;
using BarcodeScanner.Scanner;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ViewModels
{
    public class QrCodeScannerViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private RawImage cameraViewImage;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private TMP_Text textHeader;
        [SerializeField] private AspectRatioFitter fitter;

        [SerializeField] private TMP_InputField rotationXText;
        [SerializeField] private TMP_InputField rotationYText;
        [SerializeField] private TMP_InputField rotationZText;

        [SerializeField] private TMP_InputField scaleXText;
        [SerializeField] private TMP_InputField scaleYText;
        [SerializeField] private TMP_InputField scaleZText;

        private float rotationX => float.Parse(rotationXText.text);
        private float rotationY => float.Parse(rotationYText.text);
        private float rotationZ => float.Parse(rotationZText.text);

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
            //ToDo Make camera image fit the screen
            cameraViewImage.texture = _barcodeScanner.Camera.Texture;

            var imageTransform = cameraViewImage.transform;
            imageTransform.localEulerAngles = _barcodeScanner.Camera.GetEulerAngles();

            var scale = imageTransform.localScale = _barcodeScanner.Camera.GetScale();
            scale.y = (float)  _barcodeScanner.Camera.Height / _barcodeScanner.Camera.Width;
            imageTransform.localScale = scale;

            scaleX = scale.x;
            scaleY = scale.y;
            scaleZ = scale.z;

            // fitter.aspectRatio = (float) _barcodeScanner.Camera.Width / _barcodeScanner.Camera.Height;
            ((RectTransform) imageTransform).sizeDelta = new Vector2(Screen.width, Screen.height);
            textHeader.text = $"Width: {_barcodeScanner.Camera.Width.ToString()}; Height: {_barcodeScanner.Camera.Height.ToString()}";

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
            ((RectTransform) cameraViewImage.transform).localScale = new Vector3(scaleX, scaleY, scaleZ);
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