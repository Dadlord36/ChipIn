using System.Threading.Tasks;
using UnityWeld.Binding;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class TextItemViewModel : SelectableListItemBase<TextListItemData>
    {
        private string _text;


        [Binding]
        public string Text
        {
            get => _text;
            set
            {
                if (_text == value) return;
                _text = value;
                OnPropertyChanged();
            }
        }

        public TextItemViewModel() : base(nameof(TextItemViewModel))
        {
        }

        public override Task FillView(TextListItemData data, uint dataBaseIndex)
        {
            Text = data.Text;
            return Task.CompletedTask;
        }
    }
}