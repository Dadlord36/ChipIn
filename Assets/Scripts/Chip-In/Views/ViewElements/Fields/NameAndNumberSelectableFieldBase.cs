using UnityWeld.Binding;
using ViewModels.Cards;

namespace Views.ViewElements.Fields
{
    [Binding]
    public abstract class NameAndNumberSelectableFieldBase<TDataType> : SelectableListItemBase<TDataType> where TDataType : class
    {
        private string _name;
        private string _number;

        [Binding]
        public string Name
        {
            get => _name;
            protected set
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
            protected set
            {
                if (value == _number) return;
                _number = value;
                OnPropertyChanged();
            }
        }

        protected NameAndNumberSelectableFieldBase(string childClassName) : base(childClassName)
        {
        }
    }
}