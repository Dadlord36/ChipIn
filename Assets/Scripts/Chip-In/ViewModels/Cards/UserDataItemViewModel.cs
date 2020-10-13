using System;
using System.Threading.Tasks;
using DataModels;
using Factories;
using Repositories.Local;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class UserDataItemViewModel : SelectableListItemBase<UserProfileBaseData>
    {
        private string _userName;
        private Sprite _userAvatar;


        [Binding]
        public string UserName
        {
            get => _userName;
            set
            {
                if (value == _userName) return;
                _userName = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public Sprite UserAvatar
        {
            get => _userAvatar;
            set
            {
                _userAvatar = value;
                OnPropertyChanged();
            }
        }

        public UserDataItemViewModel() : base(nameof(UserDataItemViewModel))
        {
        }

        public override async Task FillView(UserProfileBaseData data, uint dataBaseIndex)
        {
            await base.FillView(data, dataBaseIndex).ConfigureAwait(false);
            try
            {
                UserName = data.Name;
                UserAvatar = await SimpleAutofac.GetInstance<IDownloadedSpritesRepository>().CreateLoadSpriteTask(data.AvatarUrl,
                    AsyncOperationCancellationController.CancellationToken)
                    .ConfigureAwait(false);
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
    }
}