using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common.Interfaces;
using Controllers;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
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
    public sealed class InterestItemViewModel : MonoBehaviour, IIdentifiedSelection, INotifyPropertyChanged, IPointerClickHandler,
        IFillingView<InterestItemViewModel.FieldFillingData>
    {
        public event Action<uint> ItemSelected;
        public uint IndexInOrder { get; set; }
        private uint InterestIndex { get; set; }


        public class FieldFillingData
        {
            public readonly string IconUrl;
            public readonly string Description;
            public readonly int InterestIndex;

            public FieldFillingData(in string iconUrl, string description, int interestIndex)
            {
                IconUrl = iconUrl;
                Description = description;
                InterestIndex = interestIndex;
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

        public async Task FillView(FieldFillingData data, uint dataBaseIndex)
        {
            try
            {
                var downloadedSpritesRepository = SimpleAutofac.GetInstance<IDownloadedSpritesRepository>();
                _asyncOperationCancellationController.CancelOngoingTask();

                Icon = downloadedSpritesRepository.IconPlaceholder;
                InterestIndex = (uint) data.InterestIndex;
                Text = data.Description;
                Icon = await downloadedSpritesRepository.CreateLoadSpriteTask(data.IconUrl, _asyncOperationCancellationController.CancellationToken)
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
            ItemSelected?.Invoke(InterestIndex);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}