using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ITotalBill
    {
        [JsonProperty("total_bill")] uint TotalBill { get; set; }
    }
}