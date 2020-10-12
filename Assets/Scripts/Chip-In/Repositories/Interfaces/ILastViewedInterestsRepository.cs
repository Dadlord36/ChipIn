using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels;
using Repositories.Local;
using Repositories.Local.DataModels;

namespace Repositories.Interfaces
{
    public interface ILastViewedInterestsRepository : IRestorable
    {
        IList<InterestBasicDataModel> LastViewedInterestsList { get; }
        Task AddUniqueItemAtStartAsync(InterestBasicDataModel item);
        Task SaveToLocalStorageAsync(LastViewedInterestsDataModel dataToSave);
    }
}