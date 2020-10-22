using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.RequestsModels;
using Factories;
using JetBrains.Annotations;
using Repositories.Local;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects.CardsControllers;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels
{
    [Serializable]
    public class ChatBotParametersData
    {
        public string name;
        public Sprite avatarIcon;
    }

    [Binding]
    public class ChatViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        private class TextPatternAnalyzer
        {
            private Regex _regex;

            public void InitializeForNewPattern(IReadOnlyList<string> patternOptions, RegexOptions regexOptions)
            {
                var patternBuilder = new StringBuilder(patternOptions.Count);
                patternBuilder.Append(patternOptions[0]);
                if (patternOptions.Count > 1)
                {
                    for (int i = 1; i < patternOptions.Count; i++)
                    {
                        patternBuilder.Append($"|{patternOptions[i]}");
                    }
                }

                _regex = new Regex(patternBuilder.ToString(), regexOptions);
            }

            public bool CheckIfAnyMatchesFound(in string lineOfText)
            {
                return _regex.IsMatch(lineOfText);
            }
        }

        [SerializeField] private ChatMessagesListAdapter chatMessagesListAdapter;
        [SerializeField] private UserProfileRemoteRepository userProfileRemoteRepository;
        [SerializeField] private SelectableTextListAdapter selectableTextListAdapter;
        private static IChatBotsRepository ChatBotsRepository => SimpleAutofac.GetInstance<IChatBotsRepository>();
        private ChatMessageItemDataExtendedData[] _questionsMessages;
        private readonly List<UserAnswer> _answers = new List<UserAnswer>();
        private static IRequestHeaders AuthorizationHeaders => SimpleAutofac.GetInstance<IUserAuthorisationDataRepository>();
        private readonly TextPatternAnalyzer _textPatternAnalyzer = new TextPatternAnalyzer();
        private Sprite UserAvatarSprite => userProfileRemoteRepository.UserAvatarSprite;
        private string UserName => userProfileRemoteRepository.Name;
        private IAlertCardController AlertCardController => SimpleAutofac.GetInstance<IAlertCardController>();

        private int CurrentQuestionId { get; set; }


        [Binding]
        public TextListItemData AdditionalText
        {
            get => null;
            set => InputText += $" {value.Text}";
        }

        private string _inputText;

        [Binding]
        public string InputText
        {
            get => _inputText;
            set
            {
                if (_inputText == value) return;
                _inputText = value;
                OnPropertyChanged();
            }
        }

        public ChatViewModel() : base(nameof(ChatViewModel))
        {
        }

        private int _interestId;

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            _interestId = (int) View.FormTransitionBundle.TransitionData;

            try
            {
                var interestSurvey = await SurveyStaticRequestsProcessor.ShowInterestSurvey(
                    out OperationCancellationController.TasksCancellationTokenSource, AuthorizationHeaders, _interestId);

                if (interestSurvey.Success)
                {
                    var survey = interestSurvey.ResponseModelInterface.Survey;
                    FormChatQuestionsExtendedDataBase(survey, ChatBotsRepository.GetRandomBotData());
                    TryToIntroduceANewQuestionToUser();
                }
                else
                {
                }
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
        public void SubmitMessage()
        {
            ProcessUserInput(InputText);
        }

        public void ProcessUserInput(in string userMessage)
        {
            if (string.IsNullOrEmpty(userMessage)) return;

            if (CheckIfAnswerIsAcceptable(userMessage))
            {
                LogUtility.PrintLog(Tag, $"Answer \"{userMessage}\" was accepted");
                OnAnswerAccepted(userMessage);

                var stringBuilder = new StringBuilder(_answers.Count);
                stringBuilder.Append("Answers are: ");
                for (int i = 0; i < _answers.Count; i++)
                {
                    stringBuilder.Append(_answers[i].Text);
                }

                LogUtility.PrintLog(Tag, stringBuilder.ToString());
            }
            else
            {
                LogUtility.PrintLog(Tag, $"Answer \"{userMessage}\" was rejected");
                OnAnswerRejected();
            }

            TryToIntroduceANewQuestionToUser();
            InputText = string.Empty;
        }

        private void TryToIntroduceANewQuestionToUser()
        {
            if (!NewQuestionIsAvailable())
            {
                OnQuestionsFinished();
                return;
            }

            PrepareNewQuestionsAndAnswers();
            AdjustAnsweredQuestionsCount();
        }

        private async void OnQuestionsFinished()
        {
            await SendAnswersToServer().ConfigureAwait(false);
            SwitchToView(nameof(OffersView), new FormsTransitionBundle(_interestId));
        }

        private void OnAnswerRejected()
        {
        }

        private void OnAnswerAccepted(in string answer)
        {
            AddNewAnswer(answer, CurrentQuestionId);
        }

        private void FormChatQuestionsExtendedDataBase(ISurveyModel surveyData, ChatBotParametersData chatBotParametersData)
        {
            var questions = surveyData.Questions;
            _questionsMessages = new ChatMessageItemDataExtendedData[surveyData.Questions.Length];

            for (int i = 0; i < questions.Length; i++)
            {
                _questionsMessages[i] = new ChatMessageItemDataExtendedData(ChatMessageItemData.EMessageType.First, questions[i].TextWithQuestion,
                    chatBotParametersData.avatarIcon, chatBotParametersData.name, DateTime.Now, (int) questions[i].Id, questions[i].Answers);
            }
        }

        #region Utilities

        private void PrepareTextAnalyzerForNewPredefinedAnswersSet(IReadOnlyList<string> predefinedAnswersSet)
        {
            _textPatternAnalyzer.InitializeForNewPattern(predefinedAnswersSet, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        }

        private int _answeredQuestionsCount;

        private void AdjustAnsweredQuestionsCount()
        {
            _answeredQuestionsCount++;
        }

        private void AddNewAnswer(in string answer, int questionId)
        {
            _answers.Add(new UserAnswer(answer, questionId));
            AddMessageToScrollView(new UserChatMessageItemData(ChatMessageItemData.EMessageType.Other, answer, UserAvatarSprite, UserName,
                DateTime.Now, questionId));
        }

        private ChatMessageItemDataExtendedData GetNextQuestion()
        {
            return _questionsMessages[_answeredQuestionsCount];
        }

        private void PrepareNewQuestionsAndAnswers()
        {
            var question = GetNextQuestion();
            CurrentQuestionId = question.QuestionId;
            AddMessageToScrollView(question);
            PrepareTextAnalyzerForNewPredefinedAnswersSet(question.PredefinedAnswers);
            RefillAnswersList(TextListItemData.MakeListFromStrings(question.PredefinedAnswers));
        }

        private void AddMessageToScrollView(ChatMessageItemData messageItemData)
        {
            chatMessagesListAdapter.AddItemAtTheEnd(messageItemData);
        }

        private void RefillAnswersList(IList<TextListItemData> answers)
        {
            selectableTextListAdapter.SetItems(answers);
        }

        private bool NewQuestionIsAvailable()
        {
            return _answeredQuestionsCount < _questionsMessages.Length;
        }

        private bool CheckIfAnswerIsAcceptable(in string userMessage)
        {
            return _textPatternAnalyzer.CheckIfAnyMatchesFound(userMessage);
        }

        private async Task<bool> SendAnswersToServer()
        {
            try
            {
                var response = await SurveyStaticRequestsProcessor.AnswerToSurveyQuestions(
                        out OperationCancellationController.TasksCancellationTokenSource,
                        AuthorizationHeaders, _interestId, new AnswersToSurveyQuestionBodyDataModel {UserAnswers = _answers})
                    .ConfigureAwait(false);

                AlertCardController.ShowAlertWithText(response.Success ? "Answers was sent successfully" : response.Error);
                return response.Success;
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
                throw;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}