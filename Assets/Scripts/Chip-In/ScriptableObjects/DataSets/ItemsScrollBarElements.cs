using System;
using DataModels.Interfaces;
using UnityEngine;
using Views.Bars.BarItems;

namespace ScriptableObjects.DataSets
{
    public interface IScrollBarItemBackground
    {
        Color BackgroundGradientColor1 { get; set; }
        Color BackgroundGradientColor2 { get; set; }
    }
    
    public interface IScrollBarItem : IScrollBarItemBackground, ITitled, IIdentifier
    {
        Sprite IconSprite { get; set; }
    }

    [Serializable]
    public class ScrollBarItemData : IScrollBarItem
    {
        [SerializeField] private Sprite iconSprite;
        [SerializeField] private string title;
        [SerializeField] private Color backgroundGradientColor1;
        [SerializeField] private Color backgroundGradientColor2;
        [SerializeField] private int id;

        public Sprite IconSprite
        {
            get => iconSprite;
            set => iconSprite = value;
        }

        public string Title
        {
            get => title;
            set => title = value;
        }

        public Color BackgroundGradientColor1
        {
            get => backgroundGradientColor1;
            set => backgroundGradientColor1 = value;
        }

        public Color BackgroundGradientColor2
        {
            get => backgroundGradientColor2;
            set => backgroundGradientColor2 = value;
        }

        public int? Id
        {
            get => id;
            set => id = (int) value;
        }
    }

    [CreateAssetMenu(fileName = nameof(ItemsScrollBarElements), menuName = nameof(DataSets) + "/" + nameof(ItemsScrollBarElements),
        order = 0)]
    public class ItemsScrollBarElements : ScriptableObject
    {
        [SerializeField] private ScrollBarItemData[] itemsData;

        public ScrollBarItemData[] ItemsData
        {
            get => itemsData;
            set => itemsData = value;
        }
    }
}