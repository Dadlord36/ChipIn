using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Common.Interfaces;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using DataModels;
using Repositories.Local;
using UnityEngine;
using UnityWeld.Binding;
using ViewModels.Basic;
using Views.Cards;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class EngageCardViewModel : CorrespondingViewModel<EngageCardView>, IEngageModel,
        IFillingView<MarketInterestDetailsDataModel>, IIdentifiedSelection, INotifyPropertyChanged
    {
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
        public event Action<uint> ItemSelected;

        private readonly EngageCardDataModel _cardData = new EngageCardDataModel();
        private uint _selectedItemDataBaseIndex;


        public event PropertyChangedEventHandler PropertyChanged
        {
            add => _cardData.PropertyChanged += value;
            remove => _cardData.PropertyChanged -= value;
        }


        #region IEngageModel implementation

        [Binding]
        public uint Size
        {
            get => _cardData.Size;
            set => _cardData.Size = value;
        }

        [Binding]
        public uint MinCap
        {
            get => _cardData.MinCap;
            set => _cardData.MinCap = value;
        }

        [Binding]
        public uint MaxCap
        {
            get => _cardData.MaxCap;
            set => _cardData.MaxCap = value;
        }

        [Binding]
        public string Age
        {
            get => _cardData.Age;
            set => _cardData.Age = value;
        }

        [Binding]
        public int? Id
        {
            get => _cardData.Id;
            set => _cardData.Id = value;
        }

        [Binding]
        public string Name
        {
            get => _cardData.Name;
            set => _cardData.Name = value;
        }

        [Binding]
        public string Description
        {
            get => _cardData.Description;
            set => _cardData.Description = value;
        }

        [Binding]
        public Sprite Icon
        {
            get => _cardData.Icon;
            set => _cardData.Icon = value;
        }

        [Binding]
        public string Spirit
        {
            get => _cardData.Spirit;
            set => _cardData.Spirit = value;
        }

        #endregion

        public EngageCardViewModel() : base(nameof(EngageCardViewModel))
        {
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            RelatedView.WasClicked += OnCardWasClicked;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            RelatedView.WasClicked -= OnCardWasClicked;
        }

        private void OnCardWasClicked()
        {
            OnItemSelected(_selectedItemDataBaseIndex);
        }

        private void ClearIcon()
        {
            Icon = null;
        }
        
        public Task FillView(MarketInterestDetailsDataModel dataModel, uint dataBaseIndex)
        {
            OperationCancellationController.CancelOngoingTask();
            ClearIcon();
            _selectedItemDataBaseIndex = dataBaseIndex;
            Age = dataModel.Age;
            Description = dataModel.Description;
            Size = dataModel.Size;
            Spirit = dataModel.Spirit;
            MaxCap = dataModel.MaxCap;
            MinCap = dataModel.MinCap;
            Id = dataModel.Id;
            Name = dataModel.Name; ;
            return downloadedSpritesRepository.CreateLoadSpriteTask(dataModel.PosterUri, OperationCancellationController.CancellationToken).
                ContinueWith(delegate(Task<Sprite> getSpriteTask)
                    {
                        Icon = getSpriteTask.Result;
                    }
                , continuationOptions: TaskContinuationOptions.OnlyOnRanToCompletion, scheduler:downloadedSpritesRepository.MainThreadScheduler,
                cancellationToken: OperationCancellationController.CancellationToken);
        }

        private void OnItemSelected(uint index)
        {
            ItemSelected?.Invoke(index);
        }
    }
}