using System.Threading.Tasks;
using DataModels;
using UnityWeld.Binding;
using ViewModels.Cards;

namespace Views.ViewElements.Fields
{ 
    [Binding]
    public sealed class NameAndNumberSelectableField : SelectableListItemBase<AnswerData>
    {
        private string _name;
        private string _number;

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

        public NameAndNumberSelectableField() : base(nameof(NameAndNumberSelectableField))
        {
        }
        
        public override Task FillView(AnswerData data, uint dataBaseIndex)
        {
            base.FillView(data, dataBaseIndex);
            Name = data.Answer;
            Number = data.Percent.ToString();
            return Task.CompletedTask;
        }
    }
}