using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels;
using Repositories.Remote;
using UnityEngine;
using UnityWeld.Binding;
using ViewModels.Basic;
using WebOperationUtilities;

namespace ViewModels
{
    [Binding]
    public class CommunityInterestLabelsViewModel : BaseMenuViewModel<CommunityBasicDataModel>
    {
        [SerializeField] private CommunitiesDataRepository communitiesDataRepository;

        protected override BaseItemsListRepository<CommunityBasicDataModel> ItemsRemoteRepository => communitiesDataRepository;


        [Binding]
        public void Button_StartAnInterest_OnClick()
        {
        }


        protected override async void UpdateItems()
        {
            base.UpdateItems();
            var itemsData = ItemsRemoteRepository.ItemsData;
            var tasks = new List<Task<Texture2D>>(itemsData.Count);

            for (int i = 0; i < itemsData.Count; i++)
            {
                var index = i;
                var task = ImagesDownloadingUtility.TryDownloadImageAsync(itemsData[index].PosterUri);
                await task.ContinueWith
                (
                    delegate(Task<Texture2D> finishedTask) 
                    { newItemsScrollView.AddElement(SpritesUtility.CreateSpriteWithDefaultParameters(finishedTask.Result)); }
                , TaskScheduler.FromCurrentSynchronizationContext());
                 
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }
    }
}