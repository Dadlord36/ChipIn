using Repositories.Local;
using Repositories.Remote.Paginated;
using UnityEngine;
using UnityWeld.Binding;
using ViewModels.Basic;
using Views;

namespace ViewModels
{
    [Binding]
    public class CommunityInterestLabelsViewModel : BaseMenuViewModel<CommunityInterestLabelsView>
    {
        private const string Tag = nameof(CommunityInterestLabelsViewModel);
        
        [SerializeField] private CommunitiesDataPaginatedListRepository communitiesDataRepository;
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;


        protected override void OnEnable()
        {
            base.OnEnable();
            communitiesDataRepository.DataWasLoaded += UpdateItems;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            communitiesDataRepository.DataWasLoaded -= UpdateItems;
        }

        [Binding]
        public void Button_StartAnInterest_OnClick()
        {
        }

        [Binding]
        public void NextPage_OnClick()
        {
            /*if (communitiesDataRepository.TryGetPageItems(out var items))
            {
                RelatedView.UpdateGridItemsContent(items); 
            }
            else
            {
                LogUtility.PrintLog(Tag,"There are no items to fill the grid");
            }*/
        }

        protected override void UpdateItems()
        {
            base.UpdateItems();
            UpdateNewItemsList();
            
            /*if (communitiesDataRepository.TryGetCurrentPageItems(out var items))
            {
                RelatedView.UpdateGridItemsContent(items); 
            }
            else
            {
                LogUtility.PrintLog(Tag,"There are no items to fill the grid");
            }*/
        }

        private async void UpdateNewItemsList()
        {
            /*try
            {
                var itemsData = communitiesDataRepository.ItemsData;
                if (itemsData == null) return;

                var parameters = new DownloadedSpritesRepository.SpriteDownloadingTaskParameters[itemsData.Count];

                for (int i = 0; i < itemsData.Count; i++)
                {
                    parameters[i] = new DownloadedSpritesRepository.SpriteDownloadingTaskParameters(itemsData[i].PosterUri,
                        newItemsScrollView.AddElement);
                }

                await downloadedSpritesRepository.TryToLoadSpritesAsync(parameters);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }*/
        }
    }
}