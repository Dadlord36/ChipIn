using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class SponsorAdCardViewModel : MonoBehaviour, INotifyPropertyChanged, IFillingView<SponsorAdCardViewModel.FieldFillingData>
    {
        public class FieldFillingData
        {
            public readonly Texture2D BackgroundTexture;

            public FieldFillingData(Texture2D backgroundTexture)
            {
                BackgroundTexture = backgroundTexture;
            }
        }

        private Texture2D _backgroundTexture;

        [Binding]
        public Texture2D BackgroundTexture
        {
            get => _backgroundTexture;
            private set
            {
                if (Equals(value, _backgroundTexture)) return;
                _backgroundTexture = value;
                OnPropertyChanged();
            }
        }

        public Task FillView(FieldFillingData dataModel, uint dataBaseIndex)
        {
            BackgroundTexture = dataModel.BackgroundTexture;
            return Task.CompletedTask;
        }

        public static FieldFillingData CreateFillingData(Texture2D backgroundTexture)
        {
            return new FieldFillingData(backgroundTexture);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}