using Views.Bars.BarItems;

namespace DataModels.Interfaces
{
    public interface IProductModel : IIdentifier, IQrData, ITitled, IDescription, IPosterImageUri
    {
    }
}