﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Behaviours.Games;
using DataModels;
using DataModels.Interfaces;
using DataModels.MatchModels;
using Repositories.Local.SingleItem;
using UnityEngine;
using Utilities;

namespace Repositories.Local
{
    [CreateAssetMenu(fileName = nameof(SelectedGameRepository),
        menuName = nameof(Repositories) + "/" + nameof(Local) + "/" + nameof(SelectedGameRepository), order = 0)]
    public sealed class SelectedGameRepository : SingleItemLocalRepository, IGameWinnerIdentifier
    {
        [SerializeField] private UserGamesRemoteRepository userGamesRemoteRepository;
        public event Action<IReadOnlyList<MatchUserDownloadingData>> UsersDataUpdated;
        private const string Tag = nameof(SelectedGameRepository);
        private int _selectedGameId;
        private MatchUserDownloadingData[] _matchUsersData;


        public GameDataModel SelectedGameData =>
            userGamesRemoteRepository.ItemsData.First(gameData => gameData.Id == GameId);

        public bool GameHasStarted => DateTime.UtcNow >= SelectedGameData.StartedAt;
        public TimeSpan TimeTillGameStarts => SelectedGameData.StartedAt - DateTime.UtcNow;

        public int GameId
        {
            get => _selectedGameId;
            set
            {
                _selectedGameId = value;
                GameWasSelected = true;
                LogUtility.PrintLog(Tag, $"Selected game ID was changed to: {value.ToString()}");
            }
        }

        public int? WinnerId { get; set; }

        public MatchUserDownloadingData GetWinnerUserData()
        {
            return new List<MatchUserDownloadingData>(_matchUsersData).Find(data => data.UserId == WinnerId);
        }

        public IReadOnlyList<string> UsersAvatarImagesSprites
        {
            get
            {
                var sprites = new List<string>(_matchUsersData.Length);

                for (int i = 0; i < _matchUsersData.Length; i++)
                {
                    if (_matchUsersData[i].UserId != WinnerId)
                        sprites.Add(_matchUsersData[i].AvatarUrl);
                }

                return sprites;
            }
        }

        public bool GameWasSelected { get; private set; }

        public Task SaveGameSateData(SlotsGameBehaviour.SlotGameRoundData roundData)
        {
            InitializeUsersData(roundData.UsersData);
            WinnerId = roundData.WinnerId;
            OnUsersDataUpdated(_matchUsersData);
            return Task.CompletedTask;
        }


        private void InitializeUsersData(IReadOnlyList<MatchUserDownloadingData> usersLoadedData)
        {
            _matchUsersData = usersLoadedData as MatchUserDownloadingData[];
        }

        /*private async Task<Sprite[]> LoadUsersSprites(IReadOnlyList<MatchUserDownloadingData> usersLoadedData)
        {
            var length = usersLoadedData.Count;
            Assert.IsNotNull(usersLoadedData);
            Assert.IsTrue(length > 0);

            _matchUsersData = new MatchUserData[length];

            var urlStrings = new List<string>(length);
            var emptyIndexes = new bool [length];

            for (int i = 0; i < length; i++)
            {
                _matchUsersData[i] = new MatchUserData(usersLoadedData[i]);

                if (string.IsNullOrEmpty(usersLoadedData[i].AvatarUrl))
                {
                    emptyIndexes[i] = true;
                    continue;
                }

                urlStrings.Add(usersLoadedData[i].AvatarUrl);
            }

            var sprites = SpritesUtility.CreateArrayOfSpritesWithDefaultParameters(await ImagesDownloadingUtility.TryDownloadImagesArray(urlStrings.ToArray()));

            var spritesToReturn = new Sprite[length];
            int spriteIndex = 0;
            for (int i = 0; i < spritesToReturn.Length; i++)
            {
                if (emptyIndexes[i])
                {
                    spritesToReturn[i] = null;
                }
                else
                {
                    spritesToReturn[i] = sprites[spriteIndex];
                    spriteIndex++;
                }
            }

            return spritesToReturn;
        }
    */

        private void OnUsersDataUpdated(IReadOnlyList<MatchUserDownloadingData> data)
        {
            UsersDataUpdated?.Invoke(data);
        }

        public void UpdateUsersData(IReadOnlyList<MatchUserDownloadingData> usersLoadedData)
        {
            for (int i = 0; i < usersLoadedData.Count; i++)
            {
                _matchUsersData[i].Score = usersLoadedData[i].Score;
            }

            OnUsersDataUpdated(_matchUsersData);
        }
    }
}