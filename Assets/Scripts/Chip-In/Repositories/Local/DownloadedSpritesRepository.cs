using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HttpRequests;
using Tasking;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;
using WebOperationUtilities;

namespace Repositories.Local
{
    public interface IDownloadedSpritesRepository
    {
        Sprite IconPlaceholder { get; }
        Task<Sprite> CreateLoadSpriteTask(string url, CancellationToken cancellationToken, bool isLocalFile = false);
        Task<Texture2D> CreateLoadTexture2DTask(string url, CancellationToken cancellationToken, bool isLocalFile = false);
    }

    [CreateAssetMenu(fileName = nameof(DownloadedSpritesRepository), menuName = nameof(Repositories) + "/" + nameof(Local) + "/"
                                                                                + nameof(DownloadedSpritesRepository), order = 0)]
    public sealed class DownloadedSpritesRepository : ScriptableObject, IDownloadedSpritesRepository
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

            public async Task<Sprite> InvokeDownloading(CancellationToken cancellationToken, TaskFactory mainThreadTaskFactory)
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
                    LoadedSprite = await LoadingTask.ConfigureAwait(false);
                    LoadingTask = null;
                    return LoadedSprite;
                }
                catch (OperationCanceledException)
                {
                    LoadingTask = null;
                    throw;
                }
                catch (Exception e)
                {
                    LogUtility.PrintLog(Tag, e.Message);
                    return null;
                }
            }
        }

        private readonly List<DownloadHandleSprite> _spritesDownloadHandles = new List<DownloadHandleSprite>();

        private void OnEnable()
        {
            _spritesDownloadHandles.Clear();
        }

        private bool SpriteLoadingHandleAlreadyExist(string url, out DownloadHandleSprite handleSprite)
        {
            handleSprite = _spritesDownloadHandles.Find(downloadHandleSprite => downloadHandleSprite != null && downloadHandleSprite.Url == url);
            return handleSprite != null;
        }

        public Task<Sprite> CreateLoadSpriteTask(string url, CancellationToken cancellationToken, bool isLocalFile = false)
        {
            if (SpriteLoadingHandleAlreadyExist(url, out var downloadHandleSprite))
            {
                if (downloadHandleSprite.IsLoading)
                {
                    return downloadHandleSprite.LoadingTask;
                }
                
                if (downloadHandleSprite.IsLoaded)
                {
                    return Task.FromResult(downloadHandleSprite.LoadedSprite);
                }


                //TODO: If sprites are loading fine - remove comment below 
                /*try
                {
                    return  downloadHandleSprite.InvokeDownloading(cancellationToken, TasksFactories.MainThreadTaskFactory);
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
                return downloadHandle.InvokeDownloading(cancellationToken, TasksFactories.MainThreadTaskFactory);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public async Task<Texture2D> CreateLoadTexture2DTask(string url, CancellationToken cancellationToken, bool isLocalFile = false)
        {
            var sprite = await CreateLoadSpriteTask(url, cancellationToken, isLocalFile).ConfigureAwait(false);
            try
            {
                Assert.IsNotNull(sprite);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

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