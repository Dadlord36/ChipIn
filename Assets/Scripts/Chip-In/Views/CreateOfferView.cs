using System;
using GlobalVariables;

namespace Views
{
    public sealed class CreateOfferView : BaseView
    {
        public event Action<string> NewCategorySelected;
        public event Action<string> NewGameTypeSelected;
        public event Action<string> NewOfferTypeSelected;


        public CreateOfferView() : base(nameof(CreateOfferView))
        {
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ResetDropdowns();
        }
        

        private void ResetDropdowns()
        {
            OnNewCategoryItemSelected(0);
            OnNewGameTypeSelected(0);
            OnNewOfferTypeSelected(0);
        }

        private void OnNewOfferTypeSelected(int index)
        {
            OnNewOfferTypeSelected(MainNames.OfferCategories.OfferCategoriesArray[index]);
        }

        private void OnNewCategoryItemSelected(int index)
        {
            OnNewCategorySelected(MainNames.OfferSegments.OffersSegmentsArray[index]);
        }

        private void OnNewGameTypeSelected(int index)
        {
            OnNewGameTypeSelected(MainNames.ChallengeTypes.ChallengeTypesArray[index]);
        }
        
        

        private void OnNewCategorySelected(string obj)
        {
            NewCategorySelected?.Invoke(obj);
        }

        private void OnNewGameTypeSelected(string obj)
        {
            NewGameTypeSelected?.Invoke(obj);
        }

        private void OnNewOfferTypeSelected(string obj)
        {
            NewOfferTypeSelected?.Invoke(obj);
        }
    }
}