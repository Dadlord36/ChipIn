using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityWeld.Binding;
using Utilities;
using Views.Base;

namespace ViewModels.Basic
{
    public abstract class BaseItemsListViewModel<TBaseView> : CorrespondingViewsSwitchingViewModel<TBaseView>, INotifyPropertyChanged
        where TBaseView : ItemsListBaseView
    {
        private const string Tag = nameof(BaseItemsListViewModel<TBaseView>);

        private bool _itemIsSelected;

        [Binding]
        public bool ItemIsSelected
        {
            get => _itemIsSelected;
            protected set
            {
                if (value == _itemIsSelected) return;
                _itemIsSelected = value;
                LogUtility.PrintLog(Tag, $"ChallengeIsSelected: {_itemIsSelected.ToString()}");
                OnPropertyChanged();
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            SubscribeToViewEvents();
            LoadDataAndFillTheList();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeFromViewEvents();
        }

        private void SubscribeToViewEvents()
        {
            RelatedView.RelatedItemSelected += OnSelectedItemIndexChanged;
            RelatedView.ItemsListUpdated += OnItemsListUpdated;
        }

        private void UnsubscribeFromViewEvents()
        {
            RelatedView.RelatedItemSelected -= OnSelectedItemIndexChanged;
            RelatedView.ItemsListUpdated -= OnItemsListUpdated;
        }

        protected abstract Task FillInfoCardWithRelatedData(int selectedId);

        protected virtual void OnSelectedItemIndexChanged(int relatedItemIndex)
        {
            ItemIsSelected = true;
        }

        protected virtual void OnItemsListUpdated()
        {
            ItemIsSelected = true;
        }

        protected abstract void LoadDataAndFillTheList();
        protected abstract Task FillDropdownList();

        protected void FillDropdownList(Dictionary<int?, string> dictionary)
        {
            RelatedView.FillDropdownList(dictionary);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}