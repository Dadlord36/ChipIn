using System.Threading.Tasks;
using Com.TheFallenGames.OSA.Core;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using DataModels;
using Utilities;
using ViewModels.Basic;

namespace Views.ViewElements.ScrollViews.ViewHolders
{
    public class EngageCardViewHolder : BaseItemViewsHolder, IFillingView<MarketInterestDetailsDataModel>
    {
        private const string Tag = nameof(EngageCardViewHolder);
        private IFillingView<MarketInterestDetailsDataModel> _fillingViewImplementation;

        // Retrieving the views from the item's root GameObject
        public override void CollectViews()
        {
            base.CollectViews();

            // GetComponentAtPath is a handy extension method from frame8.Logic.Misc.Other.Extensions
            // which infers the variable's component from its type, so you won't need to specify it yourself
            if (root.TryGetComponent(out BaseViewModel viewModel))
            {
                if (viewModel is IFillingView<MarketInterestDetailsDataModel> fillingView)
                {
                    _fillingViewImplementation = fillingView;
                }
                else
                {
                    
                }
            }
            else
            {
                LogUtility.PrintLogError(Tag, $"{root.name} has no attached component of type {nameof(BaseViewModel)}");
            }
        }

        public Task FillView(MarketInterestDetailsDataModel dataModel, uint dataBaseIndex)
        {
            return _fillingViewImplementation.FillView(dataModel, dataBaseIndex);
        }
    }
}