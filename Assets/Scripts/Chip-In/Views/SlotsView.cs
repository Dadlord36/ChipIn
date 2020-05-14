using System.Collections.Generic;
using Controllers.SlotsSpinningControllers;
using DataModels.MatchModels;
using HttpRequests.RequestsProcessors.GetRequests;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace Views
{
    public class SlotsView : UIBehaviour
    {
        private const string Tag = nameof(SlotsView);

        [SerializeField] private SlotSpinnerController[] extraSlotsSpinnerControllers;
        [SerializeField] private SlotSpinnerController[] allSlotsSpinnerControllers;
        [SerializeField] private SlotSpinnerController[] rowsSpinnerControllers;
        [SerializeField] private SlotSpinnerController[] slotSpinnerControllers;
        [SerializeField] private float slotsSpritesAnimationSwitchingInterval = 0.1f;

        private int _slotItemsCount;

        protected override void OnEnable()
        {
            base.OnEnable();
            for (int i = 0; i < rowsSpinnerControllers.Length; i++)
            {
                rowsSpinnerControllers[i].InitializeSpinningElements();
            }
        }

        public void SetSlotsIcons(List<BoardIconData> boardIconsData)
        {
            FillSlotsWithBoardIconsData(allSlotsSpinnerControllers, boardIconsData,
                slotsSpritesAnimationSwitchingInterval);
        }

        private static void FillSlotsWithBoardIconsData(IReadOnlyList<SlotSpinnerController> controllers,
            List<BoardIconData> boardIconsData, float slotsSpritesAnimationSwitchingInterval)
        {
            for (int i = 0; i < controllers.Count; i++)
            {
                controllers[i].PrepareItems(boardIconsData, slotsSpritesAnimationSwitchingInterval,
                    true);
            }
        }

        public void StartSpinning(in SpinBoardParameters spinBoardParameters)
        {
            if (spinBoardParameters.SpinBoard)
            {
                StarRowsSpinning();
            }

            if (spinBoardParameters.SpinFrame)
            {
                StartSlotsSpinning();
            }
        }

        private void StartSlotsSpinning()
        {
            for (int i = 0; i < allSlotsSpinnerControllers.Length; i++)
            {
                allSlotsSpinnerControllers[i].StartElementsSpinning();
            }
        }

        private void StarRowsSpinning()
        {
            for (int i = 0; i < rowsSpinnerControllers.Length; i++)
            {
                rowsSpinnerControllers[i].StartElementsSpinning();
            }
        }

        public void StartSlotsAnimation()
        {
            for (int i = 0; i < allSlotsSpinnerControllers.Length; i++)
            {
                allSlotsSpinnerControllers[i].StartAnimating();
            }
        }

        public void SetSlotsActivity(IReadOnlyList<IActive> iconsActivity)
        {
            var length = iconsActivity.Count;

            Assert.IsTrue(length == slotSpinnerControllers.Length);

            for (int i = 0; i < length; i++)
            {
                slotSpinnerControllers[i].SetActivityState(iconsActivity[i].Active);
            }
        }

        public void SwitchSlotsToTargetIndexesInstantly(List<IIconIdentifier> targetIdentifiers)
        {
            Assert.IsTrue(targetIdentifiers.Count == slotSpinnerControllers.Length);

            for (int i = 0; i < targetIdentifiers.Count; i++)
            {
                slotSpinnerControllers[i].SlideInstantlyToIndexPosition((uint) targetIdentifiers[i].IconId);
            }

            SetRandomSlotsTargets(extraSlotsSpinnerControllers, 1, targetIdentifiers.Count);
        }

        public void SetSpinTargets(List<IIconIdentifier> targetIdentifiers)
        {
            Assert.IsTrue(targetIdentifiers.Count == slotSpinnerControllers.Length);

            for (int i = 0; i < targetIdentifiers.Count; i++)
            {
                slotSpinnerControllers[i].ItemToFocusOnIndexFromIconId = (uint) targetIdentifiers[i].IconId;
            }

            SetRandomSlotsTargets(extraSlotsSpinnerControllers, 1, targetIdentifiers.Count);
        }

        private static void SetRandomSlotsTargets(IReadOnlyList<SlotSpinnerController> spinnerControllers, int min,
            int max)
        {
            for (int i = 0; i < spinnerControllers.Count; i++)
            {
                spinnerControllers[i].ItemToFocusOnIndex = (uint) Random.Range(min, max);
            }
        }
    }
}