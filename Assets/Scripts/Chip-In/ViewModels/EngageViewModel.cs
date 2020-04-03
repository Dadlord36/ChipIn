using Repositories.Local;
using UnityEngine;
using Views;
using Views.Bars;

namespace ViewModels
{
    public class EngageViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private OfferCreationRepository offerCreationRepository;
        [SerializeField] private ItemsScrollBarView itemsScrollBarView;


        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            itemsScrollBarView.Activate();
            itemsScrollBarView.NewItemSelected += OnNewOfferSegmentSelected;
        }

        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
            itemsScrollBarView.Deactivate();
            itemsScrollBarView.NewItemSelected -= OnNewOfferSegmentSelected;
        }

        private void OnNewOfferSegmentSelected(string segmentName)
        {
            offerCreationRepository.OfferSegmentName = segmentName;
            SwitchToView(nameof(CreateOfferView));
        }
    }
}