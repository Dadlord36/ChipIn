using System;
using System.Threading.Tasks;
using HttpRequests.RequestsProcessors.PutRequests;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Object = UnityEngine.Object;

namespace ViewModels
{
    public class QrCodeScannerViewModel : ViewsSwitchingViewModel
    {
        private const string Tag = nameof(QrCodeScannerViewModel);

        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
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
            qrPreviewController.rawimg = _qrPreviewRoot.GetComponentInChildren<RawImage>();
            if (_isInitialized) return;
            qrCodeReader.Initialize();
            _isInitialized = true;
        }

        private void ActivateQrScanning()
        {
            Initialize();
            CodeReader.OnCodeFinished += CodeReaderOnCodeFinished;
            qrCodeReader.StartWork();
        }

        private void DeactivateQrScanning()
        {
            CodeReader.OnCodeFinished -= CodeReaderOnCodeFinished;
            qrCodeReader.StopWork();
        }

        private void CodeReaderOnCodeFinished(string decodedText)
        {
            Handheld.Vibrate();
            ActivateProduct(decodedText);
        }

        private async void ActivateProduct(string decodedText)
        {
            try
            {
                await UserProductsStaticRequestsProcessor.ActivateProduct(authorisationDataRepository,
                    new ProductQrCode(decodedText));
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
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