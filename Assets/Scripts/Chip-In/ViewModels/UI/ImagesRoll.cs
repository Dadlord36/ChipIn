using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Controllers;
using Repositories.Local;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using Utilities;
using ViewModels.UI.Elements.Icons;
using Debug = System.Diagnostics.Debug;

namespace ViewModels.UI
{
    public class ImagesRoll : UIBehaviour
    {
        [Serializable]
        private struct UserAvatarInRow
        {
            public ushort rowNumber;
            public UserAvatarIcon imageComponent;
            public GameObject owningObject;
            public float scale;

            public UserAvatarInRow(GameObject owningObject, UserAvatarIcon imageComponent, ushort rowNumber)
            {
                this.imageComponent = imageComponent;
                this.rowNumber = rowNumber;
                this.owningObject = owningObject;
                scale = 0f;
            }

            public void SetSprite(Sprite sprite)
            {
                imageComponent.AvatarSprite = sprite;
            }
        }

        private enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
        [SerializeField] private IconEllipsesRepository ellipsesRepository;

        [SerializeField] private GameObject userAvatarIconPrefab;
        [SerializeField] private ushort numberOfImages;
        [SerializeField] private ushort numberOfItemsInRow;
        [SerializeField] private float basicOffset = 10f;
        [SerializeField, Range(0f, 1f)] private float minScale = 0.8f;
        [SerializeField, Range(0f, 1f)] private float scaleFactor;

        [SerializeField] private UserAvatarInRow[] imagesRows;
        [SerializeField] private UserAvatarIcon mainImage;
        [SerializeField] private UserAvatarIcon[] otherIcons;
        private AsyncOperationCancellationController _cancellationController = new AsyncOperationCancellationController();

#if UNITY_EDITOR

        public void GenerateImages(string ellipsesName)
        {
            if (numberOfImages == 0 || numberOfItemsInRow == 0) return;

            RemoveChildrenImages();
            CreateImages(ellipsesName);
            ReverseImagesPositionInHierarchy();
            ResetImagesPositions();
            ScaleImages();
            ShiftImages();
        }

#endif

        public string[] IconsEllipsesOptionsNames => ellipsesRepository.ElementsNames;

        public void SetMainAvatarIconSprite(Sprite sprite)
        {
            mainImage.AvatarSprite = sprite;
        }

        public async Task SetOtherAvatarsIconSprites(IReadOnlyList<string> avatarSprites)
        {
            Assert.IsTrue(otherIcons.Length == avatarSprites.Count);
            try
            {
                _cancellationController.CancelOngoingTask();
                for (int i = 0; i < otherIcons.Length; i++)
                {
                    otherIcons[i].AvatarSprite = await downloadedSpritesRepository.CreateLoadSpriteTask(avatarSprites[i], 
                        _cancellationController.TasksCancellationTokenSource.Token);
                }
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

#if UNITY_EDITOR
        public void CreateImages(string ellipsesName)
        {
            imagesRows = new UserAvatarInRow[numberOfImages];
            var otherIconsList = new List<UserAvatarIcon>(numberOfImages - 1);

            ushort itemsAddedToRow = 0, currentRow = 1;
            var thisTransform = transform;

            mainImage = CreateImage("Main image");
            ResetRectTransformPosition(mainImage.AvatarRectTransform);

            for (int i = 0; i < numberOfImages; i++)
            {
                var userAvatarIcon = AddItemToRow(i, CreateImage($"Image {i.ToString()}"));
                otherIconsList.Add(userAvatarIcon.imageComponent);
                if (itemsAddedToRow == numberOfItemsInRow)
                {
                    ConfirmRowAdding();
                }
            }

            otherIcons = otherIconsList.ToArray();

            UserAvatarIcon CreateImage(string objectName)
            {
                var prefabInstance = PrefabUtility.InstantiatePrefab(userAvatarIconPrefab, thisTransform) as GameObject;
                Debug.Assert(prefabInstance != null, nameof(prefabInstance) + " != null");

                prefabInstance.name = objectName;
                var avatarIcon = prefabInstance.GetComponent<UserAvatarIcon>();
                avatarIcon.Initialize();
                avatarIcon.SetIconEllipseSprite(ellipsesName);
                return avatarIcon;
            }

            UserAvatarInRow AddItemToRow(int arrayIndex, UserAvatarIcon imageItem)
            {
                itemsAddedToRow++;
                return imagesRows[arrayIndex] = new UserAvatarInRow(imageItem.gameObject, imageItem, currentRow);
            }

            void ConfirmRowAdding()
            {
                currentRow++;
                itemsAddedToRow = 0;
            }
        }
#endif
        private void ScaleImages()
        {
            for (int i = 0; i < imagesRows.Length; i++)
            {
                var scale = minScale - imagesRows[i].rowNumber * scaleFactor;
                imagesRows[i].scale = scale;
                imagesRows[i].imageComponent.SetScale(scale);
            }
        }

        private void ResetImagesPositions()
        {
            for (int i = 0; i < imagesRows.Length; i++)
            {
                if (imagesRows[i].imageComponent)
                    ResetRectTransformPosition(imagesRows[i].imageComponent.AvatarRectTransform);
            }
        }

        private static void ResetRectTransformPosition(RectTransform rectTransform)
        {
            rectTransform.anchoredPosition = Vector2.zero;
        }

        private void ShiftImages()
        {
            float calculatedOffset = 0f;
            for (int i = 0; i < imagesRows.Length; i += numberOfItemsInRow)
            {
                calculatedOffset += basicOffset * imagesRows[i].scale;

                Shift(calculatedOffset, FormatRow(i, numberOfItemsInRow));
            }

            UserAvatarInRow[] FormatRow(int fromIndex, int length)
            {
                var chunk = new UserAvatarInRow[length];
                Array.Copy(imagesRows, fromIndex, chunk, 0, length);
                return chunk;
            }

            void Shift(float offset, UserAvatarInRow[] rowElements)
            {
                bool shouldChangeDirection = false;
                for (int i = 0; i < rowElements.Length; i++)
                {
                    ShiftRectTransformHorizontal(rowElements[i].imageComponent.AvatarRectTransform, offset,
                        shouldChangeDirection ? Direction.Left : Direction.Right);
                    shouldChangeDirection = !shouldChangeDirection;
                }
            }
        }

        private static void ShiftRectTransformHorizontal(RectTransform rectTransform, float offset,
            Direction shiftDirection)
        {
            var anchoredPosition = rectTransform.anchoredPosition;

            switch (shiftDirection)
            {
                case Direction.Up:
                    anchoredPosition.y += offset;
                    break;
                case Direction.Down:
                    anchoredPosition.y -= offset;
                    break;
                case Direction.Left:
                    anchoredPosition.x -= offset;
                    break;
                case Direction.Right:
                    anchoredPosition.x += offset;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(shiftDirection), shiftDirection, null);
            }

            rectTransform.anchoredPosition = anchoredPosition;
        }

        private void ReverseImagesPositionInHierarchy()
        {
            var imagesObjects = new List<Transform>(imagesRows.Length);
            for (int i = 0; i < imagesRows.Length; i++)
            {
                imagesObjects.Add(imagesRows[i].owningObject.transform);
            }

            imagesObjects.Reverse();
            for (int i = 0; i < imagesObjects.Count; i++)
            {
                imagesObjects[i].SetSiblingIndex(i);
            }
        }

        private void RemoveChildrenImages()
        {
            if (imagesRows == null) return;

            foreach (var imageInRow in imagesRows)
            {
                if (imageInRow.owningObject)
                    DestroyImmediate(imageInRow.owningObject);
            }

            if (mainImage)
                DestroyImmediate(mainImage.gameObject);
        }
    }
}