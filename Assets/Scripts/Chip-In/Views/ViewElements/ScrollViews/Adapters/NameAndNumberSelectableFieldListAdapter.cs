using System;
using System.Collections.Generic;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using Com.TheFallenGames.OSA.DataHelpers;
using Utilities;
using Views.ViewElements.Fields;
using Views.ViewElements.Interfaces;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    public abstract class NameAndNumberSelectableFieldListAdapter<TDataType> : BasedListAdapter<BaseParamsWithPrefab, TDataType>
        where TDataType : class

    {
        private SimpleDataHelper<TDataType> Data { get; set; }

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

        protected override async void UpdateViewsHolder(BaseItemViewsHolder viewHolder)
        {
            try
            {
                var index = (uint) viewHolder.ItemIndex;
                await ((IFillingView<TDataType>) viewHolder).FillView(Data[(int) index], index)
                    .ConfigureAwait(true);
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