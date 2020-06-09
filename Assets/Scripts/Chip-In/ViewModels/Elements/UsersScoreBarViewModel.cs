using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels.MatchModels;
using Repositories.Local;
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

            for (int i = 0; i < playerScoreViewModels.Length; i++)
            {
                var url = dataArray[i].AvatarUrl;
                playerScoreViewModels[i].SetUserScore(dataArray[i].Score);

                if (string.IsNullOrEmpty(url)) continue;

                try
                {
                    await downloadedSpritesRepository.TryToLoadSpriteAsync(
                        new DownloadedSpritesRepository.SpriteDownloadingTaskParameters(url,
                            userAvatarIcons[i].SetAvatarSprite));
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