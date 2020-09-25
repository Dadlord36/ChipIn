using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common.Interfaces;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using JetBrains.Annotations;
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
            public readonly Task<Sprite> SpriteDownloadingTask;

            public FieldFillingData(Task<Sprite> spriteDownloadingTask)
            {
                SpriteDownloadingTask = spriteDownloadingTask;
            }
        }

        public event Action<uint> ItemSelected;
        
        private Sprite _sprite;
        public uint IndexInOrder { get; set; }

        private uint _dataBaseIndex;

        [Binding]
        public Sprite Sprite
        {
            get => _sprite;
            set
            {
                _sprite = value;
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
                Sprite = await data.SpriteDownloadingTask;
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

        private void OnItemSelected()
        {
            ItemSelected?.Invoke(_dataBaseIndex);
        }
    }
}