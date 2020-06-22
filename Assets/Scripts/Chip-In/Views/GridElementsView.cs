using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Common.UnityEvents;
using DataModels.Interfaces;
using JetBrains.Annotations;
using Repositories.Local;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;

namespace Views
{
    [Binding]
    public sealed class GridElementsView : BaseView, INotifyPropertyChanged
    {
#if UNITY_EDITOR
        [SerializeField, HideInInspector] public int rowsAmount;
#endif
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
        [SerializeField] private CommunityInterestGridItemView itemPrefab;
        [SerializeField] private Sprite defaultSprite;
        [SerializeField, HideInInspector] private List<CommunityInterestGridItemView> items = new List<CommunityInterestGridItemView>(0);
        private int? _newSelectedItemCorrespondingIndex;

        public IntPointerUnityEvent newItemWasSelected;


        public GridElementsView() : base(nameof(GridElementsView))
        {
        }

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

        public async Task FillOneItemWithData(int cellIndex, IIndexedNamedPosterUrl gridItemData, CancellationToken cancellationToken)
        {
            if (cellIndex >= items.Count)
            {
                return;
            }

            items[cellIndex].SetItemText(gridItemData);
            try
            {
                items[cellIndex].SetImage(await downloadedSpritesRepository.CreateLoadSpriteTask(gridItemData.PosterUri, cancellationToken));
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintLog(nameof(GridElementsView), $"Image was not loaded {gridItemData.PosterUri}");
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public void ClearItems()
        {
            foreach (var interestGridItemView in items)
            {
                interestGridItemView.SetItemImageAndText(-1, "", defaultSprite);
                interestGridItemView.ItemSelected += OnNewItemSelected;
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

        private void OnNewItemSelected(int? index)
        {
            System.Diagnostics.Debug.Assert(index != null, nameof(index) + " != null");
            OnNewItemWasSelected(index);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnNewItemWasSelected(int? index)
        {
            newItemWasSelected?.Invoke(index);
        }
    }
}