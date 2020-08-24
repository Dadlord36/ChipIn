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
        
        private readonly AsyncOperationCancellationController _cancellationController = new AsyncOperationCancellationController();


        private uint IndexInDataBaseText;

        public Sprite CardIcon;

        public string AuthorName;

        public int DaysPassed;

        public string CardName;

        public string CardDescription;

        public int CongratulationsNumber;

        public int JoiningInNumber;

        public int HoursLeftNumber;

        public int UsersNumber;

        public float Percentage;

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