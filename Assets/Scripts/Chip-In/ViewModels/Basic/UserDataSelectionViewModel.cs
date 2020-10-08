using Controllers;
using DataModels;
using UnityWeld.Binding;

namespace ViewModels.Basic
{
    [Binding]
    public sealed class UserDataSelectionViewModel : BaseOptionsSelectionViewModel<UserProfileBaseData>
    {
        public UserDataSelectionViewModel() : base(nameof(UserDataSelectionViewModel))
        {
        }

        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
            //Clears the underlying in hierarchy list with items.
            GetComponentInChildren<IClearable>().Clear();
        }
    }
}