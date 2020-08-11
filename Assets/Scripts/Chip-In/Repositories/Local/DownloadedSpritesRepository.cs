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

            public bool IsLoaded => LoadedSprite;


            public DownloadHandleSprite(string url)
            {
                Url = url;
            }

            public async Task<Sprite> InvokeDownloading(CancellationToken cancellationToken, TaskFactory mainThreadTaskFactory, Sprite defaultIcon)
            {
                try
                {
                    return LoadedSprite = await ImagesDownloadingUtility.CreateDownloadImageTask(ApiHelper.DefaultClient, mainThreadTaskFactory, Url,
                        cancellationToken).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    LogUtility.PrintLog(Tag, e.Message);
                    return defaultIcon;
                }
            }
        }

        private readonly List<DownloadHandleSprite> _spritesDownloadHandles = new List<DownloadHandleSprite>();

        public static TaskScheduler MainThreadScheduler => GameManager.MainThreadScheduler;

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

        public async Task<Sprite> CreateLoadSpriteTask(string url, CancellationToken cancellationToken)
        {
            if (SpriteIsAlreadyLoaded(url, out var downloadHandleSprite))
            {
                if (downloadHandleSprite.IsLoaded)
                {
                    return downloadHandleSprite.LoadedSprite;
                }
                try
                {
                    return await downloadHandleSprite.InvokeDownloading(cancellationToken, TasksFactories.MainThreadTaskFactory,
                        IconPlaceholder).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    return IconPlaceholder;
                }
            }

            var downloadHandle = new DownloadHandleSprite(url);
            _spritesDownloadHandles.Add(downloadHandle);
            try
            {
                return await downloadHandle.InvokeDownloading(cancellationToken, TasksFactories.MainThreadTaskFactory,
                    IconPlaceholder).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return iconPlaceholder;
            }
        }

        public Task<Texture2D> CreateLoadTexture2DTask(string url, CancellationToken cancellationToken)
        {
            return CreateLoadSpriteTask(url, cancellationToken).ContinueWith(delegate(Task<Sprite> task)
            {
                if (!task.IsCompleted) return iconPlaceholder.texture;
                var sprite = task.GetAwaiter().GetResult();
                return sprite.texture;
            }, cancellationToken, TaskContinuationOptions.NotOnCanceled, MainThreadScheduler);
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