using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Extensions;
using HttpRequests;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;
using ViewModels.UI.Elements.Icons;
using WebOperationUtilities;

namespace Repositories.Local
{
    [CreateAssetMenu(fileName = nameof(DownloadedSpritesRepository), menuName = nameof(Repositories) + "/" + nameof(Local) + "/"
                                                                                + nameof(DownloadedSpritesRepository), order = 0)]
    public sealed class DownloadedSpritesRepository : ScriptableObject
    {
        private sealed class DownloadHandleSprite
        {
            public event Action<Sprite> Loaded;

            public readonly string Url;
            public Sprite LoadedSprite { get; private set; }

            public bool IsLoaded { get; private set; }

            public DownloadHandleSprite(string url, Action<Sprite> onSpriteLoaded)
            {
                Url = url;
                Loaded += onSpriteLoaded;
            }

            public Task InvokeDownloading()
            {
                var loadedTexture = ImagesDownloadingUtility.TryDownloadImageAsync(ApiHelper.DefaultClient, Url);
                return loadedTexture.ContinueWithOnMainThread(async delegate(Task<Texture2D> task)
                {
                    var texture = await task;
                    LoadedSprite = SpritesUtility.CreateSpriteWithDefaultParameters(texture);
                    IsLoaded = true;
                    OnLoaded(LoadedSprite);
                });
            }

            public static Task DownloadFromUri(string url, Action<Sprite> onSpriteLoaded)
            {
                var downloadHandle = new DownloadHandleSprite(url, onSpriteLoaded);
                return downloadHandle.InvokeDownloading();
            }

            private void OnLoaded(Sprite obj)
            {
                Loaded?.Invoke(obj);
                Loaded = null;
            }
        }

        public readonly struct SpriteDownloadingTaskParameters
        {
            public readonly string Url;
            public readonly Action<Sprite> Callback;

            public SpriteDownloadingTaskParameters(string url, Action<Sprite> callback)
            {
                Assert.IsFalse(string.IsNullOrEmpty(url));
                Url = url;
                Callback = callback;
            }

            public SpriteDownloadingTaskParameters(string url, ISettableSprite settableSpriteInterface)
            {
                Assert.IsFalse(string.IsNullOrEmpty(url));
                Url = url;
                Callback = settableSpriteInterface.SetAvatarSprite;
            }
        }

        private readonly List<DownloadHandleSprite> _spritesDownloadHandles = new List<DownloadHandleSprite>();


        private void OnEnable()
        {
            _spritesDownloadHandles.Clear();
        }

        private bool SpriteIsAlreadyLoaded(string url, out DownloadHandleSprite handleSprite)
        {
            handleSprite = _spritesDownloadHandles.Find(downloadHandleSprite => downloadHandleSprite != null &&
                                                                                downloadHandleSprite.Url == url);
            return handleSprite != null;
        }

        public async Task TryToLoadSpriteAsync(SpriteDownloadingTaskParameters parameters)
        {
            try
            {
                await CreateLoadSpriteTask(parameters);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public async Task TryToLoadSpritesAsync(IReadOnlyList<SpriteDownloadingTaskParameters> parameters)
        {
            try
            {
                await Task.WhenAll(CreateLoadSpritesTasks(parameters));
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public Task CreateLoadSpriteTask(SpriteDownloadingTaskParameters parameters)
        {
            if (SpriteIsAlreadyLoaded(parameters.Url, out var downloadHandleSprite))
            {
                if (downloadHandleSprite.IsLoaded)
                    parameters.Callback(downloadHandleSprite.LoadedSprite);
                else
                {
                    downloadHandleSprite.Loaded += parameters.Callback;
                }

                return Task.CompletedTask;
            }

            var downloadHandle = new DownloadHandleSprite(parameters.Url, parameters.Callback);
            _spritesDownloadHandles.Add(downloadHandle);
            return downloadHandle.InvokeDownloading();
        }

        private Task[] CreateLoadSpritesTasks(IReadOnlyList<SpriteDownloadingTaskParameters> parameters)
        {
            var tasks = new Task[parameters.Count];
            for (int i = 0; i < parameters.Count; i++)
            {
                tasks[i] = CreateLoadSpriteTask(parameters[i]);
            }

            return tasks;
        }
    }
}