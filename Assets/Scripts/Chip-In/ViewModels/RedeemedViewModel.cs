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

        private string QrString { get; set; }

        private uint TotalBillNumberAsUint { get; set; }

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
                    authorisationDataRepository, new ProductQrCode(QrString,TotalBillNumberAsUint)).ConfigureAwait(false);
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