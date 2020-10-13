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
        public DesignedScrollBarItemDefaultDataModel(Task<Sprite> iconSprite, string title, Color startColor, Color endColor, uint id)
        {
            IconSprite = iconSprite;
            Title = title;
            StartColor = startColor;
            EndColor = endColor;
            Id = id;
        }

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
        public class FieldFillingData : IDesignedScrollBarItem
        {
            public FieldFillingData(DesignedScrollBarItemDefaultDataModel dataModel)
            {
                IconSprite = dataModel.IconSprite;
                Title = dataModel.Title;
                StartColor = dataModel.StartColor;
                EndColor = dataModel.EndColor;
                Id = dataModel.Id;
            }


            public Task<Sprite> IconSprite { get; set; }

            public string Title { get; set; }

            public Color StartColor { get; set; }

            public Color EndColor { get; set; }

            public uint Id { get; set; }
        }

        public event Action<uint> ItemSelected;


        public uint IndexInOrder { get; set; }
        private uint _index;

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
            _index = data.Id;
        }
    }
}