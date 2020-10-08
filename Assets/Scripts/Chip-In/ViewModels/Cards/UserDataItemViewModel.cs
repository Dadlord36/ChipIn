using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common.Interfaces;
using Controllers;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using DataModels;
using Factories;
using JetBrains.Annotations;
using Repositories.Local;
using Tasking;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class UserDataItemViewModel : MonoBehaviour, IIdentifiedSelection<UserProfileBaseData>, INotifyPropertyChanged, IPointerClickHandler,
        IFillingView<UserDataItemViewModel.FieldFillingData>
    {
        public const string Tag = nameof(UserDataItemViewModel);

        public class FieldFillingData
        {
            public readonly UserProfileBaseData Data;

            public string Name => Data.Name;
            public int? Id => Data.Id;
            public string AvatarUrl => Data.AvatarUrl;

            public FieldFillingData(UserProfileBaseData data)
            {
                Data = data;
            }
        }

        public event Action<UserProfileBaseData> ItemSelected;

        private string _userName;
        private Sprite _userAvatar;

        private UserProfileBaseData Data;

        private readonly AsyncOperationCancellationController _asyncOperationCancellationController = new AsyncOperationCancellationController();

        public uint IndexInOrder { get; }

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

        public void Select()
        {
            OnItemSelected();
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            Select();
        }

        public async Task FillView(FieldFillingData data, uint dataBaseIndex)
        {
            try
            {
                Data = data.Data;
                UserName = data.Name;
                UserAvatar = await SimpleAutofac.GetInstance<IDownloadedSpritesRepository>().CreateLoadSpriteTask(data.AvatarUrl,
                    _asyncOperationCancellationController.CancellationToken).ConfigureAwait(false);
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
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }

        private void OnItemSelected()
        {
            ItemSelected?.Invoke(Data);
        }
    }
}