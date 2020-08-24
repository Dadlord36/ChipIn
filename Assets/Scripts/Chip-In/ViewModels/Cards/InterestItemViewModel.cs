using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using JetBrains.Annotations;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class InterestItemViewModel : MonoBehaviour, INotifyPropertyChanged, IFillingView<InterestItemViewModel.FieldFillingData>
    {
        public class FieldFillingData
        {
            public readonly Task<Sprite> LoadIconTask;
            public readonly string Description;

            public FieldFillingData(Task<Sprite> loadLoadIconTaskTask, string description)
            {
                LoadIconTask = loadLoadIconTaskTask;
                Description = description;
            }
        }

        private const string Tag = nameof(InterestItemViewModel);

        private string _text;
        private Sprite _icon;

        [Binding]
        public string Text
        {
            get => _text;
            set
            {
                if (value == _text) return;
                _text = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public Sprite Icon
        {
            get => _icon;
            set
            {
                if (Equals(value, _icon)) return;
                _icon = value;
                OnPropertyChanged();
            }
        }

        public async Task FillView(FieldFillingData data, uint dataBaseIndex)
        {
            try
            {
                var iconSprite = await data.LoadIconTask;

                TasksFactories.ExecuteOnMainThread(delegate
                {
                    Icon = iconSprite;
                    Text = data.Description;
                });
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}