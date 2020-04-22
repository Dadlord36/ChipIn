using Newtonsoft.Json;
using ScriptableObjects.DataSets;
using TMPro;
using UnityEngine;

namespace Views.Bars.BarItems
{
    public interface ITitled
    {
        [JsonProperty("title")] string Title { get; set; }
    }

    public class ScrollBarItemWithTextView : BaseScrollBarItem, ITitled
    {
        [SerializeField] private TMP_Text textField;

        public string Title
        {
            get => textField.text;
            set => textField.text = value;
        }

        public override void Set(IScrollBarItem scrollBarItemData)
        {
            base.Set(scrollBarItemData);
            SetTitle(scrollBarItemData);
        }

        private void SetTitle(ITitled titled)
        {
            Title = titled.Title;
        }
    }
}