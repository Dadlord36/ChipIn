using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Controllers;
using DataModels;
using UnityEngine;
using Utilities;

namespace Views
{
    public sealed class CommunityInterestLabelsView : BaseView
    {
        [SerializeField] private GridElementsView gridElementsView;
        private readonly AsyncOperationCancellationController _asyncOperationCancellationController = new AsyncOperationCancellationController();

        public CommunityInterestLabelsView() : base(nameof(CommunityInterestLabelsView))
        {
        }

        public async void FillInterestsGridWithItems(IReadOnlyList<InterestBasicDataModel> items)
        {
            _asyncOperationCancellationController.CancelOngoingTask();


            gridElementsView.ClearItems();
            var tasks = new List<Task>(items.Count);
            try
            {
                for (var i = 0; i < items.Count; i++)
                {
                    tasks.Add(gridElementsView.FillOneItemWithData(i, items[i],
                        _asyncOperationCancellationController.TasksCancellationTokenSource.Token));
                }

                await Task.WhenAll(tasks);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}