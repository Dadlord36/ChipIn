using System;
using System.Threading.Tasks;
using Common.Interfaces;
using Controllers;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using DataModels;
using DataModels.Interfaces;
using Repositories.Local;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Views
{
    public sealed class UserInterestGridItemView : BaseView, IFillingView<InterestBasicDataModel>, IPointerClickHandler, IIdentifiedSelection
    {
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;

        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text textField;

        public event Action<uint> ItemSelected;

        private uint _interestId;

        private readonly AsyncOperationCancellationController _asyncOperationCancellationController = new AsyncOperationCancellationController();

        private Sprite ItemImageSprite
        {
            get => itemImage.sprite;
            set => itemImage.sprite = value;
        }

        private string ItemName
        {
            get => textField.text;
            set => textField.text = value;
        }

        public UserInterestGridItemView() : base(nameof(UserInterestGridItemView))
        {
        }

        public void SetImage(Sprite icon)
        {
            ItemImageSprite = icon;
        }

        public void SetItemImageAndText(IIndexedAndNamed gridItemData, Sprite icon)
        {
            SetItemText(gridItemData);
            SetImage(icon);
        }

        public void SetItemText(IIndexedAndNamed gridItemData)
        {
            ItemName = gridItemData.Name;
        }

        public void SetItemImageAndText(int id, string itemName, Sprite sprite)
        {
            ItemImageSprite = sprite;
            ItemName = itemName;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnItemSelected();
        }

        private void OnItemSelected()
        {
            ItemSelected?.Invoke(_interestId);
        }

        public Task FillView(InterestBasicDataModel dataModel, uint dataBaseIndex)
        {
            ItemName = dataModel.Name;
            _interestId = dataBaseIndex;

            _asyncOperationCancellationController.CancelOngoingTask();

            return downloadedSpritesRepository.CreateLoadSpriteTask(dataModel.PosterUri, _asyncOperationCancellationController.CancellationToken)
                .ContinueWith(delegate(Task<Sprite> task)
                    {
                        ItemImageSprite = task.Result;
                    },
                    _asyncOperationCancellationController.CancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}