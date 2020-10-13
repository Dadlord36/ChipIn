using System;
using System.Threading.Tasks;
using DataModels;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class SponsoredAdFullCardViewModel : SelectableListItemBase<SponsoredPosterDataModel>
    {
        private Sprite _background;
        private Sprite _logo;


        [Binding]
        public Sprite Background
        {
            get => _background;
            set
            {
                _background = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public Sprite Logo
        {
            get => _logo;
            set
            {
                _logo = value;
                OnPropertyChanged();
            }
        }

        public SponsoredAdFullCardViewModel() : base(nameof(SponsoredAdFullCardViewModel))
        {
        }

        public override async Task FillView(SponsoredPosterDataModel data, uint dataBaseIndex)
        {
            await base.FillView(data, dataBaseIndex).ConfigureAwait(false);
            try
            {
                Background = await DownloadedSpritesRepository
                        .CreateLoadSpriteTask(data.BackgroundUrl, AsyncOperationCancellationController.CancellationToken)
                        .ConfigureAwait(false);
                if (!string.IsNullOrEmpty(data.LogoUrl))
                    Logo = await DownloadedSpritesRepository
                        .CreateLoadSpriteTask(data.LogoUrl, AsyncOperationCancellationController.CancellationToken)
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
    }
}