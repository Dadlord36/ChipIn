using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataModels;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.RequestsModels;
using DataModels.SimpleTypes;
using Factories;
using GlobalVariables;
using JetBrains.Annotations;
using RequestsStaticProcessors;
using RestSharp;
using ScriptableObjects.CardsControllers;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels
{
    [Binding]
    public sealed class StartInterestViewModel : ViewsSwitchingViewModel, IInterestCreationModel, INotifyPropertyChanged
    {
        [SerializeField] private UsersAvatarsArrayListAdapter usersAvatarsArrayListAdapter;
        private readonly List<UserProfileBaseData> _members = new List<UserProfileBaseData>();
        private int _selectedCommunityIndex;


        private InterestCreationDataModel _interestCreationModelImplementation = new InterestCreationDataModel();
        private static IRequestHeaders RequestAuthorizationHeaders => SimpleAutofac.GetInstance<IUserProfileRequestHeadersProvider>();


        public IList<UserInterestAttribute> UserAttributes
        {
            get => _interestCreationModelImplementation.UserAttributes;
            set => _interestCreationModelImplementation.UserAttributes = value;
        }

        [Binding]
        public bool IsPublic
        {
            get => _interestCreationModelImplementation.IsPublic;
            set
            {
                if (IsPublic == value) return;
                _interestCreationModelImplementation.IsPublic = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string MemberMessage
        {
            get => _interestCreationModelImplementation.MemberMessage;
            set
            {
                if (MemberMessage == value) return;
                _interestCreationModelImplementation.MemberMessage = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string MerchantMessage
        {
            get => _interestCreationModelImplementation.MerchantMessage;
            set
            {
                if (MerchantMessage == value) return;
                _interestCreationModelImplementation.MerchantMessage = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string PosterFilePathString
        {
            get => _interestCreationModelImplementation.PosterFilePath.Path;
            set
            {
                if (PosterFilePathString == value) return;
                _interestCreationModelImplementation.PosterFilePath = new FilePath(value);
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Name
        {
            get => _interestCreationModelImplementation.Name;
            set
            {
                if (Name == value) return;
                _interestCreationModelImplementation.Name = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string SegmentName
        {
            get => _interestCreationModelImplementation.SegmentName;
            set
            {
                if (SegmentName == value) return;
                _interestCreationModelImplementation.SegmentName = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public DateTime StartedAt
        {
            get => _interestCreationModelImplementation.StartedAt;
            set
            {
                if (StartedAt == value) return;
                _interestCreationModelImplementation.StartedAt = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public DateTime EndsAtTime
        {
            get => _interestCreationModelImplementation.EndsAtTime;
            set
            {
                if (EndsAtTime == value) return;
                _interestCreationModelImplementation.EndsAtTime = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public FilePath PosterFilePath
        {
            get => _interestCreationModelImplementation.PosterFilePath;
            set
            {
                if (PosterFilePath == value) return;
                _interestCreationModelImplementation.PosterFilePath = value;
                OnPropertyChanged();
            }
        }

        public StartInterestViewModel() : base(nameof(StartInterestViewModel))
        {
        }

        private void Start()
        {
            SelectNewProductCategory(0);
            Clear();
        }

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            _selectedCommunityIndex = (int) View.FormTransitionBundle.TransitionData;
        }

        private void Clear()
        {
            _interestCreationModelImplementation = new InterestCreationDataModel {UserAttributes = new List<UserInterestAttribute>()};
        }

        [Binding]
        public void AddUserButton_OnClick()
        {
            SwitchToView(nameof(SearchForUsersView), new FormsTransitionBundle(new Action<UserProfileBaseData>(AddUser)));
        }

        [Binding]
        public void SelectProductCategoryButton_OnClick()
        {
            SwitchToView(nameof(ProductCategorySelectionView), new FormsTransitionBundle(new Action<int>(SelectNewProductCategory)));
        }

        private void SelectNewProductCategory(int itemCategory)
        {
            SegmentName = MainNames.OfferSegments.GetSegmentName(itemCategory);
        }

        private void AddUser(UserProfileBaseData selectedUserData)
        {
            _members.Add(selectedUserData);
            usersAvatarsArrayListAdapter.SetItems(_members);
            UserAttributes.Add(new UserInterestAttribute {UserId = (int) selectedUserData.Id});
        }

        [Binding]
        public async void CreateInterest_OnButtonClick()
        {
            if (!ValidationHelper.CheckIfAllFieldsAreValid(this)) return;

            try
            {
                IsAwaitingProcess = true;
                var response = await CreateInterestAsync().ConfigureAwait(false);

                SimpleAutofac.GetInstance<IAlertCardController>()
                    .ShowAlertWithText(response.IsSuccessful ? "Interest was created successfully." : "Interest has failed to create.");
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
            finally
            {
                IsAwaitingProcess = false;
            }
        }

        private Task<IRestResponse> CreateInterestAsync()
        {
            return CommunitiesInterestsStaticProcessor.CreateAnInterestAsync(OperationCancellationController.CancellationToken,
                RequestAuthorizationHeaders, _selectedCommunityIndex, _interestCreationModelImplementation);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}