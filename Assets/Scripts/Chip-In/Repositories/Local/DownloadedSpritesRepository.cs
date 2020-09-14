using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HttpRequests;
using Tasking;
using UnityEngine;
using Utilities;
using WebOperationUtilities;

namespace Repositories.Local
{
    [CreateAssetMenu(fileName = nameof(DownloadedSpritesRepository), menuName = nameof(Repositories) + "/" + nameof(Local) + "/"
                                                                                + nameof(DownloadedSpritesRepository), order = 0)]
    public sealed class DownloadedSpritesRepository : ScriptableObject
    {
        [SerializeField] private Sprite iconPlaceholder;

        public Sprite IconPlaceholder => iconPlaceholder;

        private const string Tag = nameof(DownloadedSpritesRepository);


        private sealed class DownloadHandleSprite
        {
            public readonly string Url;
            public Sprite LoadedSprite { get; private set; }
            public Task<Sprite> LoadingTask { get; private set; }

            public bool IsLoaded => LoadedSprite;
            public bool IsLoading => !IsLoaded && LoadingTask != null;
            private readonly bool _isLocalFile;

            public DownloadHandleSprite(string url, bool isLocalFile)
            {
                Url = url;
                _isLocalFile = isLocalFile;
            }

            public async Task<Sprite> InvokeDownloading(CancellationToken cancellationToken, TaskFactory mainThreadTaskFactory, Sprite defaultIcon)
            {
                try
                {
                    if (_isLocalFile)
                    {
                        LoadingTask = SpritesUtility.CreateSpriteFromPathAsync(Url);
                        LoadedSprite = await LoadingTask.ConfigureAwait(false);
                        LoadingTask = null;
                        return LoadedSprite;
                    }

                    LoadingTask = ImagesDownloadingUtility.CreateDownloadImageTask(ApiHelper.DefaultClient, mainThreadTaskFactory, Url, cancellationToken);
                    await LoadingTask.ConfigureAwait(false);
                    LoadingTask = null;
                    return LoadedSprite;
                }
                catch (Exception e)
                {
                    LogUtility.PrintLog(Tag, e.Message);
                    return defaultIcon;
                }
            }
        }

        private readonly List<DownloadHandleSprite> _spritesDownloadHandles = new List<DownloadHandleSprite>();
        
        private void OnEnable()
        {
            _spritesDownloadHandles.Clear();
        }

        private bool SpriteIsAlreadyLoaded(string url, out DownloadHandleSprite handleSprite)
        {
            handleSprite = _spritesDownloadHandles.Find(downloadHandleSprite => downloadHandleSprite != null && downloadHandleSprite.Url == url);
            return handleSprite != null;
        }

        public Task<Sprite> CreateLoadSpriteTask(string url, CancellationToken cancellationToken, bool isLocalFile = false)
        {
            if (SpriteIsAlreadyLoaded(url, out var downloadHandleSprite))
            {
                if (downloadHandleSprite.IsLoaded)
                {
                    return Task.FromResult(downloadHandleSprite.LoadedSprite);
                }

                if (downloadHandleSprite.IsLoading)
                {
                    return downloadHandleSprite.LoadingTask;
                }


                //TODO: If sprites are loading fine - remove comment below 
                /*try
                {
                    return  downloadHandleSprite.InvokeDownloading(cancellationToken, TasksFactories.MainThreadTaskFactory, IconPlaceholder);
                }
                catch (Exception)
                {
                    return Task.FromResult(IconPlaceholder);
                }*/
            }

            var downloadHandle = new DownloadHandleSprite(url, isLocalFile);
            _spritesDownloadHandles.Add(downloadHandle);
            try
            {
                return downloadHandle.InvokeDownloading(cancellationToken, TasksFactories.MainThreadTaskFactory, IconPlaceholder);
            }
            catch (Exception)
            {
                return Task.FromResult(iconPlaceholder);
            }
        }

        public async Task<Texture2D> CreateLoadTexture2DTask(string url, CancellationToken cancellationToken, bool isLocalFile = false)
        {
            var sprite = await CreateLoadSpriteTask(url, cancellationToken, isLocalFile).ConfigureAwait(false);
            return TasksFactories.ExecuteOnMainThread(() => sprite.texture);
        }

        private Task[] CreateLoadSpritesTasks(IReadOnlyList<string> parameters, in CancellationToken cancellationToken, bool isLocalFile = false)
        {
            var tasks = new Task[parameters.Count];
            for (int i = 0; i < parameters.Count; i++)
            {
                tasks[i] = CreateLoadSpriteTask(parameters[i], cancellationToken, isLocalFile);
            }

            return tasks;
        }
    }
}