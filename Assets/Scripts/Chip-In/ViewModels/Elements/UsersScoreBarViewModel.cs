using System.Collections.Generic;
using DataModels.MatchModels;
using Repositories.Local;
using UnityEngine;
using UnityEngine.Assertions;
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

        private void GameRepositoryOnUsersDataUpdated(IReadOnlyList<MatchUserDownloadingData> matchUserDownloadingData)
        {
            UpdateUsersView(matchUserDownloadingData);
        }

        private void UpdateUsersView(IReadOnlyList<MatchUserDownloadingData> dataArray)
        {
            Assert.IsTrue(dataArray.Count == playerScoreViewModels.Length && userAvatarIcons.Length == dataArray.Count);

            for (int i = 0; i < playerScoreViewModels.Length; i++)
            {
                var url = dataArray[i].AvatarUrl;
                playerScoreViewModels[i].SetUserScore(dataArray[i].Score);

                if (string.IsNullOrEmpty(url)) continue;

                downloadedSpritesRepository.TryToLoadSpriteAsync(
                    new DownloadedSpritesRepository.SpriteDownloadingTaskParameters(url,
                        userAvatarIcons[i].SetAvatarSprite));
            }
        }
    }
}