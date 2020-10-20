using System;
using System.Threading.Tasks;
using Com.TheFallenGames.OSA.Core;
using Common.Interfaces;
using UnityEngine.UI;
using Utilities;
using Views.ViewElements.Interfaces;

namespace Views.ViewElements.ScrollViews.ViewHolders
{
    public class DefaultFillingViewPageViewHolder<TDataType> : BaseItemViewsHolder, IFillingView<TDataType>, IIdentifiedSelection where TDataType : class
    {
        private const string Tag = nameof(DefaultFillingViewPageViewHolder<TDataType>);
        private IFillingView<TDataType> _fillingViewImplementation;
        private IIdentifiedSelection _identifiedSelection;
        private ContentSizeFitter _contentSizeFitter;

        public bool NeedsToBeRebuild { get; set; }

        // Retrieving the views from the item's root GameObject
        public override void CollectViews()
        {
            base.CollectViews();


            _contentSizeFitter = root.GetComponent<ContentSizeFitter>();
            if (root.TryGetComponent(out _contentSizeFitter))
            {
                _contentSizeFitter.enabled = false;
            }

            // the content size fitter should not be enabled during normal lifecycle, only in the "Twin" pass frame
            // GetComponentAtPath is a handy extension method from frame8.Logic.Misc.Other.Extensions
            // which infers the variable's component from its type, so you won't need to specify it yourself
            _fillingViewImplementation = GameObjectsUtility.GetFromRootOrChildren<IFillingView<TDataType>>(root);
            _identifiedSelection = GameObjectsUtility.GetFromRootOrChildren<IIdentifiedSelection>(root);
            if (_fillingViewImplementation == null)
                LogUtility.PrintLogError(Tag, $"{root.name} has no attached component of type {nameof(IFillingView<TDataType>)}");
        }

        public Task FillView(TDataType dataModel, uint dataBaseIndex)
        {
            if (_identifiedSelection != null)
            {
                _identifiedSelection.IndexInOrder = dataBaseIndex;
            }

            return _fillingViewImplementation.FillView(dataModel, dataBaseIndex);
        }

        public override void MarkForRebuild()
        {
            base.MarkForRebuild();
            if (_contentSizeFitter)
                _contentSizeFitter.enabled = true;
        }

        public override void UnmarkForRebuild()
        {
            if (_contentSizeFitter)
                _contentSizeFitter.enabled = false;
            base.UnmarkForRebuild();
        }

        public uint IndexInOrder
        {
            get => _identifiedSelection.IndexInOrder;
            set => _identifiedSelection.IndexInOrder = value;
        }

        public event Action<uint> ItemSelected
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