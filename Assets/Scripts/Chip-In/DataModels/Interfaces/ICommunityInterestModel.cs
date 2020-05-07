namespace DataModels.Interfaces
{
    public interface ICommunityInterestModel : INamed, IMarketSegment, IIdentifier, IUsersCount, IJoinedCount,
        IMessage, IPosterImageUri, ICreatedAtTime, IStartedAtTime
    {
    }
}