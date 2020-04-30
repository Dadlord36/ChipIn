using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.Assertions;
using WebOperationUtilities;

namespace Views
{
    public class GridElementsView : BaseView
    {
#if UNITY_EDITOR
        [SerializeField, HideInInspector] public int rowsAmount;
#endif

        private int _lastFilledGridItemIndex;

        [SerializeField] private CommunityInterestGridItemView itemPrefab;
        [SerializeField] private Sprite defaultSprite;

        [SerializeField, HideInInspector] private List<CommunityInterestGridItemView> items = new List<CommunityInterestGridItemView>(0);

        public void AddEmptyItemsRow()
        {
            var itemsRow = new CommunityInterestGridItemView[3];

            for (var i = 0; i < itemsRow.Length; i++)
            {
                itemsRow[i] = Instantiate(itemPrefab, transform);
            }

            items.AddRange(itemsRow);
        }

        public void AddEmptyItemsRows(uint rows)
        {
            RemoveItems();

            if (rows == 0)
            {
                Debug.unityLogger.Log(LogType.Error, nameof(GridElementsView), "Rows amount can't be 0");
            }

            for (var i = 0; i < rows; i++)
            {
                AddEmptyItemsRow();
            }
        }

        public async void FillOneItemWithData(CommunityBasicDataModel gridItemData)
        {
            Assert.IsTrue(_lastFilledGridItemIndex < items.Count);

            var downloadImageTask = ImagesDownloadingUtility.TryDownloadImageAsync(gridItemData.PosterUri);
            await downloadImageTask.ContinueWith(async delegate(Task<Texture2D> task)
            {
                var texture = await task;
                var sprite = SpritesUtility.CreateSpriteWithDefaultParameters(texture);
                items[_lastFilledGridItemIndex].SetItemImageAndText(gridItemData, sprite);
            }, TaskScheduler.FromCurrentSynchronizationContext());


            _lastFilledGridItemIndex++;
        }

        public void ClearItems()
        {
            _lastFilledGridItemIndex = 0;
            foreach (var interestGridItemView in items)
            {
                interestGridItemView.SetItemImageAndText(-1, "", defaultSprite);
            }
        }

        private void RemoveItems()
        {
            items.Clear();

            var gameObjects = new List<GameObject>();
            foreach (Transform child in transform)
            {
                gameObjects.Add(child.gameObject);
            }

            for (int i = 0; i < gameObjects.Count; i++)
            {
                var gO = gameObjects[i];
#if UNITY_EDITOR
                DestroyImmediate(gO);
#else
                            Destroy(gO);
#endif
            }
        }
    }
}