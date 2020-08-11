﻿using System;
using System.Threading.Tasks;
using Controllers;
using DataModels.Interfaces;
using Repositories.Local;
using ScriptableObjects.SwitchBindings;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;
using Views.Bars.BarItems;
using Views.InteractiveWindows;
using Views.InteractiveWindows.Interfaces;

namespace ScriptableObjects.CardsControllers
{
    [CreateAssetMenu(fileName = nameof(InfoCardController), menuName = nameof(CardsControllers) + "/" + nameof(InfoCardController), order = 0)]
    public class InfoCardController : BaseCardController<InfoPanelView>
    {
        private const string Tag = nameof(InfoCardController);
        
        [SerializeField] private ViewsSwitchingAnimationBinding viewsSwitchingAnimationBinding;
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;

        private readonly AsyncOperationCancellationController _asyncOperationCancellationController = new AsyncOperationCancellationController();

        private readonly ViewAppearanceParameters _cardAppearanceParameters = new ViewAppearanceParameters(
            ViewAppearanceParameters.Appearance.MoveIn, true, ViewAppearanceParameters.SwitchingViewPosition.Above,
            MoveDirection.Up
        );

        private readonly ViewAppearanceParameters _cardDisappearanceParameters = new ViewAppearanceParameters(
            ViewAppearanceParameters.Appearance.MoveOut, true, ViewAppearanceParameters.SwitchingViewPosition.Above,
            MoveDirection.Up
        );

        public async Task ShowCard(IPosterImageUri posterUri, IDescription description, ITitled titled, ICategory category)
        {
            try
            {
                _asyncOperationCancellationController.CancelOngoingTask();
                var sprite = await downloadedSpritesRepository.CreateLoadSpriteTask(posterUri.PosterUri, _asyncOperationCancellationController.CancellationToken)
                    .ConfigureAwait(true);
                await CardView.FillCardWithData(sprite, description, titled, category).ConfigureAwait(true);
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

            ShowCard();
        }

        public void ShowCard()
        {
            CardView.ShowInfoCard();
            viewsSwitchingAnimationBinding.RequestViewsSwitchingAnimation(new ViewsSwitchingParameters(_cardAppearanceParameters));
        }

        public void ShowCard(IInfoPanelData infoPanelData)
        {
            CardView.FillCardWithData(infoPanelData);
            ShowCard();
        }

        public void HideCard()
        {
            viewsSwitchingAnimationBinding.RequestViewsSwitchingAnimation(new ViewsSwitchingParameters(_cardDisappearanceParameters));
        }
    }
}