using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Repositories.Remote
{
    public interface IMerchantLibraryModel : IUserProfile
    {
        string CompanySlogan { get; set; }
        string CompanyIconPath { get; set; }
    }

    public class MerchantLibraryDataModel : IMerchantLibraryModel
    {
        public string Name { get; set; } = "Company Name";
        public int Id { get; set; } = 15155;
        public string Email { get; set; } = "Company Email";
        public string CompanySlogan { get; set; }
        public string CompanyIconPath { get; set; }
    }

    [CreateAssetMenu(fileName = nameof(MerchantLibraryRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(MerchantLibraryRepository), order = 0)]
    public sealed class MerchantLibraryRepository : RemoteRepositoryBase, IMerchantLibraryModel, INotifyPropertyChanged
    {
        private readonly IMerchantLibraryModel _merchantLibraryModel = new MerchantLibraryDataModel();
        
        public string Name
        {
            get => _merchantLibraryModel.Name;
            set
            {
                _merchantLibraryModel.Name = value;
                OnPropertyChanged();
            }
        }

        public int Id
        {
            get => _merchantLibraryModel.Id;
            set
            {
                _merchantLibraryModel.Id = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _merchantLibraryModel.Email;
            set
            {
                _merchantLibraryModel.Email = value;
                OnPropertyChanged();
            }
        }

        public string CompanySlogan
        {
            get => _merchantLibraryModel.CompanySlogan;
            set
            {
                _merchantLibraryModel.CompanySlogan = value;
                OnPropertyChanged();
            }
        }

        public string CompanyIconPath
        {
            get => _merchantLibraryModel.CompanyIconPath;
            set
            {
                _merchantLibraryModel.CompanyIconPath = value;
                OnPropertyChanged();
            }
        }

        public override async Task LoadDataFromServer()
        {
            throw new System.NotImplementedException();
        }

        public override async Task SaveDataToServer()
        {
            throw new System.NotImplementedException();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}