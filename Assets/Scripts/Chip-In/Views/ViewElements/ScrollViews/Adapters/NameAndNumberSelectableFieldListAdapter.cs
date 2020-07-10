using System;
using System.Collections.Generic;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using Com.TheFallenGames.OSA.DataHelpers;
using Utilities;
using Views.ViewElements.Fields;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    public abstract class NameAndNumberSelectableFieldListAdapter<TDataType, TFillingViewAdapter> : OSA<BaseParamsWithPrefab,
        DefaultFillingViewPageViewHolder<NameAndNumberSelectableFieldFillingData>>
        where TFillingViewAdapter : FillingViewAdapter<TDataType, NameAndNumberSelectableFieldFillingData>, new()
    {
        private readonly string Tag;

        private SimpleDataHelper<TDataType> Data { get; set; }

        private readonly TFillingViewAdapter _fillingViewAdapter = new TFillingViewAdapter();


        public NameAndNumberSelectableFieldListAdapter()
        {
            Tag = GetType().Name;
        }

        public void RefillWithData(IList<TDataType> data)
        {
            ClearData();
            if (!IsInitialized)
            {
                Init();
            }

            Data.InsertItemsAtStart(data);
        }

        private void ClearData()
        {
            if (Data != null)
            {
                if (Data.Count > 0)
                    Data.RemoveItems(0, Data.Count);
            }
            else
            {
                Data = new SimpleDataHelper<TDataType>(this);
            }
        }

        protected override DefaultFillingViewPageViewHolder<NameAndNumberSelectableFieldFillingData> CreateViewsHolder(int itemIndex)
        {
            var instance = new DefaultFillingViewPageViewHolder<NameAndNumberSelectableFieldFillingData>();

            // Using this shortcut spares you from:
            // - instantiating the prefab yourself
            // - enabling the instance game object
            // - setting its index 
            // - calling its CollectViews()
            instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);

            return instance;
        }

        protected override async void UpdateViewsHolder(DefaultFillingViewPageViewHolder<NameAndNumberSelectableFieldFillingData> newOrRecycled)
        {
            try
            {
                var index = (uint) newOrRecycled.ItemIndex;
                await newOrRecycled.FillView(_fillingViewAdapter.Convert(Data[(int) index], index), index);
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}