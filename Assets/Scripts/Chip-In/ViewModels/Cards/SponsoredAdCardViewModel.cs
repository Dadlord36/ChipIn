using System;
using System.Threading.Tasks;
using DataModels;
using Factories;
using Repositories.Local;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class SponsoredAdCardViewModel : SwitchableForm<SponsoredAdDataModel>
    {
        private Sprite _logoSprite;

        [Binding]
        public Sprite LogoSprite
        {
            get => _logoSprite;
            set
            {
                if (Equals(value, _logoSprite)) return;
                _logoSprite = value;
                OnPropertyChanged();
            }
        }

        private Sprite _backgroundSprite;

        [Binding]
        public Sprite BackgroundTexture
        {
            get => _backgroundSprite;
            private set
            {
                if (Equals(value, _backgroundSprite)) return;
                _backgroundSprite = value;
                OnPropertyChanged();
            }
        }
        
        public SponsoredAdCardViewModel() : base(nameof(SponsoredAdCardViewModel))
        {
        }

        public override async Task FillView(SponsoredAdDataModel data, uint dataBaseIndex)
        {
            await base.FillView(data, dataBaseIndex).ConfigureAwait(false);
            try
            {
                IndexInOrder = dataBaseIndex;
                BackgroundTexture = await DownloadedSpritesRepository.CreateLoadSpriteTask(data.PosterUri,
                        AsyncOperationCancellationController.CancellationToken)
                    .ConfigureAwait(false);
                
                if (!string.IsNullOrEmpty(data.LogoUrl))
                    LogoSprite = await DownloadedSpritesRepository.CreateLoadSpriteTask(data.LogoUrl,
                        AsyncOperationCancellationController.CancellationToken).ConfigureAwait(false);
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