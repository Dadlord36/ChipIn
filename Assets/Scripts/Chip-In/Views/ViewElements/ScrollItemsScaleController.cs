using UnityEngine;
using UnityEngine.EventSystems;

namespace Views.ViewElements
{
    public class ScrollItemsScaleController : UIBehaviour
    {
        private Transform[] _contentItems;
        private float _startX, _endX;
        private Vector2 _scrollableAreaCenter;
        private Vector3 _tempItemScale;

        protected override void Awake()
        {
            base.Awake();
            var objectTransform = transform;

            _contentItems = new Transform[objectTransform.childCount];

            for (int i = 0; i < objectTransform.childCount; i++)
            {
                _contentItems[i] = objectTransform.GetChild(i);
            }

            var rectTransform = transform as RectTransform;
            var rect = rectTransform.rect;

            _scrollableAreaCenter = rectTransform.position;

            _startX = 0f;
            _endX = rect.width;
        }

        public void UpdateItemsScale()
        {
            for (int i = 0; i < _contentItems.Length; i++)
            {
                var percentage = Mathf.Clamp01(GetItemPathPercentage(_contentItems[i]));

                Debug.Log(percentage.ToString());

                _tempItemScale.Set(percentage, percentage, percentage);
                _contentItems[i].localScale = _tempItemScale;
            }
        }

        private float GetItemPathPercentage(Transform item)
        {
            float GetControlParameter()
            {
                return item.TransformPoint(_scrollableAreaCenter).x;
            }

            var point = GetControlParameter();
            return point / _endX;
        }
    }
}