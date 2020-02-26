using System;
using System.Collections.Generic;
using System.IO;
using Controllers;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using Encryption;
using UnityEngine;
using Utilities;

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

        private const string EncryptionKey = "crowdcrowdcrowdcrowdcrowdcrowdSD";
        private const string SaveFileName = "Player.dat";

        private static string FileName => Path.Combine(Application.persistentDataPath, SaveFileName);

        public void Set(IAuthorisationModel source)
        {
            _authorisationModel.Set(source);
        }

        public void Set(IUserProfileRequestHeadersProvider source)
        {
            _authorisationModel.Set(source);
        }

        #region IUserProfileRequestHeadersProvider implementation

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

        public int Expiry
        {
            get => _authorisationModel.Expiry;
            set => _authorisationModel.Expiry = value;
        }

        #endregion


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
            ClearSavedData();
        }

        public void TrySaveDataLocally()
        {
            try
            {
                var authenticationDataAsString = JsonConverterUtility.ConvertModelToJson(_authorisationModel);
                var encryptedStringAsBytes = RijndaelEncryptor.Encrypt(authenticationDataAsString, EncryptionKey);
                
                ClearSavedData();
                File.WriteAllBytes(FileName, encryptedStringAsBytes);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }
        }


        public void TryLoadLocalData()
        {
            try
            {
                var authenticationDatAsBytes = File.ReadAllBytes(FileName);
                var decryptedStringAsString = RijndaelEncryptor.Decrypt(authenticationDatAsBytes, EncryptionKey);

                _authorisationModel = JsonConverterUtility.ConvertJsonString<UserProfileRequestHeadersProvider>(decryptedStringAsString);
                LogUtility.PrintLog(nameof(UserAuthorisationDataRepository), "Authentication data was loaded");
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }
        }

        private void ClearSavedData()
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
        }

        public bool CheckIfUserWasLoggedInPreviously()
        {
            return File.Exists(FileName);
        }
    }
}