using Repositories.Remote;
using UnityEngine;

namespace DataModels.Interfaces
{
    public interface IMerchantProfileSettings : IMerchantProfileSettingsModel
    {
        Sprite AvatarSprite { get; set; }
        Sprite LogoSprite { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}