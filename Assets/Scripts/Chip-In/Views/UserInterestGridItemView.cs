using System;
using System.Threading.Tasks;
using DataModels;
using DataModels.Interfaces;
using Repositories.Local;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using ViewModels.Cards;

namespace Views
{
    public sealed class UserInterestGridItemView : SelectableListItemBase<InterestBasicDataModel>
    {
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;

        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text textField;

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

        public override async Task FillView(InterestBasicDataModel dataModel, uint dataBaseIndex)
        {
            await base.FillView(dataModel, dataBaseIndex);
            try
            {
                ItemName = dataModel.Name;
                AsyncOperationCancellationController.CancelOngoingTask();

                ItemImageSprite = await downloadedSpritesRepository
                    .CreateLoadSpriteTask(dataModel.PosterUri, AsyncOperationCancellationController.CancellationToken)
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