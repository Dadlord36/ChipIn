using System;
using System.Threading.Tasks;
using DataModels;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class InterestItemViewModel : SelectableListItemBase<InterestBasicDataModel>
    {
        private string _text;
        private Sprite _icon;

        [Binding]
        public string Text
        {
            get => _text;
            set
            {
                if (value == _text) return;
                _text = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public Sprite Icon
        {
            get => _icon;
            set
            {
                if (Equals(value, _icon)) return;
                _icon = value;
                OnPropertyChanged();
            }
        }

        public InterestItemViewModel() : base(nameof(InterestItemViewModel))
        {
        }

        public override async Task FillView(InterestBasicDataModel data, uint dataBaseIndex)
        {
            await base.FillView(data, dataBaseIndex).ConfigureAwait(false);
            try
            {
                AsyncOperationCancellationController.CancelOngoingTask();

                Icon = DownloadedSpritesRepository.IconPlaceholder;
                Text = data.Name;

                if (data.LogoSprite)
                    Icon = data.LogoSprite;
                else
                    Icon = await DownloadedSpritesRepository.CreateLoadSpriteTask(data.PosterUri, AsyncOperationCancellationController.CancellationToken)
                        .ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
                throw;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}