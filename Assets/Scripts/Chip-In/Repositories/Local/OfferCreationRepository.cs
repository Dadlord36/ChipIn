using UnityEngine;

namespace Repositories.Local
{
    [CreateAssetMenu(fileName = nameof(OfferCreationRepository), menuName = nameof(Repositories) + "/" + nameof(Local) +
                                                                            "/" + nameof(OfferCreationRepository), order = 0)]
    public class OfferCreationRepository : ScriptableObject
    {
        public string OfferSegmentName { get; set; }
    }
}