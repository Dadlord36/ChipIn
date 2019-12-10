using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.Elements
{
    public interface IToggle
    {
        bool Condition { set; get; }
    }

    [Serializable]
    public abstract class BaseUIToggle : UIBehaviour, IToggle, INotifyPropertyChanged
    {
        public UnityEvent toggleSwitched;

        private float _basicValue;
        [SerializeField] private bool condition;

        public bool Condition
        {
            set
            {
                condition = value;
                _basicValue = condition ? 0 : -1.0f;
                OnPropertyChanged();
            }
            get => condition;
        }

        protected override void Awake()
        {
            base.Awake();
            SetToggleInitState();
        }

        private void SetToggleInitState()
        {
            InitializeToggle(Condition ? 1.0f : 0f);
        }

        protected abstract void InitializeToggle(float percentage);

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            Condition = condition;
            SetToggleInitState();
        }
#endif

        protected float InversePercentage(float percentage)
        {
            return Mathf.Abs(_basicValue + percentage);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnToggleSwitched()
        {
            toggleSwitched?.Invoke();
        }
    }
}