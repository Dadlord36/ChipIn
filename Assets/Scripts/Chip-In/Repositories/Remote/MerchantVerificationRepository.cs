using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Repositories.Remote
{
    public class CodesSet
    {
        public string Fist { get; set; } = "2361";
        public string Second { get; set; } = "032";
        public string Third { get; set; } = "6214";
    }

    public interface IVerificationModel : IUserProfile
    {
        CodesSet ProductCodes { get; set; }
        CodesSet SponsoredAdCodes { get; set; }
        CodesSet CommunityAdCodes { get; set; }
        CodesSet TailoredOfferCodes { get; set; }
        CodesSet BulkOfferCodes { get; set; }
        CodesSet RedemptionCodes { get; set; }
        bool FastAccess { get; set; }
    }

    public class VerificationDataModel : IVerificationModel
    {
        public string Name { get; set; } = "jhon Gremm";
        public int Id { get; set; }
        public string Email { get; set; }
        public CodesSet ProductCodes { get; set; } = new CodesSet();
        public CodesSet SponsoredAdCodes { get; set; } = new CodesSet();
        public CodesSet CommunityAdCodes { get; set; } = new CodesSet();
        public CodesSet TailoredOfferCodes { get; set; } = new CodesSet();
        public CodesSet BulkOfferCodes { get; set; } = new CodesSet();
        public CodesSet RedemptionCodes { get; set; } = new CodesSet();
        public bool FastAccess { get; set; }
    }

    [CreateAssetMenu(fileName = nameof(MerchantVerificationRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(MerchantVerificationRepository), order = 0)]
    public sealed class MerchantVerificationRepository : RemoteRepositoryBase, INotifyPropertyChanged, IVerificationModel
    {
        private readonly IVerificationModel _verificationModelImplementation = new VerificationDataModel();

        public override async Task LoadDataFromServer()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Name
        {
            get => _verificationModelImplementation.Name;
            set
            {
                _verificationModelImplementation.Name = value;
                OnPropertyChanged();
            }
        }

        public int Id
        {
            get => _verificationModelImplementation.Id;
            set
            {
                _verificationModelImplementation.Id = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _verificationModelImplementation.Email;
            set
            {
                _verificationModelImplementation.Email = value;
                OnPropertyChanged();
            }
        }

        public CodesSet ProductCodes
        {
            get => _verificationModelImplementation.ProductCodes;
            set
            {
                _verificationModelImplementation.ProductCodes = value;
                OnPropertyChanged();
            }
        }

        public CodesSet SponsoredAdCodes
        {
            get => _verificationModelImplementation.SponsoredAdCodes;
            set
            {
                _verificationModelImplementation.SponsoredAdCodes = value;
                OnPropertyChanged();
            }
        }

        public CodesSet CommunityAdCodes
        {
            get => _verificationModelImplementation.CommunityAdCodes;
            set
            {
                _verificationModelImplementation.CommunityAdCodes = value;
                OnPropertyChanged();
            }
        }

        public CodesSet TailoredOfferCodes
        {
            get => _verificationModelImplementation.TailoredOfferCodes;
            set
            {
                _verificationModelImplementation.TailoredOfferCodes = value;
                OnPropertyChanged();
            }
        }

        public CodesSet BulkOfferCodes
        {
            get => _verificationModelImplementation.BulkOfferCodes;
            set
            {
                _verificationModelImplementation.BulkOfferCodes = value;
                OnPropertyChanged();
            }
        }

        public CodesSet RedemptionCodes
        {
            get => _verificationModelImplementation.RedemptionCodes;
            set
            {
                _verificationModelImplementation.RedemptionCodes = value;
                OnPropertyChanged();
            }
        }

        public bool FastAccess
        {
            get => _verificationModelImplementation.FastAccess;
            set
            {
                _verificationModelImplementation.FastAccess = value;
                OnPropertyChanged();
            }
        }
    }
}