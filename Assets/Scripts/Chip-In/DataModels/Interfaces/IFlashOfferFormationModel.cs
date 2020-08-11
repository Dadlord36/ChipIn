using Views.Bars.BarItems;

namespace DataModels.Interfaces
{
    public interface IFlashOfferFormationModel : IIdentifier, ITitled, IDescription, IQuantity, ITokensAmount, IRadius, IExpireDate,
        IPosterImageUri
    {
    }
}