﻿using System;
using System.Collections.Generic;
using DataModels.MatchModels;
using ScriptableObjects.Parameters;
using UnityEngine;
using ViewModels.UI.Elements.Icons;

namespace Controllers.SlotsSpinningControllers
{
    public abstract class LineEngineController : MonoBehaviour
    {
        #region Private serialized field

        [SerializeField] private SlotSpinnerProperties parameters;

        #endregion

        #region Events

        public event Action MovementStarted;
        public event Action MovementEnds;

        #endregion

        #region Private fields

        private Dictionary<uint, uint> _correspondingIndexesDictionary;
        private readonly ProgressiveMovement _progressiveMovement = new ProgressiveMovement();

        #endregion

        #region Protected fields

        protected LineEngine LineEngine;
        protected GameSlotIconView[] MovementElements;

        #endregion

        #region Public properties

        public uint ItemToFocusOnIndexFromIconId
        {
            get => LineEngine.ItemToFocusOnIndex;
            set => LineEngine.ItemToFocusOnIndex = _correspondingIndexesDictionary[value];
        }

        public uint ItemToFocusOnIndex
        {
            get => LineEngine.ItemToFocusOnIndex;
            set => LineEngine.ItemToFocusOnIndex = value;
        }

        #endregion

        #region Unity event functions implementation

        private void OnEnable()
        {
            SetLineEngine();
        }

        private void Start()
        {
            Stop();
        }

        #endregion


        #region Public functions

        public void AlignItems()
        {
            SetLineEngine();
            LineEngine.AlignItems();
        }

        public void SlideInstantlyToIndexPosition(uint index)
        {
            SetLineEngine();
            LineEngine.SlideInstantlyToIndexPosition(index);
        }

        public void SlideInstantlyToPositionByIconId(uint iconId)
        {
            SlideInstantlyToIndexPosition(_correspondingIndexesDictionary[iconId]);
        }

        public void StartMovement()
        {
            _progressiveMovement.ResetParameters();
            enabled = true;
            OnMovementStarted();
        }


        public void SetActivityState(bool active)
        {
            for (int i = 0; i < MovementElements.Length; i++)
            {
                MovementElements[i].ActivityState = active;
            }
        }

        #endregion

        #region Private functions

        private void Update()
        {
            _progressiveMovement.ProgressMovement();
        }

        
        private void Stop()
        {
            enabled = false;
        }

        private void OnProgressiveMovementStops()
        {
            Stop();
            OnMovementEnds();
        }
        
        #endregion

        #region Protected functions

        protected void CreateCorrespondingIndexesDictionary(IReadOnlyList<BoardIconData> boardIconData)
        {
            _correspondingIndexesDictionary = new Dictionary<uint, uint>(boardIconData.Count);
            for (int i = 0; i < boardIconData.Count; i++)
            {
                _correspondingIndexesDictionary.Add(key: (uint) boardIconData[i].Id, value: (uint) i + 1);
            }
        }

        protected void SetLineEngine()
        {
            if (!TryGetComponent(out LineEngine))
                LineEngine = transform.GetChild(0).GetComponent<LineEngine>();
            _progressiveMovement.Initialize(LineEngine,parameters,OnProgressiveMovementStops);
        }

        #endregion


        #region Events Implementation

        private void OnMovementStarted()
        {
            MovementStarted?.Invoke();
        }

        private void OnMovementEnds()
        {
            MovementEnds?.Invoke();
        }

        #endregion
    }
}