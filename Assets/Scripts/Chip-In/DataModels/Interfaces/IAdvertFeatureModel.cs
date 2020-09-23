namespace DataModels.Interfaces
{
    public interface IAdvertFeatureBaseModel : IDescription, ITokensAmount, IIconUrl
    {
    }
    
    public interface IAdvertFeatureModel : IAdvertFeatureBaseModel, IIdentifier , IWatched
    {
    }
}