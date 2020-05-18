namespace DataModels.Interfaces
{
    public interface ICommunityInterestModel : INamed, IMarketSegment, IIdentifier, IUsersCount, IJoinedCount,
        IMemberMessage, IMerchantMessage, IPosterImageUri, ICreatedAtTime, IStartedAtTime
    {
    }
}