using System.Collections.Generic;
using Controllers;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using UnityEngine;

namespace Repositories.Remote
{
    [CreateAssetMenu(fileName = nameof(UserAuthorisationDataRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(UserAuthorisationDataRepository),
        order = 0)]
    public sealed class UserAuthorisationDataRepository : ScriptableObject, IUserProfileRequestHeadersProvider,
        IClearable
    {
        private IUserProfileRequestHeadersProvider _authorisationModel =
            new UserProfileRequestHeadersProvider();

        public string AccessToken
        {
            get => _authorisationModel.AccessToken;
            set => _authorisationModel.AccessToken = value;
        }

        public string Client
        {
            get => _authorisationModel.Client;
            set => _authorisationModel.Client = value;
        }

        public string TokenType
        {
            get => _authorisationModel.TokenType;
            set => _authorisationModel.TokenType = value;
        }

        public string Uid
        {
            get => _authorisationModel.Uid;
            set => _authorisationModel.Uid = value;
        }

        public void Set(IAuthorisationModel source)
        {
            _authorisationModel.Set(source);
        }

        public int Expiry
        {
            get => _authorisationModel.Expiry;
            set => _authorisationModel.Expiry = value;
        }

        public void Set(IUserProfileRequestHeadersProvider source)
        {
            _authorisationModel.Set(source);
        }

        public List<KeyValuePair<string, string>> GetRequestHeaders()
        {
            return _authorisationModel.GetRequestHeaders();
        }

        public string GetRequestHeadersAsString()
        {
            return _authorisationModel.GetRequestHeadersAsString();
        }

        public void Clear()
        {
            _authorisationModel = new UserProfileRequestHeadersProvider();
        }
    }
}