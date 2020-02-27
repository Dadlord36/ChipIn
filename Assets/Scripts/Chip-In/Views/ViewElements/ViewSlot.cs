using UnityEngine;
using UnityEngine.EventSystems;
using RectTransformUtility = Utilities.RectTransformUtility;

namespace Views.ViewElements
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Canvas))]
    public class ViewSlot : UIBehaviour
    {
        public bool Occupied => _view != null;
        private BaseView _view;

        public BaseView ViewInSlot => _view;

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
                var canvas = GetComponent<Canvas>();
                canvas.overrideSorting = true;
                canvas.sortingOrder = value;
            }
            get => GetComponent<Canvas>().sortingOrder;
        }

        public void ResetTransform()
        {
            ResetTransform(transform as RectTransform);
        }

        private static void ResetTransform(RectTransform rectTransform)
        {
            RectTransformUtility.ResetSize(rectTransform);
            RectTransformUtility.ResetScale(rectTransform);
        }

        public void AttachView(BaseView view)
        {
            _view = view;
            FormatView();
            _view.Show();
        }

        private void FormatView()
        {
            var viewRectTransform = _view.ViewRootRectTransform;
            viewRectTransform.SetParent(transform);
            ResetTransform(viewRectTransform);
            RectTransformUtility.Stretch(viewRectTransform);
        }

        public BaseView DetachView()
        {
            transform.DetachChildren();
            return _view;
        }
    }
}