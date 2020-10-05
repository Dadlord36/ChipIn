using System;
using System.Threading.Tasks;
using Common.Interfaces;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using Factories;
using Factories.ReferencesContainers;
using Repositories.Local;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityWeld.Binding;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class SponsoredAdCardViewModel : SwitchableForm, IPointerClickHandler, IFillingView<SponsoredAdCardViewModel.FieldFillingData>,
        IIdentifiedSelection
    {
        public uint IndexInOrder { get; private set; }
        public event Action<uint> ItemSelected;

        public class FieldFillingData
        {
            public readonly string BackgroundSpriteUrl;
            public readonly string LogoSpriteUrl;

            public FieldFillingData(in string backgroundSpriteUrl, in string logoSpriteUrl)
            {
                BackgroundSpriteUrl = backgroundSpriteUrl;
                LogoSpriteUrl = logoSpriteUrl;
            }

            public FieldFillingData(in string backgroundSpriteUrl)
            {
                BackgroundSpriteUrl = backgroundSpriteUrl;
            }
        }

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

        public async Task FillView(FieldFillingData dataModel, uint dataBaseIndex)
        {
            IndexInOrder = dataBaseIndex;
            var downloadedSpritesRepository = MainObjectsReferencesContainer.GetObjectInstance<IDownloadedSpritesRepository>();
            if (dataModel.LogoSpriteUrl != null)
                LogoSprite = await downloadedSpritesRepository.CreateLoadSpriteTask(dataModel.LogoSpriteUrl,
                    AsyncOperationCancellationController.CancellationToken).ConfigureAwait(false);

            BackgroundTexture = await downloadedSpritesRepository.CreateLoadSpriteTask(dataModel.BackgroundSpriteUrl,
                    AsyncOperationCancellationController.CancellationToken)
                .ConfigureAwait(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Select();
        }

        public void Select()
        {
            OnItemSelected(IndexInOrder);
        }

        private void OnItemSelected(uint index)
        {
            ItemSelected?.Invoke(index);
        }
    }
}