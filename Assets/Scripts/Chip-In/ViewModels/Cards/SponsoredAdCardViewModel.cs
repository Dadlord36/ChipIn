using System;
using System.Threading.Tasks;
using Common.Interfaces;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
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
            public readonly Task<Sprite> LoadBackgroundSpriteTask;
            public readonly Task<Sprite> LoadLogoSpriteTask;

            public FieldFillingData(Task<Sprite> loadBackgroundSpriteTask, Task<Sprite> createLoadSpriteTask)
            {
                LoadBackgroundSpriteTask = loadBackgroundSpriteTask;
                LoadLogoSpriteTask = createLoadSpriteTask;
            }

            public FieldFillingData(Task<Sprite> loadBackgroundSpriteTask)
            {
                LoadBackgroundSpriteTask = loadBackgroundSpriteTask;
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
            if (dataModel.LoadLogoSpriteTask != null)
                LogoSprite = await dataModel.LoadLogoSpriteTask.ConfigureAwait(false);

            BackgroundTexture = await dataModel.LoadBackgroundSpriteTask.ConfigureAwait(false);
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