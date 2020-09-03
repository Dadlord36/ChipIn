﻿using Controllers;
using ScriptableObjects.CardsControllers;
using UnityEngine;
using UnityEngine.Assertions;
using UnityWeld.Binding;
using Views;

namespace ViewModels.Basic
{
    /// <summary>
    /// Base view model class
    /// </summary>
    public abstract class BaseViewModel : MonoBehaviour, INotifySwitching
    {
        protected readonly string Tag;

        [SerializeField] private BaseView view;
        protected readonly AsyncOperationCancellationController OperationCancellationController = new AsyncOperationCancellationController();
        private static AwaitingProcessVisualizerControllerScriptable MainAwaitingProcessVisualizerControllerScriptable => GameManager.MainAwaitingProcessVisualizerControllerScriptable;

        private bool _awaitingProcess;

        [Binding]
        public virtual bool IsAwaitingProcess
        {
            get => _awaitingProcess;
            set
            {
                if (value == _awaitingProcess) return;
                _awaitingProcess = value;
                if (value)
                    MainAwaitingProcessVisualizerControllerScriptable.Show();
                else
                    MainAwaitingProcessVisualizerControllerScriptable.Hide();
            }
        }

        public BaseViewModel(string tag)
        {
            Tag = tag;
        }

        public BaseView View => view;


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!Application.isPlaying)
                TryGetComponent(out view);
            Assert.IsNotNull(view);
        }
#endif
        protected virtual void OnEnable()
        {
            View.SetViewModelSwitchNotifier(this);
        }

        protected virtual void OnDisable()
        {
        }

        /// <summary>
        /// Fires up when view becoming the one, that user is currently interacts with
        /// </summary>
        protected virtual void OnBecomingActiveView()
        {
        }

        /// <summary>
        /// Fires up when view becoming inactive, so that user can't interact with it anymore
        /// </summary>
        protected virtual void OnBecomingInactiveView()
        {
        }

        void INotifySwitching.BeingSwitchedTo()
        {
            OnBecomingActiveView();
        }

        void INotifySwitching.BeingSwitchedFrom()
        {
            OnBecomingInactiveView();
        }
    }
}