using System.Collections.Specialized;
using GlobalVariables;
using Newtonsoft.Json;

namespace DataModels.Common
{
    public class PaginationData
    {
        [JsonProperty("total")] public int Total;
        [JsonProperty("page")] public int Page;
        [JsonProperty("per_page")] public int PerPage;

        public NameValueCollection ConvertPaginationToNameValueCollection()
        {
            var collection = new NameValueCollection(2)
            {
                {MainNames.Pagination.Page, Page.ToString()},
                {MainNames.Pagination.PerPage, PerPage.ToString()}
            };
            return collection;
        }
    }

    public interface IPaginated
    {
        PaginationData Pagination { get; set; }
    }
}