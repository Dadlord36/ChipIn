using System.Threading.Tasks;
using UnityEngine;
using UnityWeld.Binding;
using ViewModels.Cards;

namespace Views.Bars.BarItems
{
    public interface I2DLinearGradientColors
    {
        Color StartColor { get; set; }
        Color EndColor { get; set; }
    }

    public interface IDesignedScrollBarItem : I2DLinearGradientColors, ITitled
    {
        Task<Sprite> IconSprite { get; set; }
    }

    public class DesignedScrollBarItemDefaultDataModel : IDesignedScrollBarItem
    {
        public DesignedScrollBarItemDefaultDataModel(Task<Sprite> loadIconTask, uint index)
        {
            IconSprite = loadIconTask;
            Id = index;
        }

        public Task<Sprite> IconSprite { get; set; }

        public string Title { get; set; }

        public Color StartColor { get; set; }

        public Color EndColor { get; set; }

        public uint Id { get; set; }
    }


    [Binding]
    public class DesignedScrollBarItemBaseViewModel : SelectableListItemBase<DesignedScrollBarItemDefaultDataModel>
    {
       
        private Color _backgroundGradientColor1;
        private Color _backgroundGradientColor2;

        [Binding]
        public Color BackgroundGradientColor1
        {
            get => _backgroundGradientColor1;
            set
            {
                if (value.Equals(_backgroundGradientColor1)) return;
                _backgroundGradientColor1 = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public Color BackgroundGradientColor2
        {
            get => _backgroundGradientColor2;
            set
            {
                if (value.Equals(_backgroundGradientColor2)) return;
                _backgroundGradientColor2 = value;
                OnPropertyChanged();
            }
        }

        public DesignedScrollBarItemBaseViewModel() : base(nameof(DesignedScrollBarItemBaseViewModel))
        {
        }

        private void SetBackground(I2DLinearGradientColors barItemBackground)
        {
            BackgroundGradientColor1 = barItemBackground.StartColor;
            BackgroundGradientColor2 = barItemBackground.EndColor;
        }

        public virtual void Set(IDesignedScrollBarItem designedScrollBarItemData)
        {
            SetBackground(designedScrollBarItemData);
        }

        public override async Task FillView(DesignedScrollBarItemDefaultDataModel data, uint dataBaseIndex)
        {
            await base.FillView(data, dataBaseIndex).ConfigureAwait(false);
            Set(data);
        }
    }
}