using System.Threading.Tasks;
using Repositories.Remote;
using UnityEngine;
using UnityWeld.Binding;
using Views.ViewElements;

namespace ViewModels
{
    [Binding]
    public class LibraryViewModel : ViewsSwitchingViewModel, IMerchantLibraryModel
    {
        [SerializeField] private SettableIconView settableIconView;
        [SerializeField] private MerchantLibraryRepository merchantLibraryRepository;

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            settableIconView.IconWasSelectedFromGallery += OnIconWasSelectedFromGallery;
        }

        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
            settableIconView.IconWasSelectedFromGallery -= OnIconWasSelectedFromGallery;
        }

        private void OnIconWasSelectedFromGallery(string path)
        {
            CompanyIconPath = path;
        }

        [Binding]
        public string Name
        {
            get => merchantLibraryRepository.Name;
            set => merchantLibraryRepository.Name = value;
        }

        [Binding]
        public int Id
        {
            get => merchantLibraryRepository.Id;
            set => merchantLibraryRepository.Id = value;
        }

        [Binding]
        public string Email
        {
            get => merchantLibraryRepository.Email;
            set => merchantLibraryRepository.Email = value;
        }

        [Binding]
        public string CompanySlogan
        {
            get => merchantLibraryRepository.CompanySlogan;
            set => merchantLibraryRepository.CompanySlogan = value;
        }

        [Binding]
        public string CompanyIconPath
        {
            get => merchantLibraryRepository.CompanyIconPath;
            set => merchantLibraryRepository.CompanyIconPath = value;
        }
    }
}