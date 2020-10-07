using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Controllers;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using DataModels;
using Factories;
using JetBrains.Annotations;
using Repositories.Local;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class UserAvatarItemViewModel : MonoBehaviour, IFillingView<UserAvatarItemViewModel.FieldFillingData>, INotifyPropertyChanged
    {
        public class FieldFillingData
        {
            private readonly UserProfileBaseData Data;
            
            public string AvatarUrl => Data.AvatarUrl;

            public FieldFillingData(UserProfileBaseData data)
            {
                Data = data;
            }
        }

        private Sprite _avatarSprite;
        private readonly AsyncOperationCancellationController _asyncOperationCancellationController = new AsyncOperationCancellationController();

        [Binding]
        public Sprite AvatarSprite
        {
            get => _avatarSprite;
            set
            {
                _avatarSprite = value;
                OnPropertyChanged();
            }
        }

        public async Task FillView(FieldFillingData data, uint dataBaseIndex)
        {
            AvatarSprite = await SimpleAutofac.GetInstance<IDownloadedSpritesRepository>().CreateLoadSpriteTask(data.AvatarUrl,
                    _asyncOperationCancellationController.CancellationToken)
                .ConfigureAwait(false);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}