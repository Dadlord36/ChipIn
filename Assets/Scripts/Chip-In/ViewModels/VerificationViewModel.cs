using System.ComponentModel;
using Repositories.Remote;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels
{
    [Binding]
    public class VerificationViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        [SerializeField] private MerchantVerificationRepository merchantVerificationRepository;

        [Binding]
        public string ProductCodePart1
        {
            get => merchantVerificationRepository.ProductCodes.Fist;
            set => merchantVerificationRepository.ProductCodes.Fist = value;
        }

        [Binding]
        public string ProductCodePart2
        {
            get => merchantVerificationRepository.ProductCodes.Second;
            set => merchantVerificationRepository.ProductCodes.Second = value;
        }

        [Binding]
        public string ProductCodePart3
        {
            get => merchantVerificationRepository.ProductCodes.Third;
            set => merchantVerificationRepository.ProductCodes.Third = value;
        }


        [Binding]
        public string SponsoredAdCodePart1
        {
            get => merchantVerificationRepository.SponsoredAdCodes.Fist;
            set => merchantVerificationRepository.SponsoredAdCodes.Fist = value;
        }

        [Binding]
        public string SponsoredAdCodePart2
        {
            get => merchantVerificationRepository.SponsoredAdCodes.Second;
            set => merchantVerificationRepository.SponsoredAdCodes.Second = value;
        }

        [Binding]
        public string SponsoredAdCodePart3
        {
            get => merchantVerificationRepository.SponsoredAdCodes.Third;
            set => merchantVerificationRepository.SponsoredAdCodes.Third = value;
        }

        [Binding]
        public string CommunityAdCodePart1
        {
            get => merchantVerificationRepository.CommunityAdCodes.Fist;
            set => merchantVerificationRepository.CommunityAdCodes.Fist = value;
        }

        [Binding]
        public string CommunityAdCodePart2
        {
            get => merchantVerificationRepository.CommunityAdCodes.Second;
            set => merchantVerificationRepository.CommunityAdCodes.Second = value;
        }

        [Binding]
        public string CommunityAdCodePart3
        {
            get => merchantVerificationRepository.CommunityAdCodes.Third;
            set => merchantVerificationRepository.CommunityAdCodes.Third = value;
        }

        [Binding]
        public string TailoredOfferCodePart1
        {
            get => merchantVerificationRepository.TailoredOfferCodes.Fist;
            set => merchantVerificationRepository.TailoredOfferCodes.Fist = value;
        }

        [Binding]
        public string TailoredOfferCodePart2
        {
            get => merchantVerificationRepository.TailoredOfferCodes.Second;
            set => merchantVerificationRepository.TailoredOfferCodes.Second = value;
        }

        [Binding]
        public string TailoredOfferCodePart3
        {
            get => merchantVerificationRepository.TailoredOfferCodes.Third;
            set => merchantVerificationRepository.TailoredOfferCodes.Third = value;
        }

        [Binding]
        public string BulkOfferCodePart1
        {
            get => merchantVerificationRepository.BulkOfferCodes.Fist;
            set => merchantVerificationRepository.BulkOfferCodes.Fist = value;
        }

        [Binding]
        public string BulkOfferCodePart2
        {
            get => merchantVerificationRepository.BulkOfferCodes.Second;
            set => merchantVerificationRepository.BulkOfferCodes.Second = value;
        }

        [Binding]
        public string BulkOfferCodePart3
        {
            get => merchantVerificationRepository.BulkOfferCodes.Third;
            set => merchantVerificationRepository.BulkOfferCodes.Third = value;
        }

        [Binding]
        public string RedemptionCodePart1
        {
            get => merchantVerificationRepository.RedemptionCodes.Fist;
            set => merchantVerificationRepository.RedemptionCodes.Fist = value;
        }

        [Binding]
        public string RedemptionCodePart2
        {
            get => merchantVerificationRepository.RedemptionCodes.Second;
            set => merchantVerificationRepository.RedemptionCodes.Second = value;
        }

        [Binding]
        public string RedemptionCodePart3
        {
            get => merchantVerificationRepository.RedemptionCodes.Third;
            set => merchantVerificationRepository.RedemptionCodes.Third = value;
        }

        [Binding]
        public string Name
        {
            get => merchantVerificationRepository.Name;
            set => merchantVerificationRepository.Name = value;
        }

        [Binding]
        public int Id
        {
            get => merchantVerificationRepository.Id;
            set => merchantVerificationRepository.Id = value;
        }

        [Binding]
        public string Email
        {
            get => merchantVerificationRepository.Email;
            set => merchantVerificationRepository.Email = value;
        }

        [Binding]
        public bool FastAccess
        {
            get => merchantVerificationRepository.FastAccess;
            set => merchantVerificationRepository.FastAccess = value;
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add => merchantVerificationRepository.PropertyChanged += value;
            remove => merchantVerificationRepository.PropertyChanged -= value;
        }
        
        public VerificationViewModel() : base(nameof(VerificationViewModel))
        {
        }
    }
}