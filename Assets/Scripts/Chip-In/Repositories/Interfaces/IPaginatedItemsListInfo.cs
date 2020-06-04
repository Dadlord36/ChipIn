namespace Repositories.Interfaces
{
    public interface IPaginatedItemsListInfo
    {
        bool IsBusy { get; }
        int ItemsPerPage { get; }
        int TotalPages { get; }
        uint TotalItemsNumber { get; }
        uint LastPageItemsNumber { get; }
    }
}