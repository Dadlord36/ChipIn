using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class AdCardViewModel : MonoBehaviour, INotifyPropertyChanged, IFillingView<AdCardViewModel.FieldFillingData>
    {
        public class FieldFillingData
        {
            public readonly Task<Texture2D> AdIcon;
            public readonly string Description;

            public FieldFillingData(Task<Texture2D> adIcon, string description)
            {
                AdIcon = adIcon;
                Description = description;
            }
        }
        
        private Texture2D _adIcon;
        private string _description;

        [Binding]
        public Texture2D AdIcon
        {
            get => _adIcon;
            set
            {
                if (Equals(value, _adIcon)) return;
                _adIcon = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Description
        {
            get => _description;
            private set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged();
            }
        }

        public Task FillView(FieldFillingData dataModel, uint dataBaseIndex)
        {
            Description = dataModel.Description;
            return dataModel.AdIcon.ContinueWith(delegate (Task<Texture2D> taskResult)
            {
                AdIcon = taskResult.GetAwaiter().GetResult();
            } ,CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, GameManager.MainThreadScheduler);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}