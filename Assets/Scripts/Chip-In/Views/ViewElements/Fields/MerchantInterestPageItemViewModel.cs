using System.Threading.Tasks;
using DataModels;
using UnityWeld.Binding;

namespace Views.ViewElements.Fields
{
    [Binding]
    public sealed class MerchantInterestPageItemViewModel : NameAndNumberSelectableFieldBase<MerchantInterestPageDataModel>
    {
        public MerchantInterestPageItemViewModel() : base(nameof(MerchantInterestPageItemViewModel))
        {
        }
        
        public override Task FillView(MerchantInterestPageDataModel data, uint dataBaseIndex)
        {
            base.FillView(data, dataBaseIndex);
            Name = data.Name;
            //TODO: replace with percentage
            Number = data.UsersCount.ToString();
            return Task.CompletedTask;
        }
    }
}