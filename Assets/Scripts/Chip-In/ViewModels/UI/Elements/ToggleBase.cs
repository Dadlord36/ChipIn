using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityWeld.Binding;
using ViewModels.UI.Interfaces;

namespace ViewModels.UI.Elements
{
    [Serializable]
    [Binding]
    public abstract class ToggleBase : UIBehaviour, INotifyPropertyChanged, IToggle, IOneOfAGroup
    {
        public UnityEvent toggleSwitched;
        public event Action ConditionChanged;
        public event UnityAction GroupActionPerformed;
        

        [SerializeField] private bool condition;

        [Binding]
        public virtual bool Condition
        {
            protected set
            {
                condition = value;
                OnPropertyChanged();
            }
            get => condition;
        }

        public void SetCondition(bool newCondition, bool notifyConditionChanged = true)
        {
            Condition = newCondition;
            if (notifyConditionChanged)
                OnConditionChanged();
        }

        public void SwitchCondition(bool notifyConditionChanged = true)
        {
            SetCondition(!Condition, notifyConditionChanged);
        }

        protected void OnToggleSwitched()
        {
            toggleSwitched?.Invoke();
        }

        private void OnConditionChanged()
        {
            ConditionChanged?.Invoke();
            OnGroupActionPerformed();
        }

        void IOneOfAGroup.OnOtherOnePerformGroupAction()
        {
            SwitchCondition();
        }
        
        private void OnGroupActionPerformed()
        {
            GroupActionPerformed?.Invoke();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}