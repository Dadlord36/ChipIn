using System;
using System.Threading.Tasks;
using Common;
using DataModels;
using RequestsStaticProcessors;
using UnityEngine;
using Utilities;

namespace Repositories.Remote
{
    [CreateAssetMenu(fileName = nameof(CommunitiesDataRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(CommunitiesDataRepository), order = 0)]
    public sealed class CommunitiesDataRepository : BaseNotPaginatedListRepository<CommunityBasicDataModel>
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;

        public override async Task LoadDataFromServer()
        {
            try
            {
                var result = await CommunitiesStaticRequestsProcessor.GetCommunitiesList(authorisationDataRepository);
                var responseInterface = result.ResponseModelInterface;
                ItemsLiveData = new LiveData<CommunityBasicDataModel>(responseInterface.Communities);
                ConfirmDataLoading();
            }

            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }
        }

        protected override void ConfirmDataLoading()
        {
            base.ConfirmDataLoading();
            LogUtility.PrintLog("Repositories", "Community repository data was loaded from server");
        }

        public override Task SaveDataToServer()
        {
            throw new NotImplementedException();
        }
    }
}