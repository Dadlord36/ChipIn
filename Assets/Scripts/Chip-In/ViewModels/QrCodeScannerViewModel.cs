using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Repositories.Remote;
using RequestsStaticProcessors;
using Tasking;
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
        [SerializeField, Range(0, 600)] private int scanningRepeatFrequencyInMilliseconds;

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

        private async void ActivateScanningRepeatedly()
        {
            while (true)
            {
                try
                {
                    IsBusy = true;
                    if (OperationCancellationController.CancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    var result = await await TasksFactories.ExecuteOnMainThreadTaskAsync(qrCodeReader.DecodeQRAsync)
                        .ConfigureAwait(false);
                    if (result != null && !string.IsNullOrEmpty(result.Text))
                    {
                        IsBusy = false;
                        FeedbackThatQrIsValid();
                        if (await CheckIfQrCodeStringIsValid(result.Text).ConfigureAwait(false))
                        {
                            FeedbackThatQrIsValid();
                            SwitchToRedeemView(result.Text);
                            break;
                        }

                        OnScanningHasFailed();
                    }

                    await Task.Delay(scanningRepeatFrequencyInMilliseconds, OperationCancellationController.CancellationToken).ConfigureAwait(false);
                }
                catch (DivideByZeroException e)
                {
                    LogUtility.PrintLogWarning(Tag,e.Message);
                }
                catch (IndexOutOfRangeException e)
                {
                    LogUtility.PrintLogWarning(Tag,e.Message);
                }
                catch (Exception e)
                {
                    LogUtility.PrintLogException(e);
                }
            }
        }

        private static void FeedbackThatQrIsValid()
        {
            TasksFactories.ExecuteOnMainThread(Handheld.Vibrate);
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

        private async Task<bool> CheckIfQrCodeStringIsValid(string qrCode)
        {
            try
            {
                var response = await UserProductsStaticRequestsProcessor.GetUserProductByQr(out _, authorisationDataRepository, qrCode)
                    .ConfigureAwait(false);
                return response.Success;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private void SwitchToRedeemView(string decodedProductQr)
        {
            SwitchToView(nameof(RedeemedView), new FormsTransitionBundle(decodedProductQr));
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
            ActivateScanningRepeatedly();
        }

        private void TryAuthorizeWebCameraAndStartQrReader()
        {
            if (NativeCamera.CheckPermission() == NativeCamera.Permission.Granted)
            {
                ActivateQrScanning();
            }
            else
            {
                AskForCameraPermission();
            }
        }

        private bool _wentToAppSettings;

        private void AskForCameraPermission()
        {
            switch (NativeCamera.RequestPermission())
            {
                case NativeCamera.Permission.Denied:
                {
                    if (NativeCamera.CanOpenSettings())
                    {
                        _wentToAppSettings = true;
                        NativeCamera.OpenSettings();
                    }
                    else
                    {
                        LogUtility.PrintLog(Tag, "This Webcam library can't work without the webcam authorization");
                    }

                    break;
                }
                case NativeCamera.Permission.Granted:
                {
                    ActivateQrScanning();
                    break;
                }
                case NativeCamera.Permission.ShouldAsk:
                {
                    AskForCameraPermission();
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus || !_wentToAppSettings) return;
            _wentToAppSettings = false;
            TryAuthorizeWebCameraAndStartQrReader();
        }


        private void OnScanningHasFailed()
        {
            TasksFactories.ExecuteOnMainThread(() => { scanningHasFailed.Invoke(); });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}