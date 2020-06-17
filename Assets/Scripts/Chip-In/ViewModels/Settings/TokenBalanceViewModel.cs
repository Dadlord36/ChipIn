using UnityWeld.Binding;
using ViewModels.Basic;

namespace ViewModels.Settings
{
    [Binding]
    public class TokenBalanceViewModel : BaseViewModel
    {
        public TokenBalanceViewModel() : base(nameof(TokenBalanceViewModel))
        {
        }
    }
}