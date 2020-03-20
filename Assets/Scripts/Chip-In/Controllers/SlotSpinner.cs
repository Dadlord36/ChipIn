using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controllers
{
    public class SlotSpinner : UIBehaviour
    {
        #region Serialized Fields

        [SerializeField] private float spinTime;
        [SerializeField] private float offset;
        [SerializeField] private int laps = 1;

        [SerializeField] private int itemToFocusIndex;

        [SerializeField] private AnimationCurve speedCurve;
        [SerializeField] private Transform center;
        [SerializeField] private Vector2 itemSize;
        [SerializeField] private Vector3 movementDirection;

        [SerializeField] private BoxCollider2D collider;

        #endregion

        private RectTransform[] _objectsToSpin;
        private Transform _targetItem;
        private RectTransform _thisTransform;
        private Vector3 _targetPosition;

        public SlotSpinner(Transform targetItem)
        {
            _targetItem = targetItem;
        }

        public void StartSpinning()
        {
            _targetPosition = _targetItem.position;
            enabled = true;
        }

        private bool _isInitialized;

        public void Initialize()
        {
            if(_isInitialized) return;
            _isInitialized = true;
            _thisTransform = transform as RectTransform;
            Debug.Assert(_thisTransform != null, nameof(_thisTransform) + " != null");
            var originalObjectsCount = _thisTransform.childCount;

            PrepareItems();

            var targetIndex = (laps - 1) * originalObjectsCount + itemToFocusIndex;
            _targetItem = _thisTransform.GetChild(targetIndex);
        }

        private void Stop()
        {
            enabled = false;
            _previousFrameDistancePercentage = _currentFrameDistancePercentage = 0f;
            _passedTime = 0f;
        }

        private void PrepareItems()
        {
            var collectedObjects = CollectItems();
            var objects = new List<RectTransform>(collectedObjects.Length * laps);
            objects.AddRange(collectedObjects);

            for (int i = 1; i < laps; i++)
            {
                objects.AddRange(CreateItemsSetCopy(collectedObjects));
            }

            _objectsToSpin = objects.ToArray();
            AlignItems();
        }

        private RectTransform[] CreateItemsSetCopy(IReadOnlyList<RectTransform> items)
        {
            var result = new RectTransform[items.Count];
            for (int i = 0; i < items.Count; i++)
            {
                result[i] = Instantiate(items[i], _thisTransform);
            }

            return result;
        }

        private RectTransform[] CollectItems()
        {
            var objects = new RectTransform[_thisTransform.childCount];
            {
                int index = 0;
                foreach (RectTransform child in _thisTransform)
                {
                    objects[index] = child;
                    index++;
                }
            }
            return objects;
        }

        private void Update()
        {
            SpinUpdate();
        }


        public void AlignItems()
        {
            var originalPosition = center.position;

            foreach (RectTransform child in transform)
            {
                child.position = originalPosition;
                var fullOffset = offset + itemSize.x;
                originalPosition += new Vector3(movementDirection.x * fullOffset, movementDirection.y * fullOffset);
            }
        }


        private float _previousFrameDistancePercentage;
        private float _currentFrameDistancePercentage;
        private float _passedTime;


        private Vector3 GetPathFragmentLength(float percentageInPreviousFrame, float percentageInCurrentFrame)
        {
            return GetPathPointFromPercentage(percentageInCurrentFrame) - GetPathPointFromPercentage(percentageInPreviousFrame);

            Vector3 GetPathPointFromPercentage(float percentage)
            {
                return Vector3.Lerp(center.position, _targetPosition, percentage);
            }
        }


        private void SpinUpdate()
        {
            _currentFrameDistancePercentage = Mathf.InverseLerp(0, spinTime, _passedTime);

            if (_previousFrameDistancePercentage >= 1f)
            {
                Stop();
            }

            var passedFragment = GetPathFragmentLength(_previousFrameDistancePercentage, _currentFrameDistancePercentage);

            passedFragment.x *= -movementDirection.x;
            passedFragment.y *= -movementDirection.y;
            passedFragment.z *= -movementDirection.z;

            for (int i = 0; i < _objectsToSpin.Length; i++)
            {
                _objectsToSpin[i].position += passedFragment;
                _objectsToSpin[i].gameObject.SetActive(collider.bounds.Contains(_objectsToSpin[i].position));
            }

            _passedTime += Time.deltaTime * speedCurve.Evaluate(_currentFrameDistancePercentage);
            _previousFrameDistancePercentage = _currentFrameDistancePercentage;
        }
    }
}