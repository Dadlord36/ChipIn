using System.Collections.Generic;
using DataModels.MatchModels;
using HttpRequests.RequestsProcessors.GetRequests;
using Repositories.Local;
using UnityEngine;

namespace Views
{
    public class SlotsGameView : BaseView
    {
        [SerializeField] private SlotsView slotsView;
        [SerializeField] private GameIconsRepository gameIconsRepository;
        [SerializeField] private SelectedGameRepository selectedGameRepository;

        public void SetSlotsActivity(IReadOnlyList<IActive> iconsActivity)
        {
            slotsView.SetSlotsActivity(iconsActivity);
        }

        public void PrepareSlots()
        {
           var iconsData = gameIconsRepository.GetBoardIconsData(selectedGameRepository.GameId);
           slotsView.SetSlotsIcons(new List<BoardIconData>(iconsData));
        }

        public void StartSpinning(in SpinBoardParameters spinBoardParameters)
        {
            slotsView.StartSpinning(spinBoardParameters);
        }
        
        
        public void StartSlotsAnimation()
        {
            slotsView.StartSlotsAnimation();
        }

        public void SwitchSlotsToTargetIndexesInstantly(List<IIconIdentifier> boardIcons)
        {
            slotsView.SwitchSlotsToTargetIndexesInstantly(boardIcons);
        }
        
        public void SetSlotsSpinTarget(List<IIconIdentifier> boardIcons)
        {
            slotsView.SetSpinTargets(boardIcons);
        }
    }
}