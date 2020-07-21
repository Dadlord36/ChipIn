using System.Threading.Tasks;
using Com.TheFallenGames.OSA.Core;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using Utilities;

namespace Views.ViewElements.ScrollViews.ViewHolders
{
    public class DefaultFillingViewPageViewHolder<TDataType> : BaseItemViewsHolder, IFillingView<TDataType> where TDataType : class
    {
        private const string Tag = nameof(DefaultFillingViewPageViewHolder<TDataType>);
        private IFillingView<TDataType> _fillingViewImplementation;

        // Retrieving the views from the item's root GameObject
        public override void CollectViews()
        {
            base.CollectViews();

            // GetComponentAtPath is a handy extension method from frame8.Logic.Misc.Other.Extensions
            // which infers the variable's component from its type, so you won't need to specify it yourself
            if (!root.TryGetComponent(out _fillingViewImplementation))
            {
                LogUtility.PrintLogError(Tag, $"{root.name} has no attached component of type {nameof(IFillingView<TDataType>)}");
            }
        }

        public Task FillView(TDataType dataModel, uint dataBaseIndex)
        {
            return _fillingViewImplementation.FillView(dataModel, dataBaseIndex);
        }
    }
}