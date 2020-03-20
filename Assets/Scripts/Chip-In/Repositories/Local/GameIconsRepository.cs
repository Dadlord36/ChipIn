﻿using System;
using System.Collections.Generic;
using System.IO;
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
        private class BoardIconsSetsContainer
        {
            private struct BoardIconsSet
            {
                public readonly int GameId;
                public readonly BoardIconData[] BoardIconsSetData;

                public BoardIconsSet(int gameId, BoardIconData[] boardIconsSetData)
                {
                    GameId = gameId;
                    BoardIconsSetData = boardIconsSetData;
                }
            }

            private readonly List<BoardIconsSet> _boardIconsSets = new List<BoardIconsSet>();

            public void AddIconsSet(int gameId, BoardIconData[] boardIconsSetData)
            {
                _boardIconsSets.Add(new BoardIconsSet(gameId, boardIconsSetData));
            }

            private int FindBoardIconsSetIndex(int gameId) => _boardIconsSets.FindIndex(set => set.GameId == gameId);
            private BoardIconsSet FindBoardIconsSet(int gameId) => _boardIconsSets.Find(set => set.GameId == gameId);

            public BoardIconData[] GetIconsSet(int gameId) => FindBoardIconsSet(gameId).BoardIconsSetData;

            public void Remove(int gameId)
            {
                _boardIconsSets.RemoveAt(FindBoardIconsSetIndex(gameId));
            }
        }


        private const string Tag = nameof(GameIconsRepository);

        public event Action IconsSetWasLoaded;

        [SerializeField] private int rowsNumber = 5;
        [SerializeField] private int columnsNumber = 5;

        private const string IconsDataFileName = "IconsData";
        private const string IconsDataHeaderFileName = "IconsHeaders";
        private const string GameIconsDirectoryName = "GameIcons";

        private static string GameIconsDirectoryPath => Path.Combine(Application.persistentDataPath, GameIconsDirectoryName);
        private static string GetGameIdDirectory(int gameId) => Path.Combine(GameIconsDirectoryPath, gameId.ToString());

        private static string CreateIconsDataFilePath(int gameId) => Path.Combine(new[]
        {
            GetGameIdDirectory(gameId), IconsDataFileName
        });

        private static string CreateIconsDataHeaderFilePath(int gameId) => Path.Combine(new[]
        {
            GetGameIdDirectory(gameId), IconsDataHeaderFileName
        });


        private bool _iconsSetIsLoaded;
        public bool IconsSetIsLoaded => _iconsSetIsLoaded;

        private readonly BoardIconsSetsContainer _boardIconsSetsContainer = new BoardIconsSetsContainer();
        public BoardIconData[] GetBoardIconsData(int gameId) => _boardIconsSetsContainer.GetIconsSet(gameId);


        public async Task StoreNewGameIconsSet(int gameId, IReadOnlyList<IndexedUrl> indexedUrls)
        {
            _iconsSetIsLoaded = false;
            await DownloadBoardIconsSetFromUrls(gameId, indexedUrls);
            _iconsSetIsLoaded = true;
            OnIconsSetWasLoaded();
        }

        private async Task DownloadBoardIconsSetFromUrls(int gameId, IReadOnlyList<IndexedUrl> indexedUrls)
        {
            try
            {
                IReadOnlyList<byte[]> textures = await ImagesDownloadingUtility.DownloadMultipleDataArrayFromUrls(indexedUrls);
                SaveIconsData(gameId, textures, indexedUrls);
                FillBoardIconsData(gameId, SpritesAnimationResourcesCreator.CreateBoardIcons(textures, indexedUrls,
                    rowsNumber, columnsNumber).ToArray());
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public void RemoveGameIconsData(int gameId)
        {
            _boardIconsSetsContainer.Remove(gameId);
            RemoveDirectoryAndFilesInIt(GetGameIdDirectory(gameId));
        }

        private static void RemoveDirectoryAndFilesInIt(string directory)
        {
            var directoryInfo = new DirectoryInfo(directory);

            var files = directoryInfo.GetFiles();
            for (var index = 0; index < files.Length; index++)
            {
                files[index].Delete();
            }

            var dirs = directoryInfo.GetDirectories();
            for (var index = 0; index < dirs.Length; index++)
            {
                dirs[index].Delete(true);
            }

            directoryInfo.Delete();
        }

        private void FillBoardIconsData(int gameId, BoardIconData[] boardIconData)
        {
            _boardIconsSetsContainer.AddIconsSet(gameId, boardIconData);
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
            public readonly int GameId;
            public ImageStoringHeader[] ImagesStoringHeaders { get; }
            public ImageStoringHeader this[int index] => ImagesStoringHeaders[index];

            public ImagesStoringHeaderDataModel(int gameId, ImageStoringHeader[] imagesStoringHeaders)
            {
                GameId = gameId;
                ImagesStoringHeaders = imagesStoringHeaders;
            }


            public static ImagesStoringHeaderDataModel Create(int gameId, IReadOnlyList<IndexedUrl> indexedUrls, IReadOnlyList<byte[]> imagesBytes)
            {
                var count = indexedUrls.Count;
                Assert.IsTrue(count > 0);
                Assert.IsTrue(count == indexedUrls.Count && count == imagesBytes.Count);

                var imagesStoringHeaders = new ImageStoringHeader[count];

                for (int i = 0; i < count; i++)
                {
                    imagesStoringHeaders[i] = ImageStoringHeader.Create(indexedUrls[i].Id, indexedUrls[i], imagesBytes[i].Length);
                }

                return new ImagesStoringHeaderDataModel(gameId, imagesStoringHeaders);
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

        private void CheckIfExistsOrCreateDirectory()
        {
        }

        private void SaveIconsData(int gameId, IReadOnlyList<byte[]> indexedTexturesBytesData, IReadOnlyList<IndexedUrl> indexedUrls)
        {
            var imagesStoringHeaderData = ImagesStoringHeaderDataModel.Create(gameId, indexedUrls, indexedTexturesBytesData);
            var json = JsonConverterUtility.ConvertModelToJson(imagesStoringHeaderData);

            Directory.CreateDirectory(GetGameIdDirectory(gameId));

            File.WriteAllText(CreateIconsDataHeaderFilePath(gameId), json);
            File.WriteAllBytes(CreateIconsDataFilePath(gameId), MergeIntoSingleArray(indexedTexturesBytesData));
        }

        private struct IconsSetRestoringData
        {
            public readonly ImagesStoringHeaderDataModel StoringHeader;
            public readonly byte[] PackedIconsData;

            private IconsSetRestoringData(ImagesStoringHeaderDataModel storingHeader, byte[] packedIconsData)
            {
                StoringHeader = storingHeader;
                PackedIconsData = packedIconsData;
            }

            public static async Task<IconsSetRestoringData> CreateAsync(int gameId)
            {
                var storingHeadersTask = LoadStoringHeaderData(gameId);
                var packedIconsDataTask = LoadPackedIconsData(gameId);

                var tasks = new List<Task> {storingHeadersTask, packedIconsDataTask};
                await Task.WhenAll(tasks);
                return new IconsSetRestoringData(storingHeadersTask.Result, packedIconsDataTask.Result);
            }
        }

        private void LoadIconsData(IconsSetRestoringData iconsSetRestoringData)
        {
            var imagesStoringHeaders = iconsSetRestoringData.StoringHeader.ImagesStoringHeaders;
            var imagesBytesList = RestoreImagesBytesList(imagesStoringHeaders, iconsSetRestoringData.PackedIconsData);

            var icons = SpritesAnimationResourcesCreator.CreateBoardIcons(imagesBytesList,
                imagesStoringHeaders, rowsNumber, columnsNumber);

            FillBoardIconsData(iconsSetRestoringData.StoringHeader.GameId, icons.ToArray());
        }

        private static async Task<ImagesStoringHeaderDataModel> LoadStoringHeaderData(int gameId)
        {
            var headersAsString = await FilesUtility.ReadFileTextAsync(CreateIconsDataHeaderFilePath(gameId));
            return JsonConverterUtility.ConvertJsonString<ImagesStoringHeaderDataModel>(headersAsString);
        }

        private static Task<byte[]> LoadPackedIconsData(int gameId)
        {
            return FilesUtility.ReadFileBytesAsync(CreateIconsDataFilePath(gameId));
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
            return Directory.Exists(GameIconsDirectoryPath);
        }

        private int[] GetGamesIdsFromGameSubdirectories()
        {
            var subfoldersPaths = Directory.GetDirectories(GameIconsDirectoryPath);
            var GamesIds = new int[subfoldersPaths.Length];

            for (int i = 0; i < subfoldersPaths.Length; i++)
            {
                GamesIds[i] = int.Parse(new DirectoryInfo(subfoldersPaths[i]).Name);
            }

            return GamesIds;
        }

        public async void Restore()
        {
            if (!RestoringDataExists()) return;
            var savedGamesIds = GetGamesIdsFromGameSubdirectories();
            var length = savedGamesIds.Length;
            var tasks = new List<Task<IconsSetRestoringData>>(length);

            for (int i = 0; i < length; i++)
            {
                tasks.Add(IconsSetRestoringData.CreateAsync(savedGamesIds[i]));
            }

            var iconsRestoringData = await Task.WhenAll(tasks);

            for (int i = 0; i < length; i++)
            {
                LoadIconsData(iconsRestoringData[i]);
            }

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
                var indexedTextures = CreateSpritesSheets(textures, boardElementsIdentifiers, rowsNumber, columnsNumber);
                return CreateBoardIcons(indexedTextures);
            }


            public static List<SimpleImageAnimator.SpritesSheet> CreateSpritesSheets(IReadOnlyList<byte[]> textures,
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