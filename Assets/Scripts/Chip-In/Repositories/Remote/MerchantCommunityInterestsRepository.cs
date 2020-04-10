using System.Threading.Tasks;
using DataModels;
using UnityEngine;

namespace Repositories.Remote
{
    [CreateAssetMenu(fileName = nameof(MerchantCommunityInterestsRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(MerchantCommunityInterestsRepository), order = 0)]
    public class MerchantCommunityInterestsRepository : BaseItemsListRepository<EngageCardDataModel>
    {
        public override async Task LoadDataFromServer()
        {
            throw new System.NotImplementedException();
        }

        public override async Task SaveDataToServer()
        {
            throw new System.NotImplementedException();
        }
    }
}