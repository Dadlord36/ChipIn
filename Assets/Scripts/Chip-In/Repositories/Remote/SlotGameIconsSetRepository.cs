using System;
using System.Threading.Tasks;
using DataModels;
using DataModels.Interfaces;
using ScriptableObjects.DataSets;
using UnityEngine;

namespace Repositories.Remote
{

    [CreateAssetMenu(fileName = nameof(SlotGameIconsSetRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(SlotGameIconsSetRepository), order = 0)]
    public sealed class SlotGameIconsSetRepository : RemoteRepositoryBase,ISlotGameIconsSet
    {
        [SerializeField] private SlotGameIconsSetScriptableObject defaultSlotIconsSet;
        [NonSerialized] private SlotGameIconsSet _loadedSlotGameIconsSet;
        
        private ISlotGameIconsSet _selectedSlotGameIconsSet;

        private void OnEnable()
        {
            _selectedSlotGameIconsSet = defaultSlotIconsSet;
        }

        public Sprite First
        {
            get => _selectedSlotGameIconsSet.First;
            set => _selectedSlotGameIconsSet.First = value;
        }

        public Sprite Second
        {
            get => _selectedSlotGameIconsSet.Second;
            set => _selectedSlotGameIconsSet.Second = value;
        }

        public Sprite Third
        {
            get => _selectedSlotGameIconsSet.Third;
            set => _selectedSlotGameIconsSet.Third = value;
        }

        public Sprite Fourth
        {
            get => _selectedSlotGameIconsSet.Fourth;
            set => _selectedSlotGameIconsSet.Fourth = value;
        }

        public override Task LoadDataFromServer()
        {
            throw new NotImplementedException();
        }

        public override Task SaveDataToServer()
        {
            throw new NotImplementedException();
        }
    }
}