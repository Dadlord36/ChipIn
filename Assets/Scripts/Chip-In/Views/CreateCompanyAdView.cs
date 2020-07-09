using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public sealed class CreateCompanyAdView : BaseView
    {
        [SerializeField] private Image logoIcon;
        [SerializeField] private Image companyPoster;

        public Sprite Logo
        {
            get => logoIcon.sprite;
            set => logoIcon.sprite = value;
        }

        public Sprite CompanyPoster
        {
            get => companyPoster.sprite;
            set => companyPoster.sprite = value;
        }

        public CreateCompanyAdView() : base(nameof(CreateCompanyAdView))
        {
        }
    }
}