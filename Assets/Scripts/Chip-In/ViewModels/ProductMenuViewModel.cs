using Repositories.Local;
using Repositories.Remote.Paginated;
using UnityEngine;
using UnityWeld.Binding;
using ViewModels.Basic;
using Views;

namespace ViewModels
{
    [Binding]
    public class ProductMenuViewModel : BaseMenuViewModel<ProductMenuView>
    {
        [SerializeField] private UserInterestsBasicDataPaginatedListRepository dataRepository;
        [SerializeField] private DownloadedSpritesRepository spritesRepository;

        public ProductMenuViewModel() : base(nameof(ProductMenuViewModel))
        {
        }

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            UpdateItems();
        }

        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
        }


        [Binding]
        public void SwitchToProductGallery()
        {
            SwitchToView(nameof(ProductGalleryView));
        }

        [Binding]
        public void Button_StartAnInterest_OnClick()
        {
        }


        protected override void UpdateItems()
        {
            base.UpdateItems();

            /*if (!dataRepository.TryGetCurrentPageItems(out var items)) return;
            for (int i = 0; i < items.Count; i++)
            {
                spritesRepository.TryToLoadSpriteAsync(new DownloadedSpritesRepository.SpriteDownloadingTaskParameters(
                    items[i].PosterUri, newItemsScrollView.AddElement));
            }*/
        }
    }
}