namespace DataModels.Common
{
    public readonly struct Pagination
    {
        public readonly int Pages;
        public readonly int PerPage;

        public Pagination(int pages, int perPage)
        {
            Pages = pages;
            PerPage = perPage;
        }

    }
}