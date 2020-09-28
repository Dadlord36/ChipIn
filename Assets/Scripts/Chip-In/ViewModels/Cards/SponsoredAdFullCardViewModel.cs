using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common.Interfaces;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using JetBrains.Annotations;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class SponsoredAdFullCardViewModel : MonoBehaviour, IFillingView<SponsoredAdFullCardViewModel.FieldFillingData>, IIdentifiedSelection,
        INotifyPropertyChanged
    {
        private const string Tag = nameof(SponsoredAdFullCardViewModel);

        public class FieldFillingData
        {
            public readonly Task<Sprite> DownloadBackgroundTask;
            public readonly Task<Sprite> DownloadLogoTask;

            public FieldFillingData(Task<Sprite> downloadBackgroundTask, Task<Sprite> createLoadSpriteTask)
            {
                DownloadBackgroundTask = downloadBackgroundTask;
                DownloadLogoTask = createLoadSpriteTask;
            }
        }

        public event Action<uint> ItemSelected;

        private uint _dataBaseIndex;
        private Sprite _background;
        private Sprite _logo;

        public uint IndexInOrder { get; private set; }

        [Binding]
        public Sprite Background
        {
            get => _background;
            set
            {
                _background = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public Sprite Logo
        {
            get => _logo;
            set
            {
                _logo = value;
                OnPropertyChanged();
            }
        }

        public void Select()
        {
            OnItemSelected();
        }

        public async Task FillView(FieldFillingData data, uint dataBaseIndex)
        {
            try
            {
                _dataBaseIndex = dataBaseIndex;
                Background = await data.DownloadBackgroundTask.ConfigureAwait(false);
                if (data.DownloadLogoTask != null)
                    Logo = await data.DownloadLogoTask.ConfigureAwait(false);
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

        private void OnItemSelected()
        {
            ItemSelected?.Invoke(_dataBaseIndex);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}