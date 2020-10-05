using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Controllers;
using DataModels.MatchModels;
using Repositories.Local;
using Repositories.Local.SingleItem;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;
using ViewModels.UI.Elements.Icons;

namespace ViewModels.Elements
{
    public class UsersScoreBarViewModel : MonoBehaviour
    {
        [SerializeField] private SelectedGameRepository selectedGameRepository;
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
        [SerializeField] private PlayerScoreViewModel[] playerScoreViewModels;
        [SerializeField] private UserAvatarIcon[] userAvatarIcons;

        private AsyncOperationCancellationController _cancellationController = new AsyncOperationCancellationController();

        private void OnEnable()
        {
            SubscribeOnEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeOnEvents()
        {
            selectedGameRepository.UsersDataUpdated += GameRepositoryOnUsersDataUpdated;
        }

        private void UnsubscribeFromEvents()
        {
            selectedGameRepository.UsersDataUpdated -= GameRepositoryOnUsersDataUpdated;
        }

        private async void GameRepositoryOnUsersDataUpdated(IReadOnlyList<MatchUserDownloadingData> matchUserDownloadingData)
        {
            try
            {
                await UpdateUsersView(matchUserDownloadingData);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private async Task UpdateUsersView(IReadOnlyList<MatchUserDownloadingData> dataArray)
        {
            Assert.IsTrue(dataArray.Count == playerScoreViewModels.Length && userAvatarIcons.Length == dataArray.Count);
            _cancellationController.CancelOngoingTask();
            
            for (int i = 0; i < playerScoreViewModels.Length; i++)
            {
                var url = dataArray[i].AvatarUrl;
                playerScoreViewModels[i].SetUserScore(dataArray[i].Score);

                if (string.IsNullOrEmpty(url)) continue;

                try
                {
                    userAvatarIcons[i].AvatarSprite = await downloadedSpritesRepository.CreateLoadSpriteTask(url, _cancellationController
                        .TasksCancellationTokenSource.Token);
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