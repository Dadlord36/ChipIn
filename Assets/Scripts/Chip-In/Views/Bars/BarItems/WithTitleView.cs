using Newtonsoft.Json;
using UnityWeld.Binding;

namespace Views.Bars.BarItems
{
    public interface ITitled
    {
        [JsonProperty("title")] string Title { get; set; }
    }

    [Binding]
    public class WithTitleView : DesignedScrollBarItemBaseViewModel, ITitled
    {
        private string _title;

        [Binding]
        public string Title
        {
            get => _title;
            set
            {
                if (value == _title) return;
                _title = value;
                OnPropertyChanged();
            }
        }


        public override void Set(IDesignedScrollBarItem designedScrollBarItemData)
        {
            base.Set(designedScrollBarItemData);
            SetTitle(designedScrollBarItemData);
        }

        private void SetTitle(ITitled titled)
        {
            Title = titled.Title;
        }
    }
}