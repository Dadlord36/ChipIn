using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common.Interfaces;
using Controllers;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using JetBrains.Annotations;
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
        public uint InterestIndex { get; private set; }

        public class FieldFillingData
        {
            public readonly Task<Sprite> LoadIconTask;
            public readonly string Description;
            public readonly int InterestIndex;

            public FieldFillingData(Task<Sprite> loadLoadIconTaskTask, string description, int interestIndex)
            {
                LoadIconTask = loadLoadIconTaskTask;
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
                _asyncOperationCancellationController.CancelOngoingTask();
                InterestIndex = (uint) data.InterestIndex;
                Text = data.Description;

                var iconSprite = await data.LoadIconTask;

                TasksFactories.ExecuteOnMainThread(delegate { Icon = iconSprite; });
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}