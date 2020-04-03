using Common.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using Views.ViewElements.Interfaces;

namespace Views.ViewElements.ScrollableList
{
    [DisallowMultipleComponent]
    public class ScrollItemsUpdater : UIBehaviour, IInitialize
    {
        [SerializeField] private Transform rectTransformMiddle;
        private Transform[] _contentItems;

        private RectTransform rectTransform;
        private IContentItemUpdater[] _contentItemsUpdaters;
        private IContentItemCanvasReceiver[] _contentItemsCanvasReceivers;

        private Vector3 _tempItemScale;

        public void Initialize()
        {
            var objectTransform = transform;

            var childCount = objectTransform.childCount;
            _contentItems = new Transform[childCount];
            _contentItemsUpdaters = GetComponents<IContentItemUpdater>();
            _contentItemsCanvasReceivers = GetComponents<IContentItemCanvasReceiver>();

            for (int i = 0; i < objectTransform.childCount; i++)
            {
                _contentItems[i] = objectTransform.GetChild(i);
            }

            rectTransform = transform as RectTransform;
        }

        private void Update()
        {
            if (!enabled) return;
            for (int i = 0; i < _contentItems.Length; i++)
            {
                SendDataToContentItemControllers(_contentItems[i], Mathf.Clamp01(GetItemPathPercentage(_contentItems[i])));
            }

            void SendDataToContentItemControllers(Transform item, float pathPercentage)
            {
                for (int i = 0; i < _contentItemsUpdaters.Length; i++)
                {
                    _contentItemsUpdaters[i].UpdateContentItem(item, pathPercentage);
                }

                for (int i = 0; i < _contentItemsCanvasReceivers.Length; i++)
                {
                    if (item.TryGetComponent(out Canvas canvas))
                        _contentItemsCanvasReceivers[i].UpdateContentItemCanvas(item, canvas, pathPercentage);
                }
            }
        }

        private float GetItemPathPercentage(Transform item)
        {
            float GetControlParameter()
            {
                return item.position.x - rectTransformMiddle.position.x;
            }

            var point = Mathf.Abs(GetControlParameter());
            return 1f - (point / rectTransform.rect.width);
        }
    }
}