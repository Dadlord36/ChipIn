using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Controllers;
using GlobalVariables;
using JetBrains.Annotations;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects.CardsControllers;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using WebOperationUtilities;

namespace ViewModels
{
    [Binding]
    public sealed class EditProfileViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        [SerializeField] private UserProfileRemoteRepository userProfileRemoteRepository;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private AlertCardController alertCardController;

        private string _firstName;
        private string _lastName;
        private string _email;
        private string _birthdate;
        private string _newAvatarImagePath;

        private readonly Dictionary<string, string> _changedPropertiesCollection = new Dictionary<string, string>();
        private DateTime _birthDateTime;


        /// <summary>
        /// Should be named same as delegated property
        /// </summary>
        [Binding]
        public Sprite UserAvatarSprite
        {
            get => userProfileRemoteRepository.UserAvatarSprite;
            set => userProfileRemoteRepository.UserAvatarSprite = value;
        }


        [Binding]
        public string NewAvatarImagePath
        {
            get => _newAvatarImagePath;
            set
            {
                if (value == _newAvatarImagePath) return;
                _newAvatarImagePath = value;
                OnPropertyChanged();
                try
                {
                    UpdateAvatarIconAsync().Start();
                }
                catch (Exception e)
                {
                    LogUtility.PrintLogException(e);
                }
            }
        }

        [Binding]
        public DateTime BirthDateTime
        {
            get => _birthDateTime;
            set
            {
                _birthDateTime = value;
                Birthdate = value.ToShortDateString();
            }
        }

        [Binding]
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value == _firstName) return;
                _firstName = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value == _lastName) return;
                _lastName = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Email
        {
            get => _email;
            set
            {
                if (value == _email) return;
                _email = value;
                OnPropertyChanged();
                AddChangedField(value, MainNames.ModelsPropertiesNames.Email);
            }
        }

        [Binding]
        public string Birthdate
        {
            get => _birthdate;
            set
            {
                if (value == _birthdate) return;
                _birthdate = value;
                OnPropertyChanged();
                AddChangedField(value, MainNames.ModelsPropertiesNames.Birthdate);
            }
        }

        [Binding] public string CurrentBirthdate => userProfileRemoteRepository.Birthday;

        public EditProfileViewModel() : base(nameof(EditProfileViewModel))
        {
        }

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            userProfileRemoteRepository.PropertyChanged += PropertyChanged;
        }

        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
            userProfileRemoteRepository.PropertyChanged -= PropertyChanged;
        }

        [Binding]
        public async void ConfirmButton_OnClick()
        {
            try
            {
                await UpdateUseProfileAsync();
                SwitchToPreviousView();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        [Binding]
        public void CancelButton_OnClick()
        {
            SwitchToPreviousView();
        }

        private async Task UpdateUseProfileAsync()
        {
            try
            {
                IsAwaitingProcess = true;
                if (!string.IsNullOrEmpty(_firstName) || !string.IsNullOrEmpty(_lastName))
                    AddChangedField($"{_firstName} {_lastName}", MainNames.ModelsPropertiesNames.Name);

                var result = await ProfileDataStaticRequestsProcessor.UpdateUserProfileData(OperationCancellationController
                        .CancellationToken, authorisationDataRepository, _changedPropertiesCollection, NewAvatarImagePath)
                    .ConfigureAwait(true);


                if (result.IsSuccessful)
                {
                    await userProfileRemoteRepository.LoadDataFromServer().ConfigureAwait(true);
                    alertCardController.ShowAlertWithText("User profile was successfully updated");
                    _changedPropertiesCollection.Clear();
                    ClearFields();
                }
                else
                {
                    alertCardController.ShowAlertWithText("User profile was failed to update");
                }
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }
            finally
            {
                IsAwaitingProcess = false;
            }
        }

        private void ClearFields()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
        }

        private async Task UpdateAvatarIconAsync()
        {
            try
            {
                var texture = NativeGallery.LoadImageAtPath(NewAvatarImagePath);
                await TasksFactories.MainThreadTaskFactory.StartNew(delegate { UserAvatarSprite = SpritesUtility.CreateSpriteWithDefaultParameters(texture); });
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private void AddChangedField(in string value, string propertyName)
        {
            if (string.IsNullOrEmpty(value)) return;
            _changedPropertiesCollection.AddOrUpdate(value, propertyName);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}