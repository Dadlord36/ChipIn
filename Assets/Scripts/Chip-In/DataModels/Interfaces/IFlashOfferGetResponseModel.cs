using Views.Bars.BarItems;

namespace DataModels.Interfaces
{
    public interface IFlashOfferGetResponseModel : IIdentifier, ICategory, ITitled, IDescription, IQuantity, IRadius, ITokensAmount, IExpireDate,
        IPosterImageUri
    {
    }
}