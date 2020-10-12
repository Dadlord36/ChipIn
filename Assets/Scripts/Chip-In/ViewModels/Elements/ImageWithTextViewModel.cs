using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Behaviours;
using Common.Interfaces;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using DataModels;
using Factories;
using JetBrains.Annotations;
using Repositories.Local;
using Tasking;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityWeld.Binding;

namespace ViewModels.Elements
{
    [Binding]
    public sealed class ImageWithTextViewModel : AsyncOperationsMonoBehaviour, IFillingView<ImageWithTextViewModel.FieldFillingData>,
        IIdentifiedSelection<InterestBasicDataModel>, IPointerClickHandler, INotifyPropertyChanged
    {
        public class FieldFillingData
        {
            public readonly InterestBasicDataModel Data;

            public FieldFillingData(InterestBasicDataModel data)
            {
                Data = data;
            }
        }

        public event Action<InterestBasicDataModel> ItemSelected;
        public uint IndexInOrder { get; }

        private Sprite _iconSprite;
        private InterestBasicDataModel _interestData;

        [Binding]
        public Sprite IconSprite
        {
            get => _iconSprite;
            set
            {
                _iconSprite = value;
                OnPropertyChanged();
            }
        }

        private string _text;

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

        public void OnPointerClick(PointerEventData eventData)
        {
            Select();
        }

        public async Task FillView(FieldFillingData data, uint dataBaseIndex)
        {
            AsyncOperationCancellationController.CancelOngoingTask();

            _interestData = data.Data;
            Text = data.Data.Name;
            IconSprite = await SimpleAutofac.GetInstance<IDownloadedSpritesRepository>().CreateLoadSpriteTask(data.Data.PosterUri,
                    AsyncOperationCancellationController.CancellationToken)
                .ConfigureAwait(false);
        }

        public void Select()
        {
            OnItemSelected(_interestData);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }

        private void OnItemSelected(InterestBasicDataModel obj)
        {
            ItemSelected?.Invoke(obj);
        }
    }
}