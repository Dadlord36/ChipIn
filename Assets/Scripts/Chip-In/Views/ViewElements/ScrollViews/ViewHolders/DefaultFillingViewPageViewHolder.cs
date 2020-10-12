using System;
using System.Threading.Tasks;
using Com.TheFallenGames.OSA.Core;
using Common.Interfaces;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using Utilities;

namespace Views.ViewElements.ScrollViews.ViewHolders
{
    public class DefaultFillingViewPageViewHolder<TDataType, TSelectionType> : BaseItemViewsHolder, IFillingView<TDataType>,
        IIdentifiedSelection<TSelectionType> where TDataType : class
    {
        private const string Tag = nameof(DefaultFillingViewPageViewHolder<TDataType,TSelectionType>);
        private IFillingView<TDataType> _fillingViewImplementation;
        private IIdentifiedSelection<TSelectionType> _identifiedSelection;

        // Retrieving the views from the item's root GameObject
        public override void CollectViews()
        {
            base.CollectViews();

            // GetComponentAtPath is a handy extension method from frame8.Logic.Misc.Other.Extensions
            // which infers the variable's component from its type, so you won't need to specify it yourself
            _fillingViewImplementation = GameObjectsUtility.GetFromRootOrChildren<IFillingView<TDataType>>(root);
            _identifiedSelection = GameObjectsUtility.GetFromRootOrChildren<IIdentifiedSelection<TSelectionType>>(root);
            if (_fillingViewImplementation == null)
                LogUtility.PrintLogError(Tag, $"{root.name} has no attached component of type {nameof(IFillingView<TDataType>)}");
        }

        public Task FillView(TDataType dataModel, uint dataBaseIndex)
        {
            return _fillingViewImplementation.FillView(dataModel, dataBaseIndex);
        }

        public uint IndexInOrder => _identifiedSelection.IndexInOrder;

        public event Action<TSelectionType> ItemSelected
        {
            add => _identifiedSelection.ItemSelected += value;
            remove => _identifiedSelection.ItemSelected -= value;
        }

        public void Select()
        {
            _identifiedSelection.Select();
        }
    }
}