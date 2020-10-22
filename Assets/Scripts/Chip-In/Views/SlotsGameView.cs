using System.Collections.Generic;
using DataModels.MatchModels;
using HttpRequests.RequestsProcessors.GetRequests;
using Repositories.Local;
using UnityEngine;
using Utilities;

namespace Views
{
    public sealed class SlotsGameView : BaseView
    {
        private const string Tag = nameof(SlotsGameView);

        [SerializeField] private SlotsView slotsView;
        [SerializeField] private GameIconsRepository gameIconsRepository;


        private bool _shouldInvokeAnimation;

        public SlotsGameView() : base(nameof(SlotsGameView))
        {
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            slotsView.RowsSpinningEnds += OnSpinningActionEnds;
            slotsView.SlotsSpinningEnds += OnSpinningActionEnds;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            slotsView.RowsSpinningEnds -= OnSpinningActionEnds;
            slotsView.SlotsSpinningEnds -= OnSpinningActionEnds;
        }

        private IReadOnlyList<IActive> _iconsActivity;


        public void UpdateSlotsActivitiesInstantly(IReadOnlyList<IActive> iconsActivity)
        {
            slotsView.SetSlotsActivity(iconsActivity);
        }

        public void UpdateSlotsActivitiesDelayed(IReadOnlyList<IActive> iconsActivity)
        {
            _iconsActivity = iconsActivity;
            LogUtility.PrintLog(Tag, "Slots Icons Activity State was updated");
        }

        public void RefillSlotsWithUniqueIcons(ISlotIconBaseData[] roundDataSlotsIconsData)
        {
            /*var gameId = selectedGameRepository.GameId;
            if (!gameIconsRepository.GameIconsSetIsInStorage(gameId))
            {
                LogUtility.PrintLogError(Tag, $"There is no icons data for Game {gameId.ToString()}");
                return;
            }

            slotsView.InitializeSlotsIcons(GetUniqueBoardIconsData(gameId, roundDataSlotsIconsData));*/
        }

        public void StartSpinning(in SpinBoardParameters spinBoardParameters)
        {
            ResetSlotsActivity();
            slotsView.StartSpinning(spinBoardParameters);
        }

        public void StopAnimatingElements()
        {
            slotsView.StopAnimatingElements();
        }

        private int[] _slotsToAnimateIndexes;

        public void SetSlotsToAnimateIndexes(int[] slotsToAnimateIndexes)
        {
            _slotsToAnimateIndexes = slotsToAnimateIndexes;
            _shouldInvokeAnimation = true;
        }

        public void SwitchSlotsToTargetIndexesInstantly(List<IIconIdentifier> boardIcons)
        {
            slotsView.SwitchSlotsToTargetIndexesInstantly(boardIcons);
        }

        public void SetSlotsSpinTarget(List<IIconIdentifier> boardIcons)
        {
            slotsView.SetSpinTargets(boardIcons);
        }

        private void InvokePredefinedSlotsAnimation()
        {
            slotsView.StartCorrespondingSlotsAnimation(_slotsToAnimateIndexes);
        }

        private void ResetSlotsActivity()
        {
            slotsView.ResetSlotsActivity();
        }

        private class SlotIconsDataComparer : IEqualityComparer<ISlotIconBaseData>
        {
            public bool Equals(ISlotIconBaseData x, ISlotIconBaseData y)
            {
                return x.IconId == y.IconId;
            }

            public int GetHashCode(ISlotIconBaseData obj)
            {
                return obj.IconId.GetHashCode();
            }
        }

        private List<BoardIconData> GetUniqueBoardIconsData(int gameId,
            IReadOnlyList<ISlotIconBaseData> roundDataSlotsIconsData)
        {
            HashSet<ISlotIconBaseData> GetUniqueIconsData()
            {
                return new HashSet<ISlotIconBaseData>(roundDataSlotsIconsData, new SlotIconsDataComparer());
            }

            var uniqueIconsData = GetUniqueIconsData();
            var icons = gameIconsRepository.GetBoardIconsData(gameId);
            var outputArray = new List<BoardIconData>();

            for (int i = 0; i < icons.Length; i++)
            {
                foreach (var slotIconBaseData in uniqueIconsData)
                {
                    if (slotIconBaseData.IconId == icons[i].Id)
                    {
                        outputArray.Add(icons[i]);
                    }
                }
            }


            return outputArray;
        }

        private void OnSpinningActionEnds()
        {
            if (_shouldInvokeAnimation)
            {
                InvokePredefinedSlotsAnimation();
                _shouldInvokeAnimation = false;
            }

            UpdateSlotsActivitiesInstantly(_iconsActivity);
        }
    }
}