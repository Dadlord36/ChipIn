using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using I2.Loc;
using JetBrains.Annotations;
using pingak9;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityWeld.Binding;

namespace ViewModels.UI.Elements
{
    [Binding]
    public sealed class TimePickerAreaViewModel : UIBehaviour, IPointerClickHandler, INotifyPropertyChanged
    {
        [SerializeField] private string localizationTerm = "Type this date";

        private MobileDateTimePicker _timeDataPicker;
        private DateTime _pickedDateTime;

        public UnityEvent datePicked;
        private string _localDateAsString;


        [Binding]
        public DateTime PickedDateTime
        {
            get => _pickedDateTime;
            set
            {
                if (value.Equals(_pickedDateTime)) return;
                _pickedDateTime = value;
                LocalDateAsString = value.ToShortDateString();
                OnPropertyChanged();
            }
        }

        [Binding]
        public string LocalDateAsString
        {
            get => _localDateAsString;
            private set
            {
                if (value == _localDateAsString) return;
                _localDateAsString = value;
                OnPropertyChanged();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ShowUpDatePickerForValidityPeriod();
        }

        protected override void Start()
        {
            base.Start();
            _timeDataPicker = MobileDateTimePicker.CreateTime();
            _timeDataPicker.OnDateChanged = OnDateChanged;
            _timeDataPicker.OnPickerClosed = OnDateChanged;
            
            LocalDateAsString = LocalizationManager.GetTranslation(localizationTerm);
        }

        [Binding]
        private static void ShowUpDatePickerForValidityPeriod()
        {
            var now = DateTime.Now;
            MobileNative.showDatePicker(now.Year, now.Month, now.Day);
        }

        private void OnDateChanged(DateTime dateTime)
        {
            PickedDateTime = dateTime;
            OnDatePicked();
        }

        private void OnDatePicked()
        {
            datePicked.Invoke();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}