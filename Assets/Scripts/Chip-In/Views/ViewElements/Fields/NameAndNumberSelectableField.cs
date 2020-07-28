using System;
using System.Threading.Tasks;
using Common.Interfaces;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public sealed class NameAndNumberSelectableField : UIBehaviour, IFillingView<NameAndNumberSelectableFieldFillingData>, IIdentifiedSelection,
        IPointerClickHandler
    {
        [SerializeField] private TMP_Text nameField;
        [SerializeField] private TMP_Text numberField;

        public uint IndexInOrder { get; set; }
        public event Action<uint> ItemSelected;

        private uint _index;

        public string Name
        {
            get => nameField.text;
            set => nameField.text = value;
        }

        public string Number
        {
            get => numberField.text;
            set => numberField.text = value;
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
    }
}