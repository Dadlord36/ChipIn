using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using Behaviours.Games;
using Common.Interfaces;
using DataModels.MatchModels;
using HttpRequests.RequestsProcessors.GetRequests;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;
using Views.ViewElements;

namespace ViewModels
{
    public interface ISlotsGame : IInitialize
    {
        event Action SpinFrameRequested;
        event Action SpinBoardRequested;
        void StartTimer(float timeInterval);
        void RefillIconsSet(ISlotIconBaseData[] roundDataSlotsIconsData);
        void AllowInteractivity();
        void OnMatchEnds();
        int RoundNumber { get; set; }
        void StopAnimatingSlots();
        void SwitchIconsInSlots(ISlotIconBaseData[] roundDataSlotsIconsData);
        void AnimateSlotsById(int[] slotsIdentifiers);

        void SetSpinTargetsAndStartSpinning(ISlotIconBaseData[] roundDataSlotsIconsData,
            in SpinBoardParameters spinBoardParameters);
    }

    [Binding]
    public sealed partial class SlotsGameViewModel : ViewsSwitchingViewModel, ISlotsGame, INotifyPropertyChanged
    {
        #region Events

        public event Action SpinFrameRequested;
        public event Action SpinBoardRequested;

        #endregion

        #region Serialized Privat Fields

        /*[SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private SelectedGameRepository selectedGameRepository;
        [SerializeField] private GameIconsRepository gameIconsRepository;*/

        [SerializeField] private Timer timer;

        /*/// <summary>
        /// Number of rows and columns on witch all spites-sheets will be slit, forming arrays of Sprites 
        /// </summary>
        [SerializeField, Tooltip("X - rows, Y - columns")]
        private Vector2Int rowsColumns = new Vector2Int(5, 5);*/

        #endregion

        #region Private Properties

        private SlotsGameBehaviour _slotsGameBehaviour;
        private SlotsGameView GameView => (SlotsGameView) View;

        #endregion


        #region Private Fields

        // private readonly BoardIconsSetHolder _boardIconsHolder;
        private int _roundNumber;
        private bool _canInteract;

        #endregion


        #region Properties Bindings

        [Binding]
        public int RoundNumber
        {
            get => _roundNumber;
            set
            {
                if (value == _roundNumber) return;
                _roundNumber = value;
                OnPropertyChanged();
            }
        }


        [Binding]
        public bool CanInteract
        {
            get => _canInteract;
            set
            {
                if (value == _canInteract) return;
                _canInteract = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Buttons Bindings

        [Binding]
        public void SpinFrame_OnClick()
        {
            OnSpinFrameRequested();
            OnInteraction();
        }

        [Binding]
        public void SpinBoard_OnClick()
        {
            OnSpinBoardRequested();
            OnInteraction();
        }

        #endregion


        protected override void OnEnable()
        {
            base.OnEnable();
            _slotsGameBehaviour = GetComponent<SlotsGameBehaviour>();
        }

        public void Initialize()
        {
            timer.Initialize();
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
                await _slotsGameBehaviour.Activate();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
            _slotsGameBehaviour.Deactivate();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _slotsGameBehaviour.Deactivate();
        }

        private void OnInteraction()
        {
            CanInteract = false;
        }


        public void StartTimer(float timeInterval)
        {
            PrintLog($"Timer interval is: {timeInterval.ToString(CultureInfo.CurrentCulture)}");
            timer.SetAndStartTimer(timeInterval);
        }

        public void RefillIconsSet(ISlotIconBaseData[] roundDataSlotsIconsData)
        {
            GameView.RefillSlotsWithUniqueIcons(roundDataSlotsIconsData);
        }

        public void StopAnimatingSlots()
        {
            GameView.StopAnimatingElements();
        }

        public void AnimateSlotsById(int[] slotsIdentifiers)
        {
            GameView.SetSlotsToAnimateIndexes(slotsIdentifiers);
        }

        public void SwitchIconsInSlots(ISlotIconBaseData[] roundDataSlotsIconsData)
        {
            GameView.SwitchSlotsToTargetIndexesInstantly(new List<IIconIdentifier>(roundDataSlotsIconsData));
            GameView.UpdateSlotsActivitiesInstantly(roundDataSlotsIconsData);
        }
        
        public void SetSpinTargetsAndStartSpinning(ISlotIconBaseData[] slotsIconsData,
            in SpinBoardParameters spinBoardParameters)
        {
            if (spinBoardParameters.SpinFrame)
            {
                GameView.SetSlotsSpinTarget(new List<IIconIdentifier>(slotsIconsData));
            }
            else
            {
                GameView.SwitchSlotsToTargetIndexesInstantly(new List<IIconIdentifier>(slotsIconsData));
            }

            GameView.UpdateSlotsActivitiesDelayed(slotsIconsData);
            GameView.StartSpinning(spinBoardParameters);
        }

        public void AllowInteractivity()
        {
            CanInteract = true;
        }

        public void OnMatchEnds()
        {
            SwitchToView(nameof(WinnerView));
        }

        private void PrintLog(string message, LogType logType = LogType.Log)
        {
            Debug.unityLogger.Log(logType, nameof(SlotsGameViewModel), message, gameObject);
        }


        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Events Invokators

        private void OnSpinFrameRequested()
        {
            SpinFrameRequested?.Invoke();
        }

        private void OnSpinBoardRequested()
        {
            SpinBoardRequested?.Invoke();
        }

        #endregion
    }
}