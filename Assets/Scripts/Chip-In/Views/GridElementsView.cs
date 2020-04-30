using System.Collections.Generic;
using DataModels;
using Repositories.Local;
using UnityEngine;
using UnityEngine.Assertions;

namespace Views
{
    public class GridElementsView : BaseView
    {
#if UNITY_EDITOR
        [SerializeField, HideInInspector] public int rowsAmount;
#endif


        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
        [SerializeField] private CommunityInterestGridItemView itemPrefab;
        [SerializeField] private Sprite defaultSprite;
        [SerializeField, HideInInspector] private List<CommunityInterestGridItemView> items = new List<CommunityInterestGridItemView>(0);


        private int _lastFilledGridItemIndex;

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

        public void FillOneItemWithData(CommunityBasicDataModel gridItemData)
        {
            Assert.IsTrue(_lastFilledGridItemIndex < items.Count);
            items[_lastFilledGridItemIndex].SetItemText(gridItemData);

            downloadedSpritesRepository.TryToLoadSpriteAsync(new DownloadedSpritesRepository.
                SpriteDownloadingTaskParameters(gridItemData.PosterUri, items[_lastFilledGridItemIndex].SetImage));
            
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