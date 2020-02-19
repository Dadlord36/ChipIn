using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels.ResponsesModels;
using RequestsStaticProcessors;
using UnityEngine;
using Views;
using WebOperationUtilities;

namespace Repositories.Remote
{
    [CreateAssetMenu(fileName = nameof(CommunityInterestRemoteRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(CommunityInterestRemoteRepository),
        order = 0)]
    public sealed class CommunityInterestRemoteRepository : BaseItemsListRepository<
        CommunityInterestGridItemView.CommunityInterestGridItemData>
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;

        public override async Task LoadDataFromServer()
        {
            try
            {
                var result =
                    await CommunityInterestsStaticRequestProcessor.TryGetCommunityInterestsLabelsData(
                        authorisationDataRepository);

                var itemsData = result.ResponseModelInterface.Communities;
                int itemsCount = itemsData.Length;
                var gridItemsData = new CommunityInterestGridItemView.CommunityInterestGridItemData[itemsCount];

                var tasks = new List<Task>(itemsCount);

                for (int i = 0; i < itemsCount; i++)
                {
                    if (!itemsData[i].IsValid)
                    {
                        throw new Exception("Loaded community interest label is not valid");
                    }
                }

                for (int i = 0; i < itemsCount; i++)
                {
                    async Task<CommunityInterestGridItemView.CommunityInterestGridItemData> FromItemData(
                        CommunityInterestLabelDataRequestResponse.CommunityInterestLabelData itemData)
                    {
                        var textureData = await DataDownloadingUtility.DownloadRawImageData(itemData.PosterUri);
                        return new CommunityInterestGridItemView.CommunityInterestGridItemData(itemData.Id,
                            itemData.Name, textureData);
                    }

                    var index = i;
                    tasks.Add(Task.Run(async delegate
                    {
                        gridItemsData[index] = await FromItemData(itemsData[index]);
                    }));
                }

                await Task.WhenAll(tasks);

                ItemsLiveData.AddRange(gridItemsData);
                ConfirmDataLoading();
            }

            catch (Exception e)
            {
                Debug.unityLogger.LogException(e);
            }
        }

        protected override void ConfirmDataLoading()
        {
            base.ConfirmDataLoading();
            Debug.unityLogger.Log(LogType.Log, "Repositories", "Community repository data was loaded from server");
        }

        public override Task SaveDataToServer()
        {
            throw new NotImplementedException();
        }
    }
}