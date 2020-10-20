using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Controllers;
using DataModels;
using Factories;
using JetBrains.Annotations;
using Repositories.Local;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views.ViewElements.Interfaces;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class UserAvatarItemViewModel : MonoBehaviour, IFillingView<UserProfileBaseData>, INotifyPropertyChanged
    {
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

        public async Task FillView(UserProfileBaseData data, uint dataBaseIndex)
        {
            try
            {
                AvatarSprite = await SimpleAutofac.GetInstance<IDownloadedSpritesRepository>().CreateLoadSpriteTask(data.AvatarUrl,
                        _asyncOperationCancellationController.CancellationToken)
                    .ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(nameof(UserAvatarItemViewModel));
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
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}