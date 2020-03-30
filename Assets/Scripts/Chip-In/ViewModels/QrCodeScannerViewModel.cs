using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace ViewModels
{
    public class QrCodeScannerViewModel : ViewsSwitchingViewModel
    {
        private const string Tag = nameof(QrCodeScannerViewModel);

        [SerializeField] private Object qrPreviewPrefab;
        [SerializeField] private CodeReader qrCodeReader;
        [SerializeField] private PreviewController qrPreviewController;

        private float _restartTime;
        private bool _isInitialized;

        private GameObject _qrPreviewRoot;

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            CreatePreviewObject();
            TryAuthorizeWebCameraAndStartQrReader();
        }

        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
            DeactivateQrScanning();
            DestroyPreviewObject();
        }

        private void CreatePreviewObject()
        {
            _qrPreviewRoot = Instantiate(qrPreviewPrefab) as GameObject;
            _qrPreviewRoot.transform.position = Vector3.zero;
        }

        private void DestroyPreviewObject()
        {
            Destroy(_qrPreviewRoot);
        }

        private void Initialize()
        {
            if (_isInitialized) return;
            _isInitialized = true;
            qrPreviewController.rawimg = _qrPreviewRoot.GetComponentInChildren<RawImage>();
            qrCodeReader.Initialize();
        }

        private void ActivateQrScanning()
        {
            Initialize();
            qrCodeReader.StartWork();
        }

        private void DeactivateQrScanning()
        {
            qrCodeReader.StopWork();
        }

        private void TryAuthorizeWebCameraAndStartQrReader()
        {
            if (Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                ActivateQrScanning();
                return;
            }

            // When the app start, ask for the authorization to use the webcam
            Application.RequestUserAuthorization(UserAuthorization.WebCam).completed += delegate
            {
                if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
                {
                    LogUtility.PrintLog(Tag, "This Webcam library can't work without the webcam authorization");
                    return;
                }

                ActivateQrScanning();
            };
        }
    }
}