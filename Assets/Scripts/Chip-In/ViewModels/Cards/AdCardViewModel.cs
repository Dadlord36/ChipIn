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
using Utilities;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class AdCardViewModel : MonoBehaviour, IIdentifiedSelection, INotifyPropertyChanged, IPointerClickHandler,
        IFillingView<AdCardViewModel.FieldFillingData>
    {
        public event Action<uint> ItemSelected;
        
        public uint IndexInOrder { get; set; }

        public class FieldFillingData
        {
            public readonly Task<Sprite> AdIcon;
            public readonly string Description;

            public FieldFillingData(Task<Sprite> adIcon, string description)
            {
                AdIcon = adIcon;
                Description = description;
            }
        }

        private Sprite _adIcon;
        private string _description;

        [Binding]
        public Sprite AdIcon
        {
            get => _adIcon;
            set
            {
                if (Equals(value, _adIcon)) return;
                _adIcon = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Description
        {
            get => _description;
            private set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged();
            }
        }

        public async Task FillView(FieldFillingData dataModel, uint dataBaseIndex)
        {
            //ToDo: replace with description
            Description = dataBaseIndex.ToString();
            try
            {
                AdIcon = await dataModel.AdIcon.ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(nameof(AdCardViewModel));
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
            OnItemSelected(IndexInOrder);
        }

        private void OnItemSelected(uint obj)
        {
            ItemSelected?.Invoke(obj);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
        

    }
}