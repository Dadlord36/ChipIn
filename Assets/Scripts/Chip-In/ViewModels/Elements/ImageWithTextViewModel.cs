using System;
using System.Threading.Tasks;
using DataModels;
using Factories;
using Repositories.Local;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Cards;

namespace ViewModels.Elements
{
    [Binding]
    public sealed class ImageWithTextViewModel : SelectableListItemBase<InterestBasicDataModel>
    {
        private Sprite _iconSprite;

        [Binding]
        public Sprite IconSprite
        {
            get => _iconSprite;
            set
            {
                _iconSprite = value;
                OnPropertyChanged();
            }
        }

        private string _text;

        [Binding]
        public string Text
        {
            get => _text;
            set
            {
                if (value == _text) return;
                _text = value;
                OnPropertyChanged();
            }
        }

        public ImageWithTextViewModel() : base(nameof(ImageWithTextViewModel))
        {
        }

        public override async Task FillView(InterestBasicDataModel data, uint dataBaseIndex)
        {
            await base.FillView(data, dataBaseIndex).ConfigureAwait(false);
            AsyncOperationCancellationController.CancelOngoingTask();

            try
            {
                Text = data.Name;
                IconSprite = await SimpleAutofac.GetInstance<IDownloadedSpritesRepository>().CreateLoadSpriteTask(data.PosterUri,
                        AsyncOperationCancellationController.CancellationToken)
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