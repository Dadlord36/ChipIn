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
    [Binding]
    public abstract class SelectableListItemBase<TDataType> : AsyncOperationsMonoBehaviour, IFillingView<TDataType>, IIdentifiedSelection,
        IPointerClickHandler, INotifyPropertyChanged where TDataType : class
    {
        protected readonly string Tag;

        protected IDownloadedSpritesRepository DownloadedSpritesRepository => SimpleAutofac.GetInstance<IDownloadedSpritesRepository>();

        public event Action<uint> ItemSelected;
        public uint IndexInOrder { get; set; }
        public TDataType ItemData { get; set; }
        public uint ItemDataIndex { get; set; }


        protected SelectableListItemBase(string childClassName)
        {
            Tag = childClassName;
        }

        public virtual Task FillView(TDataType data, uint dataBaseIndex)
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
            ItemSelected?.Invoke(IndexInOrder);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() =>
            {
                TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
            });
        }
    }
}