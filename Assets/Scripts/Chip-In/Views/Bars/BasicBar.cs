using UnityEngine;
using UnityWeld.Binding;
using ViewModels.Interfaces;

namespace Views.Bars
{
    [Binding]
    public class BasicBar : MonoBehaviour
    {
        private ObservableList<ICompanyAdFeatureModel> _companyAdFeatureModels;

        [Binding]
        public ObservableList<ICompanyAdFeatureModel> CompanyAdFeatureModels
        {
            get => _companyAdFeatureModels;
            set => _companyAdFeatureModels = value;
        }
    }
}