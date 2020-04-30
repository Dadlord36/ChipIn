using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels;
using Repositories.Local;
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
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
        protected override BaseItemsListRepository<CommunityBasicDataModel> ItemsRemoteRepository => communitiesDataRepository;


        [Binding]
        public void Button_StartAnInterest_OnClick()
        {
        }


        protected override async void UpdateItems()
        {
            base.UpdateItems();
            var itemsData = ItemsRemoteRepository.ItemsData;
            var parameters = new DownloadedSpritesRepository.SpriteDownloadingTaskParameters[itemsData.Count]; 
            
            for (int i = 0; i < itemsData.Count; i++)
            {
                parameters[i] = new DownloadedSpritesRepository.SpriteDownloadingTaskParameters(itemsData[i].PosterUri,
                    newItemsScrollView.AddElement);
            }
            await downloadedSpritesRepository.TryToLoadSpritesAsync(parameters);
        }
    }
}