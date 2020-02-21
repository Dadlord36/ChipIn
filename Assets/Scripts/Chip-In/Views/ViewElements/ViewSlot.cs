using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;
using RectTransformUtility = Utilities.RectTransformUtility;

namespace Views.ViewElements
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Canvas))]
    public class ViewSlot : UIBehaviour
    {
        public bool Occupied => _view != null;
        private BaseView _view;

        protected override void Awake()
        {
            base.Awake();
            ResetTransform(((RectTransform) transform));
        }

        public Vector2 ViewSlotSize
        {
            get => ((RectTransform) transform).sizeDelta;
            set => ((RectTransform) transform).sizeDelta = value;
        }

        public void Stretch()
        {
            RectTransformUtility.Stretch(transform as RectTransform);
        }
        
        public int CanvasSortingOrder
        {
            set
            {
                var canvas =  GetComponent<Canvas>();
                canvas.overrideSorting = true;
                canvas.sortingOrder = value;
            }
            get => GetComponent<Canvas>().sortingOrder;
        }

        private static void ResetTransform(RectTransform rectTransform)
        {
            RectTransformUtility.ResetSize(rectTransform);
            RectTransformUtility.ResetScale(rectTransform);
        }

        public void AttachView(BaseView view)
        {
            _view = view;
            var viewRectTransform = _view.ViewRootRectTransform;
            viewRectTransform.SetParent(transform);
            ResetTransform(viewRectTransform);
            RectTransformUtility.Stretch(viewRectTransform);
            _view.Show();
        }

        public BaseView DetachView()
        {
            transform.DetachChildren();
            return _view;
        }
    }
}