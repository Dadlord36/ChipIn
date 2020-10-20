using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.Elements
{
    [Binding]
    public sealed class TimeStampViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        private DateTime _initialTime;
        private TimeSpan _timeDifference;
        private bool _isUpdating;

        private readonly TimeSpan _updateInterval = new TimeSpan(0, 1, 0);

        [Binding]
        public TimeSpan TimeDifference
        {
            get => _timeDifference;
            set
            {
                _timeDifference = value;
                OnPropertyChanged();
            }
        }

        [Binding] public string TimeDifferenceAsString => $"{TimeDifference.TotalMinutes} minutes ago";

        [Binding]
        public DateTime InitialTime
        {
            get => _initialTime;
            set
            {
                _initialTime = value;
                Run(value);
                OnPropertyChanged();
            }
        }

        public async void Run(DateTime initialTime)
        {
            Stop();
            _initialTime = initialTime;
            await StartRepeatingFunctionality();
        }

        public void Stop()
        {
            StopRepeatingFunctionality();
        }

        private async Task StartRepeatingFunctionality()
        {
            _isUpdating = true;
            while (_isUpdating)
            {
                UpdatePassedTimeData();
                await Task.Delay(_updateInterval);
            }
        }

        private void StopRepeatingFunctionality()
        {
            _isUpdating = false;
        }

        private void UpdatePassedTimeData()
        {
            TimeDifference = DateTime.Now - _initialTime;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}