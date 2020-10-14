using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Behaviours;
using DataModels;
using JetBrains.Annotations;
using Repositories.Local;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects.CardsControllers;
using Tasking;
using UnityEngine;
using UnityEngine.Assertions;
using UnityWeld.Binding;
using Utilities;
using ViewModels.UI.Elements.Icons;
using Views.ViewElements.Interfaces;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class InterestCardViewModel : AsyncOperationsMonoBehaviour, INotifyPropertyChanged, IFillingView<UserInterestPageDataModel>
    {
        private const string Tag = nameof(InterestCardViewModel);

        [SerializeField] private UserAvatarIcon avatarIcon;
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private AlertCardController alertCardController;


        #region Backing fields

        private int _daysPassed;
        private string _cardName;
        private string _cardDescription;
        private int _congratulationsNumber;
        private int _joiningInNumber;
        private int _hoursLeftNumber;
        private int _usersNumber;
        private float _percentage;
        private Sprite _cardIcon;
        private string _authorName;
        private int _totalFound;
        private string _createdAt;
        private bool _supporting;

        #endregion

        #region Bindable properties

        [Binding]
        public int TotalFound
        {
            get => _totalFound;
            set
            {
                if (value == _totalFound) return;
                _totalFound = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public Sprite CardIcon
        {
            get => _cardIcon;
            private set
            {
                _cardIcon = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string AuthorName
        {
            get => _authorName;
            set
            {
                if (value == _authorName) return;
                _authorName = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public int DaysPassed
        {
            get => _daysPassed;
            set
            {
                if (value == _daysPassed) return;
                _daysPassed = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string CardName
        {
            get => _cardName;
            set
            {
                if (value == _cardName) return;
                _cardName = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string CardDescription
        {
            get => _cardDescription;
            set
            {
                if (value == _cardDescription) return;
                _cardDescription = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public int CongratulationsNumber
        {
            get => _congratulationsNumber;
            set
            {
                if (value == _congratulationsNumber) return;
                _congratulationsNumber = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public int JoiningInNumber
        {
            get => _joiningInNumber;
            set
            {
                if (value == _joiningInNumber) return;
                _joiningInNumber = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public int HoursLeftNumber
        {
            get => _hoursLeftNumber;
            set
            {
                if (value == _hoursLeftNumber) return;
                _hoursLeftNumber = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public int UsersNumber
        {
            get => _usersNumber;
            set
            {
                if (value == _usersNumber) return;
                _usersNumber = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public float Percentage
        {
            get => _percentage;
            set
            {
                if (value.Equals(_percentage)) return;
                _percentage = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string CreatedAt
        {
            get => _createdAt;
            set
            {
                if (value == _createdAt) return;
                _createdAt = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public bool Supporting
        {
            get => _supporting;
            set
            {
                if (value == _supporting) return;
                _supporting = value;
                OnPropertyChanged();
            }
        }

        #endregion

        [Binding]
        public async void LikeButton_OnClick()
        {
            try
            {
                var response = await CommunitiesInterestsStaticProcessor.SupportInterest(
                        out AsyncOperationCancellationController.TasksCancellationTokenSource, authorisationDataRepository, (int) InterestId)
                    .ConfigureAwait(false);

                alertCardController.ShowAlertWithText(response.Success ? "Successfully supported this interest" : response.Error);
                if (response.Success)
                    Supporting = true;
            }
            catch (AssertionException e)
            {
                LogUtility.PrintLogException(e);
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

        [Binding]
        public async void JoinButton_OnClick()
        {
            try
            {
                Assert.IsTrue(InterestId.HasValue);
                var response = await CommunitiesInterestsStaticProcessor.JoinToInterest(
                        out AsyncOperationCancellationController.TasksCancellationTokenSource, authorisationDataRepository, (int) InterestId)
                    .ConfigureAwait(false);

                alertCardController.ShowAlertWithText(response.Success ? "Successfully joining this interest" : response.Error);
            }
            catch (AssertionException e)
            {
                LogUtility.PrintLogException(e);
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

        public async void FundTheInterest(int tokensAmount)
        {
            if (tokensAmount <= 0) return;

            try
            {
                var response = await CommunitiesInterestsStaticProcessor.FundInterest(
                    out AsyncOperationCancellationController.TasksCancellationTokenSource, authorisationDataRepository, (int) InterestId, tokensAmount)
                    .ConfigureAwait(false);

                alertCardController.ShowAlertWithText(response.Success ? "Successfully fund this interest" : response.Error);
                if (response.Success)
                {
                    TotalFound += tokensAmount;
                }
            }
            catch (AssertionException e)
            {
                LogUtility.PrintLogException(e);
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

        private string GetCorrespondingEndText(int days)
        {
            return days == 1 ? "Day" : "Days";
        }

        public async Task FillView(UserInterestPageDataModel pageDataModel, uint index)
        {
            AsyncOperationCancellationController.CancelOngoingTask();

            DaysPassed = (DateTime.Now - pageDataModel.StartedAt.ToLocalTime()).Days;
            InterestId = pageDataModel.Id;
            // AuthorName = pageDataModel.;
            CardName = pageDataModel.Name;
            CardDescription = pageDataModel.Message;
            CongratulationsNumber = (int) pageDataModel.SupportedCount;
            JoiningInNumber = (int) pageDataModel.JoinedCount;
            HoursLeftNumber = (pageDataModel.EndsAtTime - DateTime.UtcNow).Hours;
            UsersNumber = (int) pageDataModel.UsersCount;
            TotalFound = pageDataModel.TotalFound;
            CreatedAt = pageDataModel.CreatedAt;
            Supporting = pageDataModel.Support;
            

            try
            {
                CardIcon = await downloadedSpritesRepository.CreateLoadSpriteTask(pageDataModel.PosterUri,
                    AsyncOperationCancellationController.CancellationToken)
                    .ConfigureAwait(false);
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

        public int? InterestId { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)));
        }
    }
}