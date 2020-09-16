using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HttpRequests.RequestsProcessors.PutRequests;
using JetBrains.Annotations;
using Repositories.Local;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects.CardsControllers;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Basic;
using Views;

namespace ViewModels
{
    [Binding]
    public sealed class RedeemedViewModel : CorrespondingViewsSwitchingViewModel<RedeemedView>, INotifyPropertyChanged
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
        [SerializeField] private AlertCardController alertCardController;

        private Texture2D _iconTexture;
        private string _productDescription;

        [Binding]
        public string ProductDescription
        {
            get => _productDescription;
            set
            {
                if (value == _productDescription) return;
                _productDescription = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public Texture2D IconSprite
        {
            get => _iconTexture;
            set
            {
                _iconTexture = value;
                OnPropertyChanged();
            }
        }

        private string QrString { get; set; }

        private uint TotalBillNumberAsUint { get; set; }

        [Binding]
        public string TotalBillNumber
        {
            get => TotalBillNumberAsUint.ToString();
            set
            {
                if (string.IsNullOrEmpty(value) || value == TotalBillNumber) return;
                TotalBillNumberAsUint = uint.Parse(value);
                OnPropertyChanged();
            }
        }

        public RedeemedViewModel() : base(nameof(RedeemedViewModel))
        {
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            ResetPropertiesState();
            QrString = RelatedView.FormTransitionBundle.TransitionData as string;
            try
            {
                await RefillViewsConsumableData().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private async Task RefillViewsConsumableData()
        {
            OperationCancellationController.CancelOngoingTask();
            try
            {
                var response = await UserProductsStaticRequestsProcessor.GetUserProductByQr(
                        out OperationCancellationController.TasksCancellationTokenSource, authorisationDataRepository, QrString)
                    .ConfigureAwait(false);
                
                if (response.Success)
                {
                    var productData = response.ResponseModelInterface.ProductsData;
                    ProductDescription = productData.Description;
                    IconSprite = await downloadedSpritesRepository.CreateLoadTexture2DTask(productData.PosterUri,
                        OperationCancellationController.CancellationToken).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        [Binding]
        public async void OkButton_OnClick()
        {
            try
            {
                IsAwaitingProcess = true;
                var response = await UserProductsStaticRequestsProcessor.ActivateProduct(out OperationCancellationController.TasksCancellationTokenSource,
                        authorisationDataRepository, new ProductQrCode(QrString, TotalBillNumberAsUint))
                    .ConfigureAwait(false);

                alertCardController.ShowAlertWithText(response.Success ? "Product was activated successfully" : "Failed to activate the product");
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }
            finally
            {
                IsAwaitingProcess = false;
            }
        }

        [Binding]
        public void CancelButton_OnClick()
        {
            SwitchToView(nameof(TransactView));
        }

        private void ResetPropertiesState()
        {
            TotalBillNumber = "0";
            QrString = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}