using System;
using UnityEngine;

namespace Repositories.Local.SingleItem
{
    [CreateAssetMenu(fileName = nameof(SelectedUserInterestRepository), menuName = nameof(Repositories) + "/" + nameof(Local) + "/" + nameof(SingleItem)
                                                                                   + "/" + nameof(SelectedUserInterestRepository), order = 0)]
    public class SelectedUserInterestRepository : SingleItemLocalRepository
    {
        private int? _selectedInterestId;

        public int? SelectedInterestId
        {
            get => _selectedInterestId;
            set => _selectedInterestId = value;
        }

        private void OnEnable()
        {
            SelectedInterestId = 202;
        }
    }
}