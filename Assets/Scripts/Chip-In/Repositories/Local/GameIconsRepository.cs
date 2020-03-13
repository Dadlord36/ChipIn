using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Common;
using CustomAnimators;
using DataModels.Interfaces;
using DataModels.MatchModels;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;
using WebOperationUtilities;

namespace Repositories.Local
{
    public interface IRestorable
    {
        void Restore();
    }

    [CreateAssetMenu(fileName = nameof(GameIconsRepository), menuName = nameof(Repositories) + "/" + nameof(Local) + "/" +
                                                                        nameof(GameIconsRepository), order = 0)]
    public sealed class GameIconsRepository : ScriptableObject, IRestorable
    {
        private const string Tag = nameof(GameIconsRepository);

        public event Action IconsSetWasLoaded;

        [SerializeField] private int rowsNumber = 5;
        [SerializeField] private int columnsNumber = 5;

        private const string IconsDataFileName = "IconsData";
        private const string IconsDataHeaderFileName = "IconsHeaders";

        private static string IconsDataFilePath => Path.Combine(Application.persistentDataPath, IconsDataFileName);
        private static string IconsDataHeaderFilePath => Path.Combine(Application.persistentDataPath, IconsDataHeaderFileName);

        private BoardIconData[] _boardIconsData;
        private bool _iconsSetIsLoaded;

        public bool IconsSetIsLoaded => _iconsSetIsLoaded;

        public BoardIconData[] BoardIconsData => _boardIconsData;


        public async Task StoreNewGameIconsSet(IReadOnlyList<IndexedUrl> indexedUrls)
        {
            _iconsSetIsLoaded = false;
            await DownloadBoardIconsSetFromUrls(indexedUrls);
            _iconsSetIsLoaded = true;
            OnIconsSetWasLoaded();
        }

        private async Task DownloadBoardIconsSetFromUrls(IReadOnlyList<IndexedUrl> indexedUrls)
        {
            try
            {
                var indexedTextures = await SpritesAnimationResourcesCreator.CreateIndexedTextures(indexedUrls);
                SaveIconsData(indexedTextures, indexedUrls);
                FillBoardIconsData(indexedTextures);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private BoardIconData[] GenerateSpritesAnimation(IReadOnlyList<IndexedTexture> indexedTextures)
        {
            return SpritesAnimationResourcesCreator.CreateBoardIcons(indexedTextures, rowsNumber, columnsNumber).ToArray();
        }

        private void FillBoardIconsData(IReadOnlyList<IndexedTexture> indexedTextures)
        {
            _boardIconsData = GenerateSpritesAnimation(indexedTextures);
        }

        private int[] GetArrayOfImageBitmapsSizes(IReadOnlyList<byte[]> listOfPngData)
        {
            var bitmapsSizes = new int[listOfPngData.Count];
            for (var index = 0; index < listOfPngData.Count; index++)
            {
                bitmapsSizes[index] = listOfPngData[index].Length;
            }

            return bitmapsSizes;
        }

        private List<byte[]> GetPngsAsByteArrays(IReadOnlyList<IndexedTexture> indexedTextures)
        {
            var pngsBytes = new List<byte[]>(indexedTextures.Count);

            for (int i = 0; i < indexedTextures.Count; i++)
            {
                pngsBytes.Add(indexedTextures[i].SpriteSheetTexture.GetRawTextureData());
            }

            return pngsBytes;
        }

        private class ImageStoringHeader
        {
            public int Index { get; }
            public string ImageUrl { get; }
            public int Width { get; }
            public int Height { get; }
            public int RawImageSize { get; }
            public TextureFormat TextureFormat { get; }

            public ImageStoringHeader(int index, string imageUrl, int rawImageSize, int width, int height, TextureFormat textureFormat)
            {
                Index = index;
                ImageUrl = imageUrl;
                RawImageSize = rawImageSize;
                Width = width;
                Height = height;
                TextureFormat = textureFormat;
            }

            public static ImageStoringHeader Create(IIdentifier identifierModel, IUrl urlModel, int rawImageSize, int width, int height, TextureFormat textureFormat)
            {
                Debug.Assert(identifierModel.Id != null, "identifierModel.Id != null");
                return new ImageStoringHeader((int) identifierModel.Id, urlModel.Url, rawImageSize, width, height, textureFormat);
            }
        }

        private class ImagesStoringHeaderDataModel
        {
            public ImageStoringHeader[] ImagesStoringHeaders { get; set; }
            public ImageStoringHeader this[int index] => ImagesStoringHeaders[index];

            public ImagesStoringHeaderDataModel(ImageStoringHeader[] imagesStoringHeaders)
            {
                ImagesStoringHeaders = imagesStoringHeaders;
            }


            public static ImagesStoringHeaderDataModel Create(IReadOnlyList<IndexedTexture> indexedTextures, IReadOnlyList<IndexedUrl> indexedUrls, IReadOnlyList<byte[]> pngsBytes)
            {
                var count = indexedTextures.Count;
                Assert.IsTrue(count > 0);
                Assert.IsTrue(count == indexedUrls.Count && count == pngsBytes.Count);

                var imagesStoringHeaders = new ImageStoringHeader[count];

                for (int i = 0; i < count; i++)
                {
                    var texture = indexedTextures[i].SpriteSheetTexture;
                    imagesStoringHeaders[i] = ImageStoringHeader.Create(indexedTextures[i], indexedUrls[i], pngsBytes[i].Length, texture.width, texture.height, texture.format);
                }

                return new ImagesStoringHeaderDataModel(imagesStoringHeaders);
            }
        }

        private byte[] MergeIntoSingleArray(IEnumerable<byte[]> bytesArrays)
        {
            var mergedList = new List<byte>();
            foreach (var array in bytesArrays)
            {
                mergedList.AddRange(array);
            }

            return mergedList.ToArray();
        }

        private void SaveIconsData(IReadOnlyList<IndexedTexture> indexedTextures, IReadOnlyList<IndexedUrl> indexedUrls)
        {
            var pngsBytes = GetPngsAsByteArrays(indexedTextures);
            var imagesStoringHeaderData = ImagesStoringHeaderDataModel.Create(indexedTextures, indexedUrls, pngsBytes);
            var json = JsonConverterUtility.ConvertModelToJson(imagesStoringHeaderData);

            File.WriteAllText(IconsDataHeaderFilePath, json);
            File.WriteAllBytes(IconsDataFilePath, MergeIntoSingleArray(pngsBytes));
        }


        private ImagesStoringHeaderDataModel LoadStoringHeaderData()
        {
            var headersAsString = File.ReadAllText(IconsDataHeaderFilePath);
            return JsonConverterUtility.ConvertJsonString<ImagesStoringHeaderDataModel>(headersAsString);
        }

        private byte[] LoadPackedIconsData()
        {
            return File.ReadAllBytes(IconsDataFilePath);
        }

        private void LoadIconsData(ImagesStoringHeaderDataModel storingHeaderDataModel, byte[] packedIconsData)
        {
            var imagesBytesList = GetImagesBytesList(storingHeaderDataModel.ImagesStoringHeaders, packedIconsData);
            var indexedTextures = new List<IndexedTexture>();

            for (int i = 0; i < storingHeaderDataModel.ImagesStoringHeaders.Length; i++)
            {
                var headerData = storingHeaderDataModel[i];
                indexedTextures.Add(new IndexedTexture(imagesBytesList[i], headerData.Index, storingHeaderDataModel[i].Width, storingHeaderDataModel[i].Height, headerData.TextureFormat));
            }

            FillBoardIconsData(indexedTextures);
        }

        private List<byte[]> GetImagesBytesList(ImageStoringHeader[] imageStoringHeaders, byte[] packedImagesArray)
        {
            int fromIndex = 0, toIndex = -1;
            var listOfImagesRawData = new List<byte[]>(imageStoringHeaders.Length);

            for (int i = 0; i < imageStoringHeaders.Length; i++)
            {
                var sizeOfCurrentImage = imageStoringHeaders[i].RawImageSize;
                AdjustFromToIndexes(sizeOfCurrentImage);
                listOfImagesRawData.Add(CutPartOfArray(packedImagesArray, fromIndex, sizeOfCurrentImage));
            }

            void AdjustFromToIndexes(int rawImageSize)
            {
                fromIndex = toIndex + 1;
                toIndex += rawImageSize;
            }

            byte[] CutPartOfArray(byte[] arrayToGetPartFrom, int from, int length)
            {
                byte[] array = new byte[length];
                Array.Copy(arrayToGetPartFrom, from, array, 0, length);
                return array;
            }

            return listOfImagesRawData;
        }


        private void OnIconsSetWasLoaded()
        {
            IconsSetWasLoaded?.Invoke();
        }

        private bool RestoringDataExists()
        {
            return File.Exists(IconsDataHeaderFilePath);
        }

        public void Restore()
        {
            if (!RestoringDataExists()) return;
            var storingHeaders = LoadStoringHeaderData();
            var packedIconsData = LoadPackedIconsData();
            LoadIconsData(storingHeaders, packedIconsData);
            LogUtility.PrintLog(Tag, "Icons data was restored from save file");
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
                    boardIcons.Add(new BoardIconData(new SimpleImageAnimator.SpritesAnimatorResource(new SimpleImageAnimator.SpritesSheet(indexedTextures[index].SpriteSheetTexture,
                        rowsNumber, columnsNumber)), indexedTextures[index].Id));
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