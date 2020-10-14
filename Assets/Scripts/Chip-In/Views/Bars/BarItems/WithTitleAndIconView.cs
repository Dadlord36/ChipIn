using System;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;

namespace Views.Bars.BarItems
{
    [Binding]
    public sealed class WithTitleAndIconView : WithTitleView
    {
        private Sprite _iconSprite;

        [Binding]
        public Sprite IconSprite
        {
            get => _iconSprite;
            set
            {
                if (Equals(value, _iconSprite)) return;
                _iconSprite = value;
                OnPropertyChanged();
            }
        }

        public override async void Set(IDesignedScrollBarItem designedScrollBarItemData)
        {
            base.Set(designedScrollBarItemData);

            try
            {
                IconSprite = await designedScrollBarItemData.IconSprite.ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}