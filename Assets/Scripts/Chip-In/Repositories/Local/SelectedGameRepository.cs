using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels.Interfaces;
using DataModels.MatchModels;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;
using WebOperationUtilities;
using WebSockets;

namespace Repositories.Local
{
    [CreateAssetMenu(fileName = nameof(SelectedGameRepository),
        menuName = nameof(Repositories) + "/" + nameof(Local) + "/" + nameof(SelectedGameRepository), order = 0)]
    public class SelectedGameRepository : ScriptableObject, IGameWinnerIdentifier
    {
        public event Action<IReadOnlyList<MatchUserData>> UsersDataUpdated;

        private const string Tag = nameof(SelectedGameRepository);
        private int _selectedGameId;
        private MatchUserData[] _matchUsersData;

        public int GameId
        {
            get => _selectedGameId;
            set
            {
                _selectedGameId = value;
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

        public async Task SaveUsersData(IMatchModel matchData)
        {
            InitializeUsersData(matchData.Users);
            WinnerId = matchData.WinnerId;
            AssignAvatarsSpritesToUsersData(await LoadUsersSprites(matchData.Users));
            OnUsersDataUpdated(_matchUsersData);
        }

        private void AssignAvatarsSpritesToUsersData(IReadOnlyList<Sprite> sprites)
        {
            for (int i = 0; i < _matchUsersData.Length; i++)
            {
                _matchUsersData[i].AvatarSprite = sprites[i];
            }
        }

        private void InitializeUsersData(IReadOnlyList<MatchUserLoadedData> usersLoadedData)
        {
            var length = usersLoadedData.Count;
            _matchUsersData = new MatchUserData[length];

            for (int i = 0; i < length; i++)
            {
                _matchUsersData[i] = new MatchUserData(usersLoadedData[i]);
            }
        }

        private async Task<Sprite[]> LoadUsersSprites(IReadOnlyList<MatchUserLoadedData> usersLoadedData)
        {
            var length = usersLoadedData.Count;
            Assert.IsNotNull(usersLoadedData);
            Assert.IsTrue(length > 0);

            _matchUsersData = new MatchUserData[length];

            string[] urlStrings = new string[length];
            for (int i = 0; i < length; i++)
            {
                urlStrings[i] = usersLoadedData[i].AvatarUrl;
                _matchUsersData[i] = new MatchUserData(usersLoadedData[i]);
            }

            return SpritesUtility.CreateArrayOfSpritesWithDefaultParameters(
                await ImagesDownloadingUtility.DownloadImagesArray(urlStrings));
        }

        protected virtual void OnUsersDataUpdated(MatchUserData[] data)
        {
            UsersDataUpdated?.Invoke(data);
        }

        public void UpdateUsersData(IReadOnlyList<MatchUserLoadedData> usersLoadedData)
        {
            for (int i = 0; i < usersLoadedData.Count; i++)
            {
                _matchUsersData[i].Score = usersLoadedData[i].Score;
            }

            OnUsersDataUpdated(_matchUsersData);
        }
    }
}