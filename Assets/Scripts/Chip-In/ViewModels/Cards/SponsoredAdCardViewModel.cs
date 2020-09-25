using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common.Interfaces;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using JetBrains.Annotations;
using Tasking;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityWeld.Binding;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class SponsoredAdCardViewModel : MonoBehaviour, INotifyPropertyChanged, IPointerClickHandler,
        IFillingView<SponsoredAdCardViewModel.FieldFillingData>, IIdentifiedSelection
    {
        public uint IndexInOrder { get; set; }
        public event Action<uint> ItemSelected;


        public class FieldFillingData
        {
            public readonly Task<Sprite> LoadBackgroundSpriteTask;

            public FieldFillingData(Task<Sprite> loadBackgroundSpriteTask)
            {
                LoadBackgroundSpriteTask = loadBackgroundSpriteTask;
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}