using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using DataModels.Interfaces;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class VerificationItemViewModel : MonoBehaviour, IFillingView<VerificationItemViewModel.FieldFillingData>, INotifyPropertyChanged
    {
        private string _idAndTitle;
        private string _category;
        private string _dateDay;
        private string _dateTime;

        public class FieldFillingData
        {
            public int? Id { get; }
            public string Category { get; }
            public string Title { get; }
            public DateTime Date { get; }

            public FieldFillingData(IVerificationModel data)
            {
                Id = data.Id;
                Category = data.Category;
                Title = data.Title;
                Date = data.Date;
            }
        }

        [Binding]
        public string IdAndTitle
        {
            get => _idAndTitle;
            set
            {
                if (value == _idAndTitle) return;
                _idAndTitle = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Category
        {
            get => _category;
            set
            {
                if (value == _category) return;
                _category = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string DateDay
        {
            get => _dateDay;
            set
            {
                if (value == _dateDay) return;
                _dateDay = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string DateTime
        {
            get => _dateTime;
            set
            {
                if (value == _dateTime) return;
                _dateTime = value;
                OnPropertyChanged();
            }
        }

        public Task FillView(FieldFillingData data, uint dataBaseIndex)
        {
            IdAndTitle = $"{data.Id.ToString()} {data.Title}";
            Category = data.Category;
            DateDay = data.Date.ToShortDateString();
            DateTime = data.Date.ToShortTimeString();

            return Task.CompletedTask;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}