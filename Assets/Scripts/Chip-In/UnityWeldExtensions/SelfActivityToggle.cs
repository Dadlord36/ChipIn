using UnityEngine;
using UnityWeld.Binding;
using UnityWeld.Binding.Internal;

namespace UnityWeldExtensions
{
    [AddComponentMenu("Unity Weld/SelfActivityToggle Binding")]
    public class SelfActivityToggle : AbstractMemberBinding
    {
        [SerializeField] private string viewAdapterTypeName;
        [SerializeField] private AdapterOptions viewAdapterOptions;
        [SerializeField] private string viewModelPropertyName;
        private PropertyWatcher viewModelWatcher;

        public string ViewAdapterTypeName
        {
            get => viewAdapterTypeName;
            set => viewAdapterTypeName = value;
        }

        public AdapterOptions ViewAdapterOptions
        {
            get => viewAdapterOptions;
            set => viewAdapterOptions = value;
        }

        public string ViewModelPropertyName
        {
            get => viewModelPropertyName;
            set => viewModelPropertyName = value;
        }

        public bool IsActive
        {
            set => gameObject.SetActive(value);
        }

        public override void Connect()
        {
            PropertyEndPoint source = MakeViewModelEndPoint(viewModelPropertyName, null, null);
            PropertySync propertySync = new PropertySync(source, new PropertyEndPoint(this, "IsActive",
                    CreateAdapter(viewAdapterTypeName), viewAdapterOptions, "view", this),
                null, this);
            viewModelWatcher = source.Watch( () => propertySync.SyncFromSource());
            propertySync.SyncFromSource();
        }

        public override void Disconnect()
        {
            if (viewModelWatcher == null)
                return;
            viewModelWatcher.Dispose();
            viewModelWatcher = null;
        }
    }
}