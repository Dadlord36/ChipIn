using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Controllers;
using DataModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using JetBrains.Annotations;
using Repositories.Local;
using Repositories.Local.SingleItem;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;
using Views.Bars.BarItems;
using Views.ViewElements.Lists.ScrollableList;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels
{
    [Binding]
    public sealed class MerchantInterestDetailsViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
        [SerializeField] private MerchantInterestAnswersListAdapter merchantInterestAnswersListAdapter;

        [SerializeField] private SelectedMerchantInterestPageRepository selectedMerchantInterestPageRepository;
        [SerializeField] private ScrollableItemsSelector scrollableItemsSelector;

        //TODO: remove this field once API request will be implemented
        [SerializeField] private string jsonString;
        private Dictionary<string, IList<InterestQuestionAnswer>> _questionAnswersDictionary;

        private string _currentQuestion;
        private string _interestPageName;

        private readonly AsyncOperationCancellationController _asyncOperationCancellationController =
            new AsyncOperationCancellationController();

        private string _interestPageDescription;

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

        public MerchantInterestDetailsViewModel() : base(nameof(MerchantInterestDetailsViewModel))
        {
        }

        [Binding]
        public void CreateOffer_OnButtonClick()
        {
            SwitchToView(nameof(CreateOfferView));
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();

            RefillAnswersDictionary(JsonConverterUtility
                .ConvertJsonString<InterestQuestionAnswerRequestResponse>(jsonString));
            FillListAdapterWithCorrespondingData(_questionAnswersDictionary.Keys.First());
            scrollableItemsSelector.NewItemSelected += ScrollableItemsSelectorOnNewItemSelected;
            try
            {
                await SetInterestPageReflectionData();
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

        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
            scrollableItemsSelector.NewItemSelected -= ScrollableItemsSelectorOnNewItemSelected;
        }

        private Task SetInterestPageReflectionData()
        {
            return selectedMerchantInterestPageRepository.CreateGetSelectedInterestPageDataTask().ContinueWith(
                delegate(Task<MerchantInterestPageDataModel> task)
                {
                    InterestPageName = task.Result.Name;
                    InterestPageDescription = task.Result.Message;
                },
                scheduler: downloadedSpritesRepository.MainThreadScheduler,
                continuationOptions: TaskContinuationOptions.OnlyOnRanToCompletion,
                cancellationToken: _asyncOperationCancellationController.CancellationToken);
        }

        private void FillListAdapterWithCorrespondingData(string question)
        {
            merchantInterestAnswersListAdapter.RefillWithData(_questionAnswersDictionary[question]);
        }

        private void RefillAnswersDictionary(IInterestQuestionAnswerRequestResponseModel questionAnswerRequestResponse)
        {
            var questions = questionAnswerRequestResponse.Questions;
            var count = questions.Length;
            _questionAnswersDictionary = new Dictionary<string, IList<InterestQuestionAnswer>>(count);

            foreach (var question in questions)
            {
                _questionAnswersDictionary.Add(ReformatQuestion(question.Question), question.Answers);
            }
        }

        private void ScrollableItemsSelectorOnNewItemSelected(Transform selectedTransform)
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
            return string.Concat(question.ToLower().Where(c => !char.IsWhiteSpace(c)));;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}