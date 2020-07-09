using System;
using System.Threading.Tasks;
using Controllers;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using DataModels;
using Repositories.Local;
using TMPro;
using UnityEngine;
using Utilities;
using ViewModels.UI.Elements;
using ViewModels.UI.Elements.Icons;

namespace Views.Cards
{
    public class InterestCardView : BaseView, IFillingView<UserInterestPageDataModel>
    {
        [SerializeField] private UserAvatarIcon avatarIcon;
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
        [SerializeField] private TMP_Text cardNameTextField;
        [SerializeField] private TMP_Text cardDescriptionTextField;
        [SerializeField] private TMP_Text authorNameTextField;
        [SerializeField] private TMP_Text daysPassedTextField;

        [SerializeField] private InterestCardElementView congratulationsNumber;
        [SerializeField] private InterestCardElementView joiningInNumber;
        [SerializeField] private InterestCardElementView hoursLeftNumber;
        [SerializeField] private InterestCardElementView usersNumber;

        //Temporary 
        [SerializeField] private TMP_Text indexInDataBaseTextField;
        
        [SerializeField] private PercentageView percentageView;

        private readonly AsyncOperationCancellationController _cancellationController = new AsyncOperationCancellationController();


        private uint IndexInDataBaseText
        {
            get => uint.Parse(indexInDataBaseTextField.text);
            set => indexInDataBaseTextField.text = value.ToString();
        }
        
        public Sprite CardIcon
        {
            get => avatarIcon.AvatarSprite;
            set => avatarIcon.AvatarSprite = value;
        }

        public string AuthorName
        {
            get => authorNameTextField.text;
            set => authorNameTextField.text = value;
        }

        public int DaysPassed
        {
            set => daysPassedTextField.text = $"{value.ToString()} {GetCorrespondingEndText(value)}";
        }

        public string CardName
        {
            get => cardNameTextField.text;
            set => cardNameTextField.text = value;
        }

        public string CardDescription
        {
            get => cardDescriptionTextField.text;
            set => cardDescriptionTextField.text = value;
        }

        public int CongratulationsNumber
        {
            get => congratulationsNumber.Number;
            set => congratulationsNumber.Number = value;
        }

        public int JoiningInNumber
        {
            get => joiningInNumber.Number;
            set => joiningInNumber.Number = value;
        }

        public int HoursLeftNumber
        {
            get => hoursLeftNumber.Number;
            set => hoursLeftNumber.Number = value;
        }

        public int UsersNumber
        {
            get => usersNumber.Number;
            set => usersNumber.Number = value;
        }

        public float Percentage
        {
            get => percentageView.Percentage;
            set => percentageView.Percentage = value;
        }

        public InterestCardView() : base(nameof(InterestCardView))
        {
        }

        private string GetCorrespondingEndText(int days)
        {
            return days == 1 ? "Day" : "Days";
        }

        public async Task FillView(UserInterestPageDataModel pageDataModel, uint index)
        {
            _cancellationController.CancelOngoingTask();
           
            //TODO: Implement Support number
            
            //TODO: remove temporary code   
            IndexInDataBaseText = index;
            
            //TODO: recalculate from UTC to LocalTime;
            
            // AuthorName = dataModel.;
            DaysPassed = (DateTime.UtcNow - pageDataModel.StartedAt).Days;
            CardName = pageDataModel.Name;
            CardDescription = pageDataModel.Message;
            // CongratulationsNumber = dataModel.;
            JoiningInNumber = (int) pageDataModel.JoinedCount;
            HoursLeftNumber = (pageDataModel.EndsAtTime - DateTime.UtcNow).Hours;
            UsersNumber = (int) pageDataModel.UsersCount;
            // Percentage = dataModel.;
            try
            {
                CardIcon = await downloadedSpritesRepository.CreateLoadSpriteTask(pageDataModel.PosterUri,
                    _cancellationController.CancellationToken);
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
    }
}