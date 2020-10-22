using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using Repositories.Local;
using ScriptableObjects.CardsControllers;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Basic;
using Views;
using Views.InteractiveWindows;

namespace ViewModels
{
    [Binding]
    public sealed class MyChallengeViewModel : BaseItemsListViewModel<MyChallengeView>
    {
        [SerializeField] private InfoCardController infoCardController;





        public MyChallengeViewModel() : base(nameof(MyChallengeViewModel))
        {
        }

        /*[Binding]
        public async Task ShowInfo_OnButtonClick()
        {
            try
            {
                await FillInfoCardWithRelatedData(SelectedGameId);
                RelatedView.ShowInfoCard();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }*/

        [Binding]
        public void Challenge_OnButtonClick()
        {
            SwitchToChallengeView();
        }

        private void SwitchToChallengeView()
        {
            SwitchToView(nameof(ChallengeView));
        }

        protected override void OnItemsListUpdated()
        {
            /*var itemsList = userGamesRemoteRepository.ItemsData;
            //Set first game id as selected in list
            if (itemsList.Count > 0)
            {
                SelectedGameId = itemsList[0].Id;
                ItemIsSelected = true;
            }*/
        }

        /*protected override void OnSelectedItemIndexChanged(int relatedItemIndex)
        {
            base.OnSelectedItemIndexChanged(relatedItemIndex);
            SelectedGameId = relatedItemIndex;
        }*/

        protected override async Task FillInfoCardWithRelatedData(int selectedId)
        {
            try
            {
                /*var responseModel = await userGamesRemoteRepository.GetOfferDataForGivenGameId(selectedId);
                var offer = responseModel.ResponseModelInterface.Offer;
                await infoCardController.ShowCard(offer, offer, offer, offer);*/
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private Task LoadGamesList()
        {
            return Task.CompletedTask;
        }

        protected override async Task LoadDataAndFillTheList()
        {
            try
            {
                await LoadGamesList();
                await FillDropdownList();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        protected override async Task FillDropdownList()
        {
            /*try
            {
                var itemsList = userGamesRemoteRepository.ItemsData;

                var tasks = new List<Task<BaseRequestProcessor<object, OfferDetailsResponseModel, IOfferDetailsResponseModel>.HttpResponse>>(itemsList.Count);

                for (int i = 0; i < itemsList.Count; i++)
                {
                    tasks.Add(userGamesRemoteRepository.GetOfferDataForGivenGameId(itemsList[i].Id));
                }

                var correspondingOffers = await Task.WhenAll(tasks);
                var itemsNamesDictionary = new Dictionary<int?, string>(itemsList.Count);

                for (int i = 0; i < itemsList.Count; i++)
                {
                    itemsNamesDictionary.Add(itemsList[i].Id, correspondingOffers[i].ResponseModelInterface.Offer.Title);
                }

                FillDropdownList(itemsNamesDictionary);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }*/
        }
    }
}