using System;
using System.Threading;
using System.Threading.Tasks;
using DataModels.Interfaces;
using HttpRequests;
using ScriptableObjects.CardsControllers;
using ScriptableObjects.SwitchBindings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Views.Bars.BarItems;
using Views.InteractiveWindows.Interfaces;
using WebOperationUtilities;

namespace Views.InteractiveWindows
{
    public interface IInfoPanelView
    {
        void FillCardWithData(IInfoPanelData data);
        void ShowInfoCard();
        void HideInfoCard();
    }

    public sealed class InfoPanelView : BaseView, IInfoPanelData, IInfoPanelView
    {
        private static CancellationTokenSource _cancellationTokenSource;

        public class InfoPanelData : IInfoPanelData
        {
            public InfoPanelData(Sprite itemLabel, IDescription description, ITitled titled, ICategory category)
            {
                ItemLabel = itemLabel;
                ItemName = titled?.Title;
                ItemType = category?.Category;
                ItemDescription = description?.Description;
            }

            public Sprite ItemLabel { get; set; }
            public string ItemName { get; set; }
            public string ItemType { get; set; }
            public string ItemDescription { get; set; }
        }


        [SerializeField] private Image productIconImage;
        [SerializeField] private TMP_Text itemNameField;
        [SerializeField] private TMP_Text itemTypeField;
        [SerializeField] private TMP_Text itemDescriptionField;

        [SerializeField] private InfoCardController infoCardController;

        public Sprite ItemLabel
        {
            get => productIconImage.sprite;
            set => productIconImage.sprite = value;
        }

        public string ItemName
        {
            get => itemNameField.text;
            set => itemNameField.text = value;
        }

        public string ItemType
        {
            get => itemTypeField.text;
            set => itemTypeField.text = value;
        }

        public string ItemDescription
        {
            get => itemDescriptionField.text;
            set => itemDescriptionField.text = value;
        }

        public InfoPanelView() : base(nameof(InfoPanelView))
        {
        }

        protected override void Awake()
        {
            base.Awake();
            infoCardController.SetCardViewToControl(this);
        }

        public void FillCardWithData(IInfoPanelData data)
        {
            if (data.ItemLabel != null)
                ItemLabel = data.ItemLabel;
            if (data.ItemName != null)
                ItemName = data.ItemName;
            if (data.ItemType != null)
                ItemType = data.ItemType;
            if (data.ItemDescription != null)
                ItemDescription = data.ItemDescription;
        }

        public void ShowInfoCard()
        {
            gameObject.SetActive(true);
        }

        public void HideInfoCard()
        {
            gameObject.SetActive(false);
        }

        private static void CancelFillingTask()
        {
            _cancellationTokenSource?.Cancel();
        }

        public Task FillCardWithData(Sprite labelIcon, IDescription description, ITitled titled, ICategory category)
        {
            try
            {
                CancelFillingTask();
                _cancellationTokenSource = new CancellationTokenSource();
                FillCardWithData(new InfoPanelData(labelIcon, description, titled, category));
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}