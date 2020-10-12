using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common.Interfaces;
using Controllers;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using DataModels;
using Factories;
using JetBrains.Annotations;
using Repositories.Local;
using Tasking;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class InterestItemViewModel : MonoBehaviour, IIdentifiedSelection<InterestBasicDataModel>, INotifyPropertyChanged, IPointerClickHandler,
        IFillingView<InterestItemViewModel.FieldFillingData>
    {
        public event Action<InterestBasicDataModel> ItemSelected;
        public uint IndexInOrder { get; set; }
        private InterestBasicDataModel Interest { get; set; }


        public class FieldFillingData
        {
            public readonly InterestBasicDataModel Interest;
            public string IconUrl => Interest.PosterUri;
            public string Description => Interest.Name;
            public int InterestIndex => (int) Interest.Id;

            public FieldFillingData(InterestBasicDataModel interest)
            {
                Interest = interest;
            }
        }

        private const string Tag = nameof(InterestItemViewModel);
        private readonly AsyncOperationCancellationController _asyncOperationCancellationController = new AsyncOperationCancellationController();

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

        private static IDownloadedSpritesRepository DownloadedSpritesRepository => SimpleAutofac.GetInstance<IDownloadedSpritesRepository>();
        
        public async Task FillView(FieldFillingData data, uint dataBaseIndex)
        {
            try
            {
                _asyncOperationCancellationController.CancelOngoingTask();

                Interest = data.Interest;
                Icon = DownloadedSpritesRepository.IconPlaceholder;
                Text = data.Description;
                Icon = await DownloadedSpritesRepository.CreateLoadSpriteTask(data.IconUrl, _asyncOperationCancellationController.CancellationToken)
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

        public void OnPointerClick(PointerEventData eventData)
        {
            Select();
        }

        public void Select()
        {
            OnItemSelected();
        }

        private void OnItemSelected()
        {
            ItemSelected?.Invoke(Interest);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}