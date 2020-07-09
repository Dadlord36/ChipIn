using System.Collections.Specialized;
using GlobalVariables;
using Newtonsoft.Json;

namespace DataModels.Common
{

    public interface IPaginationBase
    {
        [JsonProperty("page")]  int Page { get; set; }
        [JsonProperty("per_page")]  int PerPage { get; set; }
    }
    
    public class PaginatedRequestData :  IPaginationBase
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        
        public NameValueCollection ConvertPaginationToNameValueCollection()
        {
            var collection = new NameValueCollection(2)
            {
                {MainNames.Pagination.Page, Page.ToString()},
                {MainNames.Pagination.PerPage, PerPage.ToString()}
            };
            return collection;
        }
        
        public PaginatedRequestData(int page, int perPage)
        {
            Page = page;
            PerPage = perPage;
        }
    }
    
    
    public class PaginatedResponseData : PaginatedRequestData
    {
        [JsonProperty("total")] public int Total { get; set; }
        public PaginatedResponseData(int total, int page, int perPage) : base(page, perPage)
        {
            Total = total;
        }
    }
    
}