using GlobalVariables;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ICategory
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Category)]
        string Category { get; set; }
    }

    public interface IProductCategory
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.ProductCategory)]
        string ProductCategory
        {
            get;
            set;
        }
    }
}