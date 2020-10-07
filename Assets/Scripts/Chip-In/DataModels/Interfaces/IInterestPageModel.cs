namespace DataModels.Interfaces
{
    public interface IInterestPageModel : IInterestBasicModel, IMarketSegment, IUsersCount, IJoinedCount, IWatchedCount, ISupportedCount,
        IFoundedCount, ITotalFound, IInterestMessage, ICreatedAtTime, IStartedAtTime, IEndsAtTime
    {
    }
    
    
    public interface IMerchantInterestPageModel : IInterestPageModel, IUserInterestPageActions
    {
    }
}