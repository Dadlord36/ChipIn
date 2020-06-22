using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.UnityEvents;
using Controllers.PaginationControllers;
using Repositories.Interfaces;
using Repositories.Remote;
using UnityEngine;
using Utilities;

namespace ViewModels.Basic
{
    public abstract class BasicPaginatedListViewModel<TEventType, TItemsDataModel, TPaginatedDataExplorer, TPaginatedDataListRepository>
        : ViewsSwitchingViewModel
        where TEventType : ReadOnlyListUnityEvent<TItemsDataModel>
        where TItemsDataModel : class
        where TPaginatedDataListRepository : RemoteRepositoryBase, IPaginatedItemsListRepository<TItemsDataModel>
        where TPaginatedDataExplorer : PaginatedDataExplorer<TPaginatedDataListRepository, TItemsDataModel>
    {
        public TEventType contentUpdated;

        [SerializeField] private TPaginatedDataExplorer paginatedDataExplorer;

        protected BasicPaginatedListViewModel(string tag) : base(tag)
        {
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            paginatedDataExplorer.Initialize();
        }

        private async void Start()
        {
            try
            {
                await TryToFillWithCurrentPageItems();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }
        }


        private async Task TryToFillWithCurrentPageItems()
        {
            try
            {
                OnContentUpdated(await paginatedDataExplorer.CreateGetCurrentPageItemsTask());
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

        public async void ListForward()
        {
            if (!paginatedDataExplorer.CanListForward) return;
            try
            {
                OnContentUpdated(await paginatedDataExplorer.CreateListForwardTask());
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (ArgumentOutOfRangeException e)
            {
                LogUtility.PrintLog(Tag, e.Message);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public async void ListBackward()
        {
            if (!paginatedDataExplorer.CanListBackward) return;
            try
            {
                OnContentUpdated(await paginatedDataExplorer.CreateListBackwardTask());
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (ArgumentOutOfRangeException e)
            {
                LogUtility.PrintLog(Tag, e.Message);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public async void ListForward(uint itemsAmount)
        {
        }

        public async void ListBackward(uint itemsAmount)
        {
        }


        private void OnContentUpdated(IReadOnlyList<TItemsDataModel> itemsList)
        {
            contentUpdated.Invoke(itemsList);
        }


        // protected abstract void FillScrollableViewWithItems(IReadOnlyList<TItemsDataModel> communityBasicDataModels);
    }
}