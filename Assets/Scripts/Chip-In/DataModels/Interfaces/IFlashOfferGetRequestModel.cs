using Newtonsoft.Json;
using Views.Bars.BarItems;

namespace DataModels.Interfaces
{
    public interface IFlashOfferGetRequestModel : ICategory, ITitled, IDescription, IQuantity, IRadius, ITokensAmount, IExpireDate
    {
    }
}