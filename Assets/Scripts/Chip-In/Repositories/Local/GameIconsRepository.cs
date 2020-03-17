using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
                IReadOnlyList<byte[]> textures = await ImagesDownloadingUtility.DownloadMultipleDataArrayFromUrls(indexedUrls);
                SaveIconsData(textures, indexedUrls);
                FillBoardIconsData(SpritesAnimationResourcesCreator.CreateBoardIcons(textures, indexedUrls,
                    rowsNumber, columnsNumber));
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private void FillBoardIconsData(IEnumerable<BoardIconData> indexedTextures)
        {
            _boardIconsData = indexedTextures.ToArray();
        }

        private class ImageStoringHeader : IIdentifier, IUrl
        {
            public int? Id { get; set; }
            public string Url { get; set; }
            public int RawImageSize { get; }

            public ImageStoringHeader(int index, string url, int rawImageSize)
            {
                Id = index;
                Url = url;
                RawImageSize = rawImageSize;
            }

            public static ImageStoringHeader Create(int? id, IUrl urlModel, int rawImageSize)
            {
                Debug.Assert(id != null, "identifierModel.Id != null");
                return new ImageStoringHeader((int) id, urlModel.Url, rawImageSize);
            }
        }

        private class ImagesStoringHeaderDataModel
        {
            public ImageStoringHeader[] ImagesStoringHeaders { get; }
            public ImageStoringHeader this[int index] => ImagesStoringHeaders[index];

            public ImagesStoringHeaderDataModel(ImageStoringHeader[] imagesStoringHeaders)
            {
                ImagesStoringHeaders = imagesStoringHeaders;
            }


            public static ImagesStoringHeaderDataModel Create(IReadOnlyList<IndexedUrl> indexedUrls, IReadOnlyList<byte[]> imagesBytes)
            {
                var count = indexedUrls.Count;
                Assert.IsTrue(count > 0);
                Assert.IsTrue(count == indexedUrls.Count && count == imagesBytes.Count);

                var imagesStoringHeaders = new ImageStoringHeader[count];

                for (int i = 0; i < count; i++)
                {
                    imagesStoringHeaders[i] = ImageStoringHeader.Create(indexedUrls[i].Id, indexedUrls[i], imagesBytes[i].Length);
                }

                return new ImagesStoringHeaderDataModel(imagesStoringHeaders);
            }
        }

        private byte[] MergeIntoSingleArray(IReadOnlyList<byte[]> bytesArrays)
        {
            var mergedList = new List<byte>(CalculateTotalLength(bytesArrays));
            foreach (var array in bytesArrays)
            {
                mergedList.AddRange(array);
            }

            return mergedList.ToArray();
        }

        private int CalculateTotalLength(IReadOnlyList<byte[]> bytesArrays)
        {
            int length = 0;
            for (int i = 0; i < bytesArrays.Count; i++)
            {
                length += bytesArrays[i].Length;
            }

            return length;
        }

        private void SaveIconsData(IReadOnlyList<byte[]> indexedTexturesBytesData, IReadOnlyList<IndexedUrl> indexedUrls)
        {
            var imagesStoringHeaderData = ImagesStoringHeaderDataModel.Create(indexedUrls, indexedTexturesBytesData);
            var json = JsonConverterUtility.ConvertModelToJson(imagesStoringHeaderData);

            File.WriteAllText(IconsDataHeaderFilePath, json);
            File.WriteAllBytes(IconsDataFilePath, MergeIntoSingleArray(indexedTexturesBytesData));
        }

        private void LoadIconsData(ImagesStoringHeaderDataModel storingHeaderDataModel, byte[] packedIconsData)
        {
            var imagesBytesList = RestoreImagesBytesList(storingHeaderDataModel.ImagesStoringHeaders, packedIconsData);
            FillBoardIconsData(SpritesAnimationResourcesCreator.CreateBoardIcons(imagesBytesList,
                storingHeaderDataModel.ImagesStoringHeaders, rowsNumber, columnsNumber));
        }


        private static ImagesStoringHeaderDataModel LoadStoringHeaderData()
        {
            var headersAsString = File.ReadAllText(IconsDataHeaderFilePath);
            return JsonConverterUtility.ConvertJsonString<ImagesStoringHeaderDataModel>(headersAsString);
        }

        private static byte[] LoadPackedIconsData()
        {
            return File.ReadAllBytes(IconsDataFilePath);
        }


        private static List<byte[]> RestoreImagesBytesList(IReadOnlyList<ImageStoringHeader> imageStoringHeaders, byte[] packedImagesArray)
        {
            int fromIndex = 0, toIndex = -1;
            var listOfImagesRawData = new List<byte[]>(imageStoringHeaders.Count);

            for (int i = 0; i < imageStoringHeaders.Count; i++)
            {
                var sizeOfCurrentImage = imageStoringHeaders[i].RawImageSize;
                AdjustFromToIndexes(sizeOfCurrentImage);
                var cutCompressedBytesArray = CutPartOfArray(packedImagesArray, fromIndex, sizeOfCurrentImage);
                listOfImagesRawData.Add(cutCompressedBytesArray);
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
            public static List<BoardIconData> CreateBoardIcons(IReadOnlyList<SimpleImageAnimator.SpritesSheet> spritesSheets)
            {
                var boardIcons = new List<BoardIconData>(spritesSheets.Count);
                for (var i = 0; i < spritesSheets.Count; i++)
                {
                    boardIcons.Add(new BoardIconData(new SimpleImageAnimator.SpritesAnimatorResource(spritesSheets[i]),
                        spritesSheets[i]));
                }

                return boardIcons;
            }

            public static List<BoardIconData> CreateBoardIcons(IReadOnlyList<byte[]> textures,
                IReadOnlyList<IIdentifier> boardElementsIdentifiers, int rowsNumber, int columnsNumber)
            {
                var indexedTextures = CreateIndexedTextures(textures, boardElementsIdentifiers, rowsNumber, columnsNumber);
                return CreateBoardIcons(indexedTextures);
            }


            public static List<SimpleImageAnimator.SpritesSheet> CreateIndexedTextures(IReadOnlyList<byte[]> textures,
                IReadOnlyList<IIdentifier> identifiers, int rowsNumber, int columnsNumber)
            {
                Assert.IsTrue(textures.Count == identifiers.Count);

                var elementsCount = textures.Count;
                var indexedTextures = new List<SimpleImageAnimator.SpritesSheet>(elementsCount);

                for (var index = 0; index < elementsCount; index++)
                {
                    var texture = new Texture2D(0, 0);
                    texture.LoadImage(textures[index]);

                    indexedTextures.Add(new SimpleImageAnimator.SpritesSheet(texture, rowsNumber, columnsNumber,
                        identifiers[index].Id));
                }

                return indexedTextures;
            }
        }
    }
}