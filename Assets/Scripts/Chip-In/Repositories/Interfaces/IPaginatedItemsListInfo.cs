namespace Repositories.Interfaces
{
    public interface IPaginatedItemsListInfo
    {
        bool IsInitialized { get; }
        uint GetCorrespondingToIndexPage(uint pageItemIndex);
        int ItemsPerPage { get; }
        int TotalPages { get; }
        uint TotalItemsNumber { get; }
        uint LastPageItemsNumber { get; }
    }
}