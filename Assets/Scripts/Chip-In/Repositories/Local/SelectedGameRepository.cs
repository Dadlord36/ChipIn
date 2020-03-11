using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Behaviours.Games;
using DataModels;
using DataModels.Interfaces;
using DataModels.MatchModels;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;
using WebOperationUtilities;

namespace Repositories.Local
{
    [CreateAssetMenu(fileName = nameof(SelectedGameRepository),
        menuName = nameof(Repositories) + "/" + nameof(Local) + "/" + nameof(SelectedGameRepository), order = 0)]
    public sealed class SelectedGameRepository : ScriptableObject, IGameWinnerIdentifier
    {
        [SerializeField] private UserGamesRemoteRepository userGamesRemoteRepository;
        public event Action<IReadOnlyList<MatchUserData>> UsersDataUpdated;
        private const string Tag = nameof(SelectedGameRepository);
        private int _selectedGameId;
        private MatchUserData[] _matchUsersData;
        
        
        public GameDataModel SelectedGameData => userGamesRemoteRepository.ItemsData.First(gameData => gameData.Id == GameId);
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

        public MatchUserData GetWinnerUserData()
        {
            return new List<MatchUserData>(_matchUsersData).Find(data => data.UserId == WinnerId);
        }

        public IReadOnlyList<Sprite> UsersAvatarImagesSprites
        {
            get
            {
                List<Sprite> sprites = new List<Sprite>(_matchUsersData.Length);

                for (int i = 0; i < _matchUsersData.Length; i++)
                {
                    if (_matchUsersData[i].UserId != WinnerId)
                        sprites.Add(_matchUsersData[i].AvatarSprite);
                }

                return sprites;
            }
        }

        public bool GameWasSelected { get; private set; }

        public async Task SaveGameSateData(SlotsGameBehaviour.SlotGameRoundData roundData)
        {
            InitializeUsersData(roundData.UsersData);
            WinnerId = roundData.WinnerId;
            AssignAvatarsSpritesToUsersData(await LoadUsersSprites(roundData.UsersData));
            OnUsersDataUpdated(_matchUsersData);
        }

        private void AssignAvatarsSpritesToUsersData(IReadOnlyList<Sprite> sprites)
        {
            for (int i = 0; i < _matchUsersData.Length; i++)
            {
                _matchUsersData[i].AvatarSprite = sprites[i];
            }
        }

        private void InitializeUsersData(IReadOnlyList<MatchUserDownloadingData> usersLoadedData)
        {
            var length = usersLoadedData.Count;
            _matchUsersData = new MatchUserData[length];

            for (int i = 0; i < length; i++)
            {
                _matchUsersData[i] = new MatchUserData(usersLoadedData[i]);
            }
        }

        private async Task<Sprite[]> LoadUsersSprites(IReadOnlyList<MatchUserDownloadingData> usersLoadedData)
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

        private void OnUsersDataUpdated(MatchUserData[] data)
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