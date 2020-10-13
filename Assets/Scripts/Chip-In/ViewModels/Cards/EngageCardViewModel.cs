using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataModels;
using DataModels.Interfaces;
using JetBrains.Annotations;
using Repositories.Local;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class EngageCardViewModel : SelectableListItemBase<MarketInterestDetailsDataModel>
    {
        private const string EmptyFieldText = "-";
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

        private void OnCardWasClicked()
        {
            Select();
        }


        private void ClearIcon()
        {
            Icon = DownloadedSpritesRepository.IconPlaceholder;
        }

        private static string ChooseFieldValue(in string value)
        {
            return string.IsNullOrEmpty(value) ? EmptyFieldText : value;
        }

        public override async Task FillView(MarketInterestDetailsDataModel dataModel, uint dataBaseIndex)
        {
            AsyncOperationCancellationController.CancelOngoingTask();
            ClearIcon();
            SetViewModelFields(dataModel);
            try
            {
                Icon = await DownloadedSpritesRepository.CreateLoadSpriteTask(dataModel.PosterUri, AsyncOperationCancellationController.CancellationToken)
                    .ConfigureAwait(false);
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

        private void SetViewModelFields(IMarketInterestDetailsDataModel dataModel)
        {
            Description = dataModel.Description;
            Age = dataModel.Age;
            Size = dataModel.Size;
            Spirit = dataModel.Spirit;
            MinCapMaxCap = $"$ {dataModel.MinCap.ToString()} - {dataModel.MaxCap.ToString()}";
            Id = dataModel.Id;
            Name = dataModel.Name;
        }
    }
}