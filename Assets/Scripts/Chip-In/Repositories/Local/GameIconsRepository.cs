using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using CustomAnimators;
using DataModels.Interfaces;
using DataModels.MatchModels;
using UnityEngine;
using Utilities;
using WebOperationUtilities;

namespace Repositories.Local
{
    [CreateAssetMenu(fileName = nameof(GameIconsRepository), menuName = nameof(Repositories) + "/" + nameof(Local) + "/" +
                                                                        nameof(GameIconsRepository), order = 0)]
    public class GameIconsRepository : ScriptableObject
    {
        public event Action IconsSetWasLoaded;
        
        [SerializeField] private int rowsNumber = 5;
        [SerializeField] private int columnsNumber = 5;

        private BoardIconData[] _boardIconsData;
        private bool _iconsSetIsLoaded;

        public bool IconsSetIsLoaded => _iconsSetIsLoaded;

        public BoardIconData[] BoardIconsData => _boardIconsData;

        public async Task LoadBoardIconsSetFromUrls(IReadOnlyList<IndexedUrl> indexedUrls)
        {
            _iconsSetIsLoaded = false;
            var boardIcons = await SpritesAnimationResourcesCreator.CreateBoardIcons(indexedUrls, rowsNumber, columnsNumber);
            _boardIconsData = boardIcons.ToArray();
            _iconsSetIsLoaded = true;
            OnIconsSetWasLoaded();
        }
        
        private static class SpritesAnimationResourcesCreator
        {
            public static List<SimpleImageAnimator.SpritesAnimatorResource> CreateAnimatorResourcesFromSpritesSheetsTextures(IReadOnlyList<Texture2D> spritesSheets, int rowsNumber,
                    int columnsNumber)
            {
                var spritesSheetsList = new List<SimpleImageAnimator.SpritesSheet>(spritesSheets.Count);
                for (int i = 0; i < spritesSheets.Count; i++)
                {
                    spritesSheetsList.Add(new SimpleImageAnimator.SpritesSheet(spritesSheets[i], rowsNumber, columnsNumber));
                }

                return CreateAnimatorResourcesFromSpritesSheets(spritesSheetsList);
            }

            public static List<BoardIconData> CreateBoardIcons(
                IReadOnlyList<IndexedTexture> indexedTextures,
                int rowsNumber, int columnsNumber)
            {
                var boardIcons = new List<BoardIconData>(indexedTextures.Count);
                for (var index = 0; index < indexedTextures.Count; index++)
                {
                    boardIcons.Add(new BoardIconData(
                        new SimpleImageAnimator.SpritesAnimatorResource(new SimpleImageAnimator.SpritesSheet(indexedTextures[index].SpriteSheetTexture, rowsNumber,
                                columnsNumber)),
                        indexedTextures[index].CorrespondingIndex));
                }

                return boardIcons;
            }

            public static async Task<List<BoardIconData>> CreateBoardIcons(IReadOnlyList<IndexedUrl> boardElementsData,
                int rowsNumber, int columnsNumber)
            {
                try
                {
                    var indexedTextures = await CreateIndexedTextures(boardElementsData);
                    return CreateBoardIcons(indexedTextures, rowsNumber, columnsNumber);
                }
                catch (Exception e)
                {
                    LogUtility.PrintLogException(e);
                    throw;
                }
            }

            public static List<SimpleImageAnimator.SpritesAnimatorResource> CreateAnimatorResourcesFromSpritesSheets(
                IReadOnlyList<SimpleImageAnimator.SpritesSheet> spritesSheets)
            {
                var resources = new List<SimpleImageAnimator.SpritesAnimatorResource>(spritesSheets.Count);
                for (int i = 0; i < spritesSheets.Count; i++)
                {
                    resources.Add(new SimpleImageAnimator.SpritesAnimatorResource(spritesSheets[i]));
                }

                return resources;
            }

            public static async Task<List<IndexedTexture>> CreateIndexedTextures(IReadOnlyList<IndexedUrl> indexedUrls)
            {
                try
                {
                    var elementsCount = indexedUrls.Count;

                    var uris = new string[elementsCount];

                    for (int i = 0; i < elementsCount; i++)
                    {
                        uris[i] = indexedUrls[i].Url;
                    }

                    var textures = await ImagesDownloadingUtility.TryDownloadImagesArray(uris);

                    var indexedTextures = new List<IndexedTexture>(elementsCount);

                    for (var index = 0; index < elementsCount; index++)
                    {
                        indexedTextures.Add(new IndexedTexture(textures[index], indexedUrls[index].Id));
                    }

                    return indexedTextures;
                }
                catch (Exception e)
                {
                    LogUtility.PrintLogException(e);
                    throw;
                }
            }
        }

        protected virtual void OnIconsSetWasLoaded()
        {
            IconsSetWasLoaded?.Invoke();
        }
    }
}