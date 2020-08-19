using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Repositories.Remote;
using ScriptableObjects.CardsControllers;
using UnityEngine;
using UnityWeld.Binding;
using Views;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels
{
    [Binding]
    public sealed class CartViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        [SerializeField] private UserProductsRepository userProductsRepository;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private InfoCardController infoCardController;

        private bool _infoCanBeShown;

        [Binding]
        public bool InfoCanBeShown
        {
            get => _infoCanBeShown;
            set
            {
                if (value == _infoCanBeShown) return;
                _infoCanBeShown = value;
                OnPropertyChanged();
            }
        }

        public CartViewModel() : base(nameof(CartViewModel))
        {
        }

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            InitializeAsyncInChildren(transform);
        }

        public static Task InitializeAsyncInChildren(Transform givenTransform)
        {
            var tasks = new List<Task>();
            foreach (var initializeAsync in givenTransform.GetComponentsInChildren<IInitializeAsync>())
            {
                tasks.Add(initializeAsync.Initialize());
            }

            return tasks.Count == 0 ? Task.CompletedTask : Task.WhenAll(tasks);
        }


        [Binding]
        public async Task ShowInfo_OnButtonClick()
        {
        }

        [Binding]
        public void SwitchItemSelection()
        {
        }

        [Binding]
        public void SwitchToQrCodeView()
        {
            SwitchToView(nameof(QrCodeView));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}