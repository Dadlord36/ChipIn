using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityWeld.Binding;

namespace UI.Elements
{
    public interface IToggle
    {
        bool Condition { set; get; }
    }

    public interface IViewElement
    {
        void RefreshVisual();
    }

    [Serializable]
    [Binding]
    public abstract class BaseUIToggle : UIBehaviour, IToggle, INotifyPropertyChanged
    {
        public UnityEvent toggleSwitched;

        private float _basicValue;
        [SerializeField] private bool condition;

        [Binding]
        public bool Condition
        {
            set
            {
                condition = value;
                _basicValue = condition ? 0 : -1.0f;
                OnConditionChanger();
                OnPropertyChanged();
            }
            get => condition;
        }

        protected float GetPathPercentageFromCondition()
        {
            return Condition ? 1.0f : 0f;
        } 

        protected void SetToggleInitState()
        {
            SetHandlePositionAlongSlide(GetPathPercentageFromCondition());
        }

        protected abstract void SetHandlePositionAlongSlide(float percentage);

/*#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            Condition = condition;
        }
#endif*/

        protected float InversePercentage(float percentage)
        {
            return Mathf.Abs(_basicValue + percentage);
        }


        protected void OnToggleSwitched()
        {
            toggleSwitched?.Invoke();
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected abstract void OnConditionChanger();
    }
}