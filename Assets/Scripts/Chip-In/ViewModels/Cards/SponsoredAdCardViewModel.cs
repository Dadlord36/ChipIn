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
            public readonly Task<Texture2D> LoadBackgroundTextureTask;

            public FieldFillingData(Task<Texture2D> loadBackgroundTextureTask)
            {
                LoadBackgroundTextureTask = loadBackgroundTextureTask;
            }
        }

        private Texture2D _backgroundTexture;

        [Binding]
        public Texture2D BackgroundTexture
        {
            get => _backgroundTexture;
            private set
            {
                if (Equals(value, _backgroundTexture)) return;
                _backgroundTexture = value;
                OnPropertyChanged();
            }
        }

        public async Task FillView(FieldFillingData dataModel, uint dataBaseIndex)
        {
            BackgroundTexture = await dataModel.LoadBackgroundTextureTask.ConfigureAwait(false);
        }
        
        public void OnPointerClick(PointerEventData eventData)
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
            TasksFactories.ExecuteOnMainThread(()=>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            });
        }
    }
}