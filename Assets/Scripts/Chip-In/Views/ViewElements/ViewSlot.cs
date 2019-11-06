using UnityEngine;
using UnityEngine.EventSystems;
using RectTransformUtility = Utilities.RectTransformUtility;

namespace Views.ViewElements
{
    [RequireComponent(typeof(RectTransform))]
    public class ViewSlot : UIBehaviour
    {
        public bool Occupied => _view != null;
        private BaseView _view;

        protected override void Awake()
        {
            base.Awake();
            var rectTransform = GetComponent<RectTransform>();
            RectTransformUtility.Stretch(rectTransform);
            RectTransformUtility.ResetSize(rectTransform);
        }

        public void AttachView(BaseView view)
        {
            _view = view;
            var viewRectTransform = _view.ViewRootRectTransform;
            viewRectTransform.SetParent(transform);
            RectTransformUtility.Stretch(viewRectTransform);
            RectTransformUtility.ResetSize(viewRectTransform);
            RectTransformUtility.ResetScale(viewRectTransform);
        }



        public BaseView DetachView()
        {
            transform.DetachChildren();
            return _view;
        }
    }
}