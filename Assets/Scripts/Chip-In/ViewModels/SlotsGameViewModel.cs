using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataModels.Interfaces;
using DataModels.MatchModels;
using JetBrains.Annotations;
using Repositories.Local;
using Repositories.Remote;
using UnityEngine;
using UnityWeld.Binding;
using Views;
using Views.ViewElements;

namespace ViewModels
{
    public interface ISlotsGame
    {
        event Action SpinFrameRequested;
        event Action SpinBoardRequested;
        void StartTimer(float timeInterval);
        Task RefillIconsSet(IReadOnlyList<IndexedUrl> indexedUrls);
        void SetSlotsIcons(ISlotIconBaseData[] slotsIconsData);
        void AllowInteractivity();
        void OnGameFinished();
        int RoundNumber { get; set; }
    }

    public sealed partial class SlotsGameViewModel : ViewsSwitchingViewModel, ISlotsGame,
        INotifyPropertyChanged
    {
        #region Events

        public event Action SpinFrameRequested;
        public event Action SpinBoardRequested;

        #endregion

        #region Serialized Privat Fields

        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private SelectedGameRepository selectedGameRepository;
        [SerializeField] private Timer timer;

        /// <summary>
        /// Number of rows and columns on witch all spites-sheets will be slit, forming arrays of Sprites 
        /// </summary>
        [SerializeField, Tooltip("X - rows, Y - columns")]
        private Vector2Int rowsColumns = new Vector2Int(5, 5);

        #endregion

        #region Private Properties

        private SlotsGameView GameView => (SlotsGameView) View;

        #endregion


        #region Private Fields

        private readonly BoardIconsSetHolder _boardIconsHolder;
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


        #region Constructor

        public SlotsGameViewModel()
        {
            _boardIconsHolder = new BoardIconsSetHolder(rowsColumns.x, rowsColumns.y);
        }

        #endregion

        private void OnInteraction()
        {
            CanInteract = false;
        }

        private void UpdateSlotsIconsFramesActivity(IReadOnlyList<IActive> iconsActivity)
        {
            GameView.SetSlotsActivity(iconsActivity);
            PrintLog("Slots Icons Activity State was updated");
        }

        public void StartTimer(float timeInterval)
        {
            PrintLog($"Timer interval is: {timeInterval.ToString()}");
            timer.SetAndStartTimer(timeInterval);
        }

        public async Task RefillIconsSet(IReadOnlyList<IndexedUrl> indexedUrls)
        {
            await _boardIconsHolder.Refill(indexedUrls);
        }

        public void SetSlotsIcons(ISlotIconBaseData[] slotsIconsData)
        {
            var boardIcons = _boardIconsHolder.GetBoardIconsDataWithIDs(slotsIconsData);
            UpdateSlotsIconsFramesActivity(slotsIconsData);
            GameView.SetSlotsIcons(boardIcons);
            GameView.StartSlotsAnimation();
        }

        public void AllowInteractivity()
        {
            CanInteract = true;
        }

        public void OnGameFinished()
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