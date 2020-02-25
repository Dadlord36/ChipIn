using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using CustomAnimators;
using DataModels.Interfaces;
using DataModels.MatchModels;
using UnityEngine;
using Utilities;
using Views;
using WebOperationUtilities;

namespace ViewModels
{
    public sealed partial class SlotsGameViewModel
    {
        /// <summary>
        /// Holds array of <see cref="SlotsView"/> Slot Icon resources, that are representing a group by their design,
        /// and gives functionality to get resource, that is corresponding to an ID  
        /// </summary>
        private class BoardIconsSetHolder
        {
            private readonly List<BoardIconData> _boardIcons = new List<BoardIconData>();
            private readonly int _rowsNumber, _columnsNumber;

            private BoardIconData[] BoardIcons
            {
                set
                {
                    _boardIcons.Clear();
                    _boardIcons.Capacity = value.Length;
                    _boardIcons.AddRange(value);
                }
            }

            public BoardIconsSetHolder(int rowsNumber, int columnsNumber)
            {
                _rowsNumber = rowsNumber;
                _columnsNumber = columnsNumber;
            }

            public async Task Refill(IReadOnlyList<IndexedUrl> indexedUrls)
            {
                var boardIcons = await SpritesAnimationResourcesCreator
                    .CreateBoardIcons(indexedUrls, _rowsNumber, _columnsNumber);
                BoardIcons = boardIcons.ToArray();
            }

            public void Refill(IReadOnlyList<IndexedTexture> indexedTextures)
            {
                BoardIcons = SpritesAnimationResourcesCreator
                    .CreateBoardIcons(indexedTextures, _rowsNumber, _columnsNumber).ToArray();
            }


            private BoardIconData GetBordIconDataWithId(int index)
            {
                return _boardIcons.Find(icon => icon.Id == index);
            }

            public List<BoardIconData> GetBoardIconsDataWithIDs(IReadOnlyList<IIconIdentifier> identifiers)
            {
                var sprites = new List<BoardIconData>(identifiers.Count);
                for (int i = 0; i < identifiers.Count; i++)
                {
                    sprites.Add(GetBordIconDataWithId(identifiers[i].IconId));
                }

                return sprites;
            }
        }


        private static class SpritesAnimationResourcesCreator
        {
            public static List<SimpleImageAnimator.SpritesAnimatorResource>
                CreateAnimatorResourcesFromSpritesSheetsTextures(IReadOnlyList<Texture2D> spritesSheets, int rowsNumber,
                    int columnsNumber)
            {
                var spritesSheetsList = new List<SimpleImageAnimator.SpritesSheet>(spritesSheets.Count);
                for (int i = 0; i < spritesSheets.Count; i++)
                {
                    spritesSheetsList.Add(
                        new SimpleImageAnimator.SpritesSheet(spritesSheets[i], rowsNumber, columnsNumber));
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
                        new SimpleImageAnimator.SpritesAnimatorResource(
                            new SimpleImageAnimator.SpritesSheet(indexedTextures[index].SpriteSheetTexture, rowsNumber,
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
    }
}