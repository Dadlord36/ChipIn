using Repositories.Local;
using UnityEngine;
using Views;

namespace ViewModels
{
    public class MerchantInterestViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private TitledItemsList itemsList;
        [SerializeField] private OfferCreationRepository offerCreationRepository;

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            RefillList();
        }

        private void RefillList()
        {
            itemsList.Fill(null, OnItemSelected);
        }

        private void OnItemSelected(string initialCategoryName)
        {
            offerCreationRepository["InitialCategory"] = initialCategoryName;
            SwitchToView(nameof(MerchantInterestDetailsViewModel));
        }
    }
}