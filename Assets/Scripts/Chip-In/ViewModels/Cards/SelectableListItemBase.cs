using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Behaviours;
using Common.Interfaces;
using DataModels.Interfaces;
using Factories;
using JetBrains.Annotations;
using Repositories.Local;
using Tasking;
using UnityEngine.EventSystems;
using UnityWeld.Binding;
using Views.ViewElements.Interfaces;

namespace ViewModels.Cards
{
    public abstract class ListItemBase<TDataType> : AsyncOperationsMonoBehaviour, IFillingView<TDataType>, INotifyPropertyChanged where TDataType : class
    {
        protected readonly string Tag;

        protected ListItemBase(string childClassName)
        {
            Tag = childClassName;
        }

        public abstract Task FillView(TDataType data, uint dataBaseIndex);

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }

    [Binding]
    public abstract class SelectableListItemBase<TDataType> : ListItemBase<TDataType>, IIdentifiedSelection, IPointerClickHandler where TDataType : class
    {
        protected IDownloadedSpritesRepository DownloadedSpritesRepository => SimpleAutofac.GetInstance<IDownloadedSpritesRepository>();

        public event Action<uint> ItemSelected;
        public uint IndexInOrder { get; set; }
        public TDataType ItemData { get; set; }
        public uint ItemDataIndex { get; set; }


        protected SelectableListItemBase(string childClassName) : base(childClassName)
        {
        }

        public override Task FillView(TDataType data, uint dataBaseIndex)
        {
            ItemData = data;
            ItemDataIndex = (uint) ((IIdentifier) data).Id;
            return Task.CompletedTask;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Select();
        }

        public void Select()
        {
            OnItemSelected();
        }

        private void OnItemSelected()
        {
            TasksFactories.ExecuteOnMainThread(() => { ItemSelected?.Invoke(IndexInOrder); });
        }
    }
}