using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common.UnityEvents;
using Controllers;
using DataModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using JetBrains.Annotations;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects.CardsControllers;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Basic;
using Views;
using Views.Bars.BarItems;
using Views.ViewElements.ScrollViews.Adapters;
using Component = UnityEngine.Component;
using NotifyCollectionChangedAction = System.Collections.Specialized.NotifyCollectionChangedAction;
using NotifyCollectionChangedEventArgs = System.Collections.Specialized.NotifyCollectionChangedEventArgs;

namespace ViewModels
{
    [Binding]
    public sealed class MerchantInterestDetailsViewModel : CorrespondingViewsSwitchingViewModel<MerchantInterestDetailsView>, INotifyPropertyChanged
    {
        [SerializeField] private AlertCardController alertCardController;
        [SerializeField] private MerchantInterestAnswersListAdapter merchantInterestAnswersListAdapter;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;

        private Dictionary<string, IList<AnswerData>> _questionAnswersDictionary;

        public CollectionChangedUnityEvent questionsCollectionChanged;

        private string _currentQuestion;
        private string _interestPageName;


        private readonly AsyncOperationCancellationController _asyncOperationCancellationController =
            new AsyncOperationCancellationController();

        private string _interestPageDescription;
        private Transform _selectedItemTransform;
        private bool _listIsFilled;

        [Binding]
        public bool ListIsFilled
        {
            get => _listIsFilled;
            set
            {
                if (value == _listIsFilled) return;
                _listIsFilled = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string InterestPageName
        {
            get => _interestPageName;
            private set
            {
                if (value == _interestPageName) return;
                _interestPageName = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string InterestPageDescription
        {
            get => _interestPageDescription;
            private set
            {
                if (value == _interestPageDescription) return;
                _interestPageDescription = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public Transform SelectedItemTransform
        {
            get => _selectedItemTransform;
            set => ScrollableItemsSelectorOnNewItemSelected(_selectedItemTransform = value);
        }

        public MerchantInterestDetailsViewModel() : base(nameof(MerchantInterestDetailsViewModel))
        {
        }


        private void QuestionsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            questionsCollectionChanged.Invoke(e);
        }

        [Binding]
        public void CreateOffer_OnButtonClick()
        {
            SwitchToView(nameof(CreateOfferView));
        }

        public readonly struct CommunityAndInterestIds
        {
            public readonly int CommunityId;
            public readonly int InterestId;

            public CommunityAndInterestIds(int communityId, int interestId)
            {
                CommunityId = communityId;
                InterestId = interestId;
            }
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
                await FillScrollsWithDataFromServerAsync().ConfigureAwait(true);
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

        private async Task FillScrollsWithDataFromServerAsync()
        {
            if (RelatedView.FormTransitionBundle.TransitionData == null)
            {
                return;
            }
            var selectedCommunityInterest = (CommunityAndInterestIds) RelatedView.FormTransitionBundle.TransitionData;
            
            
            await SetInterestPageReflectionDataAsync(selectedCommunityInterest).ConfigureAwait(false);
            
            var result = await CommunitiesInterestsStaticProcessor.GetInterestQuestionsAnswers
            (out _asyncOperationCancellationController.TasksCancellationTokenSource, authorisationDataRepository,
                selectedCommunityInterest.InterestId).ConfigureAwait(false);
            
            if (!result.Success)
            {
                alertCardController.ShowAlertWithText(result.Error);
                return;
            }
            else
            {
                var answers = result.ResponseModelInterface.Answers;
                if (answers == null || answers.Count == 0)
                {
                    alertCardController.ShowAlertWithText("Nothing to show");
                    return;
                }
            }
                
            RefillAnswersDictionary(result.ResponseModelInterface);
        }

        private async Task SetInterestPageReflectionDataAsync(CommunityAndInterestIds communityAndInterestIds)
        {
            var response = await CommunitiesInterestsStaticProcessor.GetMerchantInterestPages(out _asyncOperationCancellationController
            .TasksCancellationTokenSource, authorisationDataRepository, communityAndInterestIds.CommunityId, null)
                .ConfigureAwait(true);

            if (!response.Success)
            {
                alertCardController.ShowAlertWithText(response.Error);
            }
            var interestData = response.ResponseModelInterface.Interests.First(model => model.Id == communityAndInterestIds.InterestId);
            
            await TasksFactories.MainThreadTaskFactory.StartNew(delegate
            {
                InterestPageName = interestData.Name;
                InterestPageDescription = interestData.Message;
            }).ConfigureAwait(true);
        }

        private void FillListAdapterWithCorrespondingData(string question)
        {
            if(string.IsNullOrEmpty(question)) return;
            
            merchantInterestAnswersListAdapter.RefillWithData(_questionAnswersDictionary[question]);
        }

        private void RefillAnswersDictionary(IInterestAnswersRequestModel interestAnswersRequestDataModel)
        {
            var answers = interestAnswersRequestDataModel.Answers;
            var count = answers.Count;
            _questionAnswersDictionary = new Dictionary<string, IList<AnswerData>>(count);
            var questions = new List<string>(count);
            foreach (var answer in answers)
            {
                questions.Add(answer.Question);
                _questionAnswersDictionary.Add(ReformatQuestion(answer.Question), answer.Answers);
            }

            CheckIfThereIsQuestions();
            RefillQuestionsList(questions);
        }

        private void CheckIfThereIsQuestions()
        {
            ListIsFilled = _questionAnswersDictionary.Count > 0;
        }
        
        private void RefillQuestionsList(IList<string> questions)
        {
            questionsCollectionChanged.Invoke(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            questionsCollectionChanged.Invoke(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, questions));
        }

        private void ScrollableItemsSelectorOnNewItemSelected(Component selectedTransform)
        {
            SwitchSelectedQuestion(selectedTransform.GetComponent<ITitled>());
        }

        private void SwitchSelectedQuestion(ITitled selectedCategoryTitle)
        {
            _currentQuestion = ReformatQuestion(selectedCategoryTitle.Title);
            FillListAdapterWithCorrespondingData(_currentQuestion);
        }

        private static string ReformatQuestion(in string question)
        {
            return string.Concat(question.ToLower().Where(c => !char.IsWhiteSpace(c)));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}