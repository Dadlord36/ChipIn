using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common.Interfaces;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityWeld.Binding;

namespace Views.ViewElements.Fields
{
    public sealed class NameAndNumberSelectableFieldFillingData
    {
        public readonly uint Id;
        public readonly string Name;
        public readonly uint Number;

        public NameAndNumberSelectableFieldFillingData(string name, uint id, uint number)
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
        private uint _index;
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
            _index = dataBaseIndex;
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
            OnItemSelected(_index);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}