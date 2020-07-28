using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common.Interfaces;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using DataModels;
using JetBrains.Annotations;
using Repositories.Local;
using UnityEngine;
using UnityWeld.Binding;
using ViewModels.Basic;
using Views.Cards;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class EngageCardViewModel : CorrespondingViewModel<EngageCardView>, IFillingView<MarketInterestDetailsDataModel>, IIdentifiedSelection,
        INotifyPropertyChanged
    {
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
        public uint IndexInOrder { get; set; }
        public event Action<uint> ItemSelected;

        private const string EmptyFieldText = "-";

        private uint _selectedItemDataBaseIndex;
        private uint _size;
        private string _minCapMaxCap = EmptyFieldText;
        private string _age = EmptyFieldText;
        private int? _id;
        private string _name = EmptyFieldText;
        private string _description = EmptyFieldText;
        private Sprite _icon;
        private string _spirit = EmptyFieldText;


        #region IEngageModel implementation

        [Binding]
        public uint Size
        {
            get => _size;
            private set
            {
                if (value == _size) return;
                _size = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string MinCapMaxCap
        {
            get => _minCapMaxCap;
            private set
            {
                if (value == _minCapMaxCap) return;
                _minCapMaxCap = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Age
        {
            get => _age;
            private set
            {
                if (value == _age) return;
                _age = ChooseFieldValue(value);
                OnPropertyChanged();
            }
        }

        [Binding]
        public int? Id
        {
            get => _id;
            private set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Name
        {
            get => _name;
            private set
            {
                if (value == _name) return;
                _name = ChooseFieldValue(value);
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Description
        {
            get => _description;
            private set
            {
                if (value == _description) return;
                _description = ChooseFieldValue(value);
                OnPropertyChanged();
            }
        }

        [Binding]
        public Sprite Icon
        {
            get => _icon;
            private set
            {
                if (Equals(value, _icon)) return;
                _icon = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Spirit
        {
            get => _spirit;
            private set
            {
                if (value == _spirit) return;
                _spirit = ChooseFieldValue(value);
                OnPropertyChanged();
            }
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

        private static string ChooseFieldValue(in string value)
        {
            return string.IsNullOrEmpty(value) ? EmptyFieldText : value;
        }

        public Task FillView(MarketInterestDetailsDataModel dataModel, uint dataBaseIndex)
        {
            OperationCancellationController.CancelOngoingTask();
            ClearIcon();
            _selectedItemDataBaseIndex = dataBaseIndex;
            Description = dataModel.Description;
            Age = dataModel.Age;
            Size = dataModel.Size;
            Spirit = dataModel.Spirit;
            MinCapMaxCap = $"$ {dataModel.MinCap.ToString()} - {dataModel.MaxCap.ToString()}";
            Id = dataModel.Id;
            Name = dataModel.Name;
            return downloadedSpritesRepository.CreateLoadSpriteTask(dataModel.PosterUri, OperationCancellationController.CancellationToken)
                .ContinueWith(delegate(Task<Sprite> getSpriteTask) { Icon = getSpriteTask.GetAwaiter().GetResult(); },
                    continuationOptions: TaskContinuationOptions.OnlyOnRanToCompletion, scheduler: downloadedSpritesRepository.MainThreadScheduler,
                    cancellationToken: OperationCancellationController.CancellationToken);
        }

        private void OnItemSelected(uint index)
        {
            ItemSelected?.Invoke(index);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}