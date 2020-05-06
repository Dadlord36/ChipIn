using DataModels.Interfaces;
using Views.Bars.BarItems;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public interface IProductModel : IIdentifier, IQrData, ITitled, IDescription, IPosterImageUri
    {
    }
}