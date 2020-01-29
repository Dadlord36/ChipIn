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
        
        public virtual bool Condition
        {
            set
            {
                if (condition == value) return;
                condition = value;
                OnGroupActionPerformed();
                OnToggleSwitched();
                OnPropertyChanged();
            }
            get => condition;
        }

        public void SwitchCondition()
        {
            Condition = !Condition;
        }

        protected virtual void OnToggleSwitched()
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