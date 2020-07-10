using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;
using Views.ViewElements;

namespace ViewModels
{
    [Binding]
    public sealed class TransactViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private DotsDiagram dotsDiagram;

        private bool _alternativeDiagram;

        [Binding]
        public bool AlternativeDiagram
        {
            get => _alternativeDiagram;
            set
            {
                if (value == _alternativeDiagram) return;
                _alternativeDiagram = value;
                OnPropertyChanged();
            }
        }

        public TransactViewModel() : base(nameof(TransactViewModel))
        {
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
                await TryUpdateRadarViewData();
            }
            catch (Exception e)
            {
               LogUtility.PrintLogException(e);
                throw;
            }
        }


        [Binding]
        public void Redemption_OnClick()
        {
            SwitchToQrCodeForm();
        }

        private void SwitchToQrCodeForm()
        {
            SwitchToView(nameof(QrCodeScannerView));
        }

        [Binding]
        public void SwitchDiagram()
        {
            AlternativeDiagram = !AlternativeDiagram;
        }


        private async Task TryUpdateRadarViewData()
        {
            try
            {
                var response = await MerchantMarketRequestsStaticProcessor.GetRadarData(out OperationCancellationController.TasksCancellationTokenSource,
                    authorisationDataRepository);
                if (!response.Success) return;

                var responseModel = response.ResponseModelInterface;
                if (responseModel.Data.Points == null)
                {
                    LogUtility.PrintLog(Tag, "There are no points were returned");
                    return;
                }

                dotsDiagram.SetDataToVisualize(responseModel.Data);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}