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
        private event UnityAction GroupActionPerformedBackfield;
        event UnityAction IOneOfAGroup.GroupActionPerformed
        {
            add => GroupActionPerformedBackfield += value;
            remove => GroupActionPerformedBackfield -= value;
        }

        [SerializeField] private bool condition;


        [Binding]
        public virtual bool Condition
        {
            protected set
            {
                if (condition == value) return;
                condition = value;
                OnPropertyChanged();
            }
            get => condition;
        }

        public void SetCondition(bool newCondition, bool notifyConditionChanged = true)
        {
            Condition = newCondition;

            if (notifyConditionChanged)
            {
                OnGroupActionPerformed();
            }
        }

        public void SwitchCondition(bool notifyConditionChanged = true)
        {
            SetCondition(!Condition, notifyConditionChanged);
            if (notifyConditionChanged)
                OnToggleSwitched();
        }

        protected void OnToggleSwitched()
        {
            toggleSwitched?.Invoke();
        }

        void IOneOfAGroup.OnOtherOnePerformGroupAction()
        {
            SwitchCondition();
        }
        protected virtual void OnGroupActionPerformed()
        {
            GroupActionPerformedBackfield?.Invoke();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}