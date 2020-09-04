using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HttpRequests.RequestsProcessors.PutRequests;
using JetBrains.Annotations;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityWeld.Binding;
using Utilities;
using Views;
using Object = UnityEngine.Object;
using RectTransformUtility = Utilities.RectTransformUtility;

namespace ViewModels
{
    [Binding]
    public sealed class QrCodeScannerViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private Object qrPreviewPrefab;
        [SerializeField] private CodeReader qrCodeReader;
        [SerializeField] private PreviewController qrPreviewController;
        [SerializeField] private Transform cameraViewContainer;

        public UnityEvent scanningHasFailed;

        private GameObject _qrPreviewRoot;
        private bool _isBusy;

        [Binding]
        public bool IsBusy
        {
            get => _isBusy;
            private set
            {
                if (value == _isBusy) return;
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public QrCodeScannerViewModel() : base(nameof(QrCodeScannerViewModel))
        {
        }

        [Binding]
        public async void ScanButton_OnClick()
        {
            try
            {
                IsBusy = true;
                var result = await qrCodeReader.DecodeQRAsync().ConfigureAwait(true);
                if (result != null && !string.IsNullOrEmpty(result.Text))
                {
                    ProcessDecodedString(result.Text);
                }
                else
                {
                    OnScanningHasFailed();
                }
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }
        }

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            TryAuthorizeWebCameraAndStartQrReader();
        }


        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
            qrPreviewController.StopWork();
            DestroyPreviewObject();
        }

        private void ProcessDecodedString(string decodedString)
        {
            Handheld.Vibrate();
            SwitchToView(nameof(RedeemedView), new FormsTransitionBundle(decodedString));
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
            _qrPreviewRoot.transform.SetParent(cameraViewContainer);
            qrCodeReader.previewctr = qrPreviewController;
            qrCodeReader.Initialize();
            RectTransformUtility.Centralize(_qrPreviewRoot.transform as RectTransform);
            qrPreviewController.StartWork();
        }

        private void ActivateQrScanning()
        {
            CreatePreviewObject();
            Initialize();
        }

        private Task ActivateProductAsync(string decodedText)
        {
            return UserProductsStaticRequestsProcessor.ActivateProduct(out OperationCancellationController.TasksCancellationTokenSource,
                authorisationDataRepository, new ProductQrCode(decodedText));
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


        private void OnScanningHasFailed()
        {
            scanningHasFailed.Invoke();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}