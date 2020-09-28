using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common.Interfaces;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using JetBrains.Annotations;
using UnityEngine.EventSystems;
using UnityWeld.Binding;

namespace Views.ViewElements.Fields
{
    public sealed class NameAndNumberSelectableFieldFillingData
    {
        public readonly int Id;
        public readonly string Name;
        public readonly uint Number;

        public NameAndNumberSelectableFieldFillingData(string name, int id, uint number)
        {
            Name = name;
            Id = id;
            Number = number;
        }
    }

    [Binding]
    public sealed class NameAndNumberSelectableField : UIBehaviour, IFillingView<NameAndNumberSelectableFieldFillingData>,
        IIdentifiedSelection, IPointerClickHandler, INotifyPropertyChanged
    {
        private uint _id;
        private string _name;
        private string _number;
        public event Action<uint> ItemSelected;
        public uint IndexInOrder { get; set; }

        [Binding]
        public string Name
        {
            get => _name;
            private set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Number
        {
            get => _number;
            private set
            {
                if (value == _number) return;
                _number = value;
                OnPropertyChanged();
            }
        }


        public Task FillView(NameAndNumberSelectableFieldFillingData fillingData, uint dataBaseIndex)
        {
            _id = (uint) fillingData.Id;
            Name = fillingData.Name;
            Number = fillingData.Number.ToString();
            return Task.CompletedTask;
        }


        private void OnItemSelected(uint index)
        {
            ItemSelected?.Invoke(index);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Select();
        }

        public void Select()
        {
            OnItemSelected(_id);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}