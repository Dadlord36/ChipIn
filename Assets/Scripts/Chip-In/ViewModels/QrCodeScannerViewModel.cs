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
                AuthorizeWebCamera();
            }
            else
            {
                _barcodeScanner.Camera.Play();
            }

            // Display the camera texture through a RawImage
            _barcodeScanner.OnReady += ProjectCameraOnScreen;
        }

        private void DeactivateQrScanning()
        {
            _barcodeScanner.Camera.Stop();
        }

        void AuthorizeWebCamera()
        {
            // When the app start, ask for the authorization to use the webcam
            Application.RequestUserAuthorization(UserAuthorization.WebCam).completed += delegate
            {
                if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
                {
                    throw new Exception("This Webcam library can't work without the webcam authorization");
                }

                _barcodeScanner.Camera.Play();
            };
        }


        private void ProjectCameraOnScreen(object sender, EventArgs eventArgs)
        {
            cameraViewImage.texture = _barcodeScanner.Camera.Texture;

            var imageTransform = cameraViewImage.transform;
            imageTransform.localEulerAngles = _barcodeScanner.Camera.GetEulerAngles();

            if (Screen.width != _barcodeScanner.Camera.Width && Screen.height != _barcodeScanner.Camera.Height)
            {
                var aspectRation = (float) _barcodeScanner.Camera.Height / _barcodeScanner.Camera.Width;
                imageTransform.localScale = new Vector3(aspectRation, aspectRation, aspectRation);
            }
            else
            {
                imageTransform.localScale = _barcodeScanner.Camera.GetScale();
            }

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