using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using pingak9;
using Tasking;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityWeld.Binding;

namespace ViewModels.UI.Elements
{
    [Binding]
    public sealed class TimePickerAreaViewModel : UIBehaviour, IPointerClickHandler, INotifyPropertyChanged
    {
        private MobileDateTimePicker _timeDataPicker;
        private DateTime _pickedDateTime;
        private bool _dateIsPicked;

        [Binding]
        public bool DateIsPicked
        {
            get => _dateIsPicked;
            set
            {
                _dateIsPicked = value;
                OnPropertyChanged();
            }
        }

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
                DateIsPicked = true;
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
        }

        [Binding]
        private static void ShowUpDatePickerForValidityPeriod()
        {
            var now = DateTime.Now;
            MobileNative.showDatePicker(now.Year, now.Month, now.Day);
        }

        public void Clear()
        {
            DateIsPicked = false;
            LocalDateAsString = string.Empty;
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
            TasksFactories.ExecuteOnMainThread(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            });
        }
    }
}