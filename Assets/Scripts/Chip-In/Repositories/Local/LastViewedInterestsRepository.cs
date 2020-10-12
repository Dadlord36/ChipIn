using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels;
using DataModels.Interfaces;
using Repositories.Interfaces;
using Repositories.Local.DataModels;

namespace Repositories.Local
{
    public class LastViewedInterestsRepository : BaseStorableRepository<LastViewedInterestsDataModel>, ILastViewedInterestsRepository
    {
        private List<InterestBasicDataModel> _lastViewedInterests = new List<InterestBasicDataModel>();
        public IList<InterestBasicDataModel> LastViewedInterestsList => _lastViewedInterests;

        public LastViewedInterestsRepository() : base("LocalRepositoriesData", nameof(LastViewedInterestsRepository))
        {
        }

        public Task AddUniqueItemAtStartAsync(InterestBasicDataModel item)
        {
            if (ItemIsExists(item)) return Task.CompletedTask;
            _lastViewedInterests.Insert(0, item);
            return SaveItemsAsync();
        }

        public Task RemoveIfExistsAsync(InterestBasicDataModel interest)
        {
            if (!ItemIsExists(interest)) return Task.CompletedTask;
            _lastViewedInterests.RemoveAt(_lastViewedInterests.FindIndex(model => model.Id == interest.Id));
            return SaveItemsAsync();
        }

        protected override void OnDataRestored(LastViewedInterestsDataModel restoredData)
        {
            _lastViewedInterests = restoredData.LastViewedInterests;
        }

        private bool ItemIsExists(IIdentifier item)
        {
            return _lastViewedInterests.Exists(model => model.Id == item.Id);
        }

        private Task SaveItemsAsync()
        {
            return SaveToLocalStorageAsync(new LastViewedInterestsDataModel {LastViewedInterests = _lastViewedInterests});
        }
    }
}