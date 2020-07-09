using System;
using System.Collections.Generic;
using Common;
using Controllers.SlotsSpinningControllers;
using DataModels.MatchModels;
using HttpRequests.RequestsProcessors.GetRequests;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Views
{
    public class SlotsView : UIBehaviour
    {
        private const string Tag = nameof(SlotsView);

        [SerializeField] private SlotSpinningController[] extraSlotsSpinnerControllers;
        [SerializeField] private SlotSpinningController[] allSlotsSpinnerControllers;
        [SerializeField] private LineEngineRowController[] rowsSpinnerControllers;
        [SerializeField] private SlotSpinningController[] slotSpinnerControllers;
        [SerializeField] private float slotsSpritesAnimationSwitchingInterval = 0.1f;

        private int _itemsCount;

        private readonly WhenAllAction _whenAllRowsSpinningEndsAction = new WhenAllAction();
        private readonly WhenAllAction _whenAllSlotsSpinningEndsAction = new WhenAllAction();

        public event Action RowsSpinningEnds
        {
            add => _whenAllRowsSpinningEndsAction.WhenAllActionsHappened += value;
            remove => _whenAllRowsSpinningEndsAction.WhenAllActionsHappened -= value;
        }

        public event Action SlotsSpinningEnds
        {
            add => _whenAllSlotsSpinningEndsAction.WhenAllActionsHappened += value;
            remove => _whenAllSlotsSpinningEndsAction.WhenAllActionsHappened -= value;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _whenAllRowsSpinningEndsAction.ResetCounter();
            _whenAllSlotsSpinningEndsAction.ResetCounter();

            SubscribeOnEvents();
            for (int i = 0; i < rowsSpinnerControllers.Length; i++)
            {
                rowsSpinnerControllers[i].Prepare(3);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeFromEvents();
        }

        private void SubscribeOnEvents()
        {
            for (int i = 0; i < rowsSpinnerControllers.Length; i++)
            {
                rowsSpinnerControllers[i].MovementEnds += _whenAllRowsSpinningEndsAction.IterateActionCounter;
            }

            for (int i = 0; i < slotSpinnerControllers.Length; i++)
            {
                slotSpinnerControllers[i].MovementEnds += _whenAllSlotsSpinningEndsAction.IterateActionCounter;
            }

            RowsSpinningEnds += OnRowsSpinningEnds;
            SlotsSpinningEnds += OnSlotsSpinningEnds;
        }

        private void UnsubscribeFromEvents()
        {
            for (int i = 0; i < rowsSpinnerControllers.Length; i++)
            {
                rowsSpinnerControllers[i].MovementEnds -= _whenAllRowsSpinningEndsAction.IterateActionCounter;
            }

            for (int i = 0; i < slotSpinnerControllers.Length; i++)
            {
                slotSpinnerControllers[i].MovementEnds -= _whenAllSlotsSpinningEndsAction.IterateActionCounter;
            }
        }


        public void InitializeSlotsIcons(List<BoardIconData> boardIconsData)
        {
            _itemsCount = boardIconsData.Count;
            FillSlotsWithBoardIconsData(allSlotsSpinnerControllers, boardIconsData,
                slotsSpritesAnimationSwitchingInterval);
        }

        private static void FillSlotsWithBoardIconsData(IReadOnlyList<SlotSpinningController> controllers,
            List<BoardIconData> boardIconsData, float slotsSpritesAnimationSwitchingInterval)
        {
            for (int i = 0; i < controllers.Count; i++)
            {
                controllers[i].PrepareItems(boardIconsData, slotsSpritesAnimationSwitchingInterval, true);
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
                allSlotsSpinnerControllers[i].StartMovement();
            }
        }

        private void StarRowsSpinning()
        {
            for (int i = 0; i < rowsSpinnerControllers.Length; i++)
            {
                rowsSpinnerControllers[i].StartMovement();
            }
        }

        public void StopAnimatingElements()
        {
            for (int i = 0; i < allSlotsSpinnerControllers.Length; i++)
            {
                allSlotsSpinnerControllers[i].StopAnimating();
            }
        }

        public void StartCorrespondingSlotsAnimation(int[] slotsToAnimateIndexes)
        {
            StartSlotsAnimation(slotSpinnerControllers, slotsToAnimateIndexes);
        }

        private static void StartSlotsAnimation(SlotSpinningController[] animatedSlotsControllers,
            int[] slotsToAnimateIndexes)
        {
            for (int i = 0; i < slotsToAnimateIndexes.Length; i++)
            {
                animatedSlotsControllers[slotsToAnimateIndexes[i]].StartAnimating();
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

            for (int i = 0; i < extraSlotsSpinnerControllers.Length; i++)
            {
                extraSlotsSpinnerControllers[i].SlideInstantlyToIndexPosition(GenerateRandomTargetIndex());
            }
        }

        public void SetSpinTargets(List<IIconIdentifier> targetIdentifiers)
        {
            Assert.IsTrue(targetIdentifiers.Count == slotSpinnerControllers.Length);

            for (int i = 0; i < targetIdentifiers.Count; i++)
            {
                slotSpinnerControllers[i].ItemToFocusOnIndexFromIconId = (uint) targetIdentifiers[i].IconId;
            }

            SetRandomSlotsTargets(extraSlotsSpinnerControllers);
        }

        private void SetRandomSlotsTargets(IReadOnlyList<LineEngineController> spinnerControllers)
        {
            for (int i = 0; i < spinnerControllers.Count; i++)
            {
                spinnerControllers[i].ItemToFocusOnIndex = GenerateRandomTargetIndex();
            }
        }

        private uint GenerateRandomTargetIndex()
        {
            return (uint) Random.Range(1, _itemsCount);
        }

        public void ResetSlotsActivity()
        {
            for (int i = 0; i < slotSpinnerControllers.Length; i++)
            {
                slotSpinnerControllers[i].SetActivityState(true);
            }
        }

        private void OnRowsSpinningEnds()
        {
        }

        private void OnSlotsSpinningEnds()
        {
        }
    }
}