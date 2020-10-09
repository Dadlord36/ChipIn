﻿using System.Collections.Generic;
using DataModels;
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

        public void AddUniqueItemAtStart(InterestBasicDataModel item)
        {
            if (!_lastViewedInterests.Exists(model => model.Id == item.Id))
            {
                _lastViewedInterests.Insert(0, item);
            }
        }

        protected override void OnDataRestored(LastViewedInterestsDataModel restoredData)
        {
            _lastViewedInterests = restoredData.LastViewedInterests;
        }
    }
}