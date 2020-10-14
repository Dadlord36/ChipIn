using System;
using System.Threading.Tasks;
using DataModels;
using Factories;
using Repositories.Local;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class AdCardViewModel : SelectableListItemBase<AdvertItemDataModel>
    {
        private Sprite _adIcon;
        private string _description;

        [Binding]
        public Sprite AdIcon
        {
            get => _adIcon;
            set
            {
                if (Equals(value, _adIcon)) return;
                _adIcon = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Description
        {
            get => _description;
            private set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged();
            }
        }

        public AdCardViewModel() : base(nameof(AdCardViewModel))
        {
        }

        public override async Task FillView(AdvertItemDataModel data, uint dataBaseIndex)
        {
            await base.FillView(data, dataBaseIndex).ConfigureAwait(false);

            AsyncOperationCancellationController.CancelOngoingTask();

            //ToDo: replace with description
            IndexInOrder = dataBaseIndex;
            Description = dataBaseIndex.ToString();
            try
            {
                AdIcon = await SimpleAutofac.GetInstance<IDownloadedSpritesRepository>()
                    .CreateLoadSpriteTask(data.LogoUrl, AsyncOperationCancellationController.CancellationToken)
                    .ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}