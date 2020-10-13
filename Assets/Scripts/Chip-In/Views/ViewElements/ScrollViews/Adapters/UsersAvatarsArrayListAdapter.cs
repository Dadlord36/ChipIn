using Com.TheFallenGames.OSA.CustomParams;
using Common;
using DataModels;
using UnityWeld.Binding;
using ViewModels.Cards;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    [Binding]
    public class UsersAvatarsArrayListAdapter : BasedListAdapter<BaseParamsWithPrefab,UserProfileBaseData>
    {
        public class FillingViewAdapter : FillingViewAdapter<UserProfileBaseData, UserAvatarItemViewModel.FieldFillingData>
        {
            public override UserAvatarItemViewModel.FieldFillingData Convert(DisposableCancellationTokenSource cancellationTokenSource,
                UserProfileBaseData data, uint dataIndexInRepository)
            {
                return new UserAvatarItemViewModel.FieldFillingData(data);
            }
        }
    }
}