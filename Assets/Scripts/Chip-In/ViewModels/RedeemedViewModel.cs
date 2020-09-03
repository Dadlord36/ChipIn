using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HttpRequests.RequestsProcessors.PutRequests;
using JetBrains.Annotations;
using Repositories.Remote;
using RequestsStaticProcessors;
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
        private int _totalBillNumber;

        [Binding]
        public string TotalBillNumber
        {
            get => _totalBillNumber.ToString();
            set
            {
                if (string.IsNullOrEmpty(value) || value == TotalBillNumber) return;
                _totalBillNumber = int.Parse(value);
                OnPropertyChanged();
            }
        }

        private string QrString { get; set; }

        public RedeemedViewModel() : base(nameof(RedeemedViewModel))
        {
        }

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            ResetPropertiesState();
            QrString = RelatedView.FormTransitionBundle.TransitionData as string;
        }

        [Binding]
        public async void OkButton_OnClick()
        {
            try
            {
                IsAwaitingProcess = true;
                await UserProductsStaticRequestsProcessor.ActivateProduct(out OperationCancellationController.TasksCancellationTokenSource,
                    authorisationDataRepository, new ProductQrCode(QrString));
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}