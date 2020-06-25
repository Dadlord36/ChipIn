using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HttpRequests;
using UnityEngine;
using WebOperationUtilities;

namespace Repositories.Local
{
    [CreateAssetMenu(fileName = nameof(DownloadedSpritesRepository), menuName = nameof(Repositories) + "/" + nameof(Local) + "/"
                                                                                + nameof(DownloadedSpritesRepository), order = 0)]
    public sealed class DownloadedSpritesRepository : ScriptableObject
    {
        private const string Tag = nameof(DownloadedSpritesRepository);

        private sealed class DownloadHandleSprite
        {
            public readonly string Url;
            public Sprite LoadedSprite { get; private set; }

            public bool IsLoaded => LoadedSprite;


            public DownloadHandleSprite(string url)
            {
                Url = url;
            }

            public Task<Sprite> InvokeDownloading(CancellationToken cancellationToken, TaskScheduler taskScheduler)
            {
                return ImagesDownloadingUtility.CreateDownloadImageTask(ApiHelper.DefaultClient, taskScheduler, Url, cancellationToken)
                    .ContinueWith(delegate(Task<Texture2D> task)
                    {
                        var texture = task.Result;
                        return LoadedSprite = SpritesUtility.CreateSpriteWithDefaultParameters(texture);
                    }, cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion, taskScheduler);
            }
        }

        private readonly List<DownloadHandleSprite> _spritesDownloadHandles = new List<DownloadHandleSprite>();

        private TaskScheduler MainThreadScheduler { get; set; }

        private void OnEnable()
        {
            _spritesDownloadHandles.Clear();
            MainThreadScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }

        private bool SpriteIsAlreadyLoaded(string url, out DownloadHandleSprite handleSprite)
        {
            handleSprite = _spritesDownloadHandles.Find(downloadHandleSprite => downloadHandleSprite != null &&
                                                                                downloadHandleSprite.Url == url);
            return handleSprite != null;
        }

        public Task CreateLoadSpritesTask(IReadOnlyList<string> parameters, in CancellationToken cancellationToken)
        {
            return Task.WhenAll(CreateLoadSpritesTasks(parameters, cancellationToken));
        }

        public Task<Sprite> CreateLoadSpriteTask(string url, CancellationToken cancellationToken)
        {
            if (SpriteIsAlreadyLoaded(url, out var downloadHandleSprite))
            {
                return downloadHandleSprite.IsLoaded
                    ? Task.FromResult(downloadHandleSprite.LoadedSprite)
                    : downloadHandleSprite.InvokeDownloading(cancellationToken, MainThreadScheduler);
            }

            var downloadHandle = new DownloadHandleSprite(url);
            _spritesDownloadHandles.Add(downloadHandle);
            return downloadHandle.InvokeDownloading(cancellationToken, MainThreadScheduler);
        }

        public Task<Texture2D> CreateLoadTexture2DTask(string url, CancellationToken cancellationToken)
        {
            return CreateLoadSpriteTask(url, cancellationToken).ContinueWith(async delegate(Task<Sprite> task)
            {
                var sprite = await task;
                return sprite.texture;
            }, cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion, MainThreadScheduler).Unwrap();
        }

        private Task[] CreateLoadSpritesTasks(IReadOnlyList<string> parameters, in CancellationToken cancellationToken)
        {
            var tasks = new Task[parameters.Count];
            for (int i = 0; i < parameters.Count; i++)
            {
                tasks[i] = CreateLoadSpriteTask(parameters[i], cancellationToken);
            }

            return tasks;
        }
    }
}