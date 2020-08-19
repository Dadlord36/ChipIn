using System;
using System.Threading.Tasks;
using Common;
using DataModels;
using RequestsStaticProcessors;
using UnityEngine;
using Utilities;

namespace Repositories.Remote
{
    [CreateAssetMenu(fileName = nameof(UserProductsRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(UserProductsRepository), order = 0)]
    public class UserProductsRepository : BaseNotPaginatedListRepository<ProductDataModel>
    {
        private const string Tag = nameof(UserProductsRepository);

        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;

        public int CurrentlySelectedIndex { get; set; }
        public ProductDataModel CurrentlySelectedProduct => this[CurrentlySelectedIndex];

        public override async Task LoadDataFromServer()
        {
            try
            {
                var result = await UserProductsStaticRequestsProcessor.GetUserProducts(out TasksCancellationTokenSource,
                    authorisationDataRepository);
                
                if (!result.Success)
                {
                    LogUtility.PrintLogError(Tag, "User Products was not loaded");
                    return;
                }

                ItemsLiveData = new LiveData<ProductDataModel>(result.ResponseModelInterface.ProductsData);

                ConfirmDataLoading();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}