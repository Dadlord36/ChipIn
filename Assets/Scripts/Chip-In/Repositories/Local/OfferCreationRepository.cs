using DataModels;
using UnityEngine;

namespace Repositories.Local
{
    [CreateAssetMenu(fileName = nameof(OfferCreationRepository), menuName = nameof(Repositories) + "/" + nameof(Local) +
                                                                            "/" + nameof(OfferCreationRepository), order = 0)]
    public class OfferCreationRepository : ScriptableObject
    {
        public string OfferSegmentName { get; set; }
        
        /*public EngageCardDataModel SelectedInterestData { get; set; }*/
        
        /*public OfferFormationData OfferFormationData { get; } = new OfferFormationData();


        public string this[string fieldName]
        {
            get => OfferFormationData[fieldName];
            set => OfferFormationData[fieldName] = value;
        }*/
    }
}