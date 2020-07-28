using DataModels;
using UnityEngine;

namespace Repositories.Temporary
{
    [CreateAssetMenu(fileName = nameof(SponsoredAdRepository), menuName = nameof(Repositories) + "/" + nameof(Temporary) + "/"
                                                                          + nameof(SponsoredAdRepository), order = 0)]
    public class SponsoredAdRepository : StringSourcedDataRepository<SponsoredAdDataModel>
    {
    }
}