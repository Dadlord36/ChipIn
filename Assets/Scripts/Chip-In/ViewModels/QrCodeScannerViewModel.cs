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
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private Object qrPreviewPrefab;
        [SerializeField] private CodeReader qrCodeReader;
        [SerializeField] private PreviewController qrPreviewController;

        private float _restartTime;
        private bool _isInitialized;

        private GameObject _qrPreviewRoot;

        public QrCodeScannerViewModel() : base(nameof(QrCodeScannerViewModel))
        {
        }
        
        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
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
            CreatePreviewObject();
            Initialize();
            CodeReader.OnCodeFinished += CodeReaderOnCodeFinished;
            qrCodeReader.StartWork();
        }

        private void DeactivateQrScanning()
        {
            CodeReader.OnCodeFinished -= CodeReaderOnCodeFinished;
            qrCodeReader.StopWork();
        }

        private async void CodeReaderOnCodeFinished(string decodedText)
        {
            Handheld.Vibrate();
            try
            {
                await ActivateProduct(decodedText);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private async Task ActivateProduct(string decodedText)
        {
            try
            {
                await UserProductsStaticRequestsProcessor.ActivateProduct(out OperationCancellationController.TasksCancellationTokenSource,
                    authorisationDataRepository, new ProductQrCode(decodedText));
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