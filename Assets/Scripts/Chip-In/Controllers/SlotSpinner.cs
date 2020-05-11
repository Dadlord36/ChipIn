using UnityEngine;
using UnityEngine.EventSystems;

namespace Controllers
{
    public class PathMovingObject
    {
        private readonly Transform _movingObject;

        public float PercentageOnPath { get; private set; }

        public Vector3 LocalPosition
        {
            get => _movingObject.localPosition;
            private set => _movingObject.localPosition = value;
        }

        public Vector3 WorldPosition => _movingObject.position;

        public PathMovingObject(Transform movingObject, float initialRelativePathPercentage)
        {
            _movingObject = movingObject;
            PercentageOnPath = initialRelativePathPercentage;
        }

        public void SetPositionAndAdjustPathPercentage(in Vector3 position, float pathPercentage)
        {
            LocalPosition = position;
            PercentageOnPath = pathPercentage;
        }

        public void Deactivate()
        {
            _movingObject.gameObject.SetActive(false);
        }

        public void Activate()
        {
            _movingObject.gameObject.SetActive(true);
        }
    }

    public class SlotSpinner : UIBehaviour
    {
        #region Serialized Fields

        [SerializeField] private float spinTime;
        [SerializeField] private float offset;

        [SerializeField] private uint laps = 1;
        [SerializeField] private uint itemToFocusIndex;
        [SerializeField] private uint controlItemIndex;

        [SerializeField] private AnimationCurve speedCurve;
        [Range(0f, 360f)] [SerializeField] private float movementAngle;

        [SerializeField] private BoxCollider2D boundingCollider;

        #endregion


        private PathMovingObject[] _pathMovingObjects;
        private Vector3 _lapStartPoint, _lapEndPoint;
        private Vector3 _wholePathStartPoint, _wholePathEndPoint;

        private float _wholePathLength;
        private float _lapLength;

        private int ChildCount => transform.childCount;

        protected override void Start()
        {
            base.Start();
            Stop();
        }

        public void StartSpinning()
        {
            ResetParameters();
            enabled = true;
        }

        private void ResetParameters()
        {
            Stop();
            CalculateMainParameters();
            Initialize();
        }

        public void Initialize()
        {
            CalculateMainParameters();

            PathMovingObject[] CreatePathMovingObjectsForChildren()
            {
                var pathMovingObjects = new PathMovingObject[ChildCount];
                var thisTransform = transform;

                for (int i = 0; i < thisTransform.childCount; i++)
                {
                    var progress = _itemsStep * i;
                    pathMovingObjects[i] = new PathMovingObject(thisTransform.GetChild(i), progress);
                }

                return pathMovingObjects;
            }

            _pathMovingObjects = CreatePathMovingObjectsForChildren();
        }

        private float CalculateLapLength()
        {
            return ChildCount * offset;
        }

        private float CalculateWholePathLength()
        {
            return _itemLength * controlItemIndex + laps * CalculateLapLength() + _itemLength * (ChildCount - itemToFocusIndex);
        }

        private void Stop()
        {
            enabled = false;
            _previousFrameDistancePercentage = _currentFrameDistancePercentage = 0f;
            _passedTime = 0f;
        }

        private void Update()
        {
            SpinUpdate();
        }

        private void CalculateMainParameters()
        {
            _itemLength = offset;

            void CalculateLapAndWholeLengths()
            {
                _lapLength = CalculateLapLength();
                _wholePathLength = CalculateWholePathLength();
            }

            void CalculateBorderPoints()
            {
                var center = Vector3.zero;

                _lapEndPoint = _lapStartPoint = center;

                _wholePathStartPoint.x = center.x - _wholePathLength;
                _wholePathEndPoint.x = center.x;

                _lapStartPoint.x = center.x - _lapLength;
                _lapEndPoint.x = center.x;
            }

            CalculateLapAndWholeLengths();
            CalculateBorderPoints();

            _itemsStep = CalculateWholePathPercentageFromWholePathPartLength(_itemLength);
        }

        public void AlignItems()
        {
            CalculateMainParameters();
            for (int i = 0; i < ChildCount; i++)
            {
                var position = AdjustPositionWithAngle(_itemLength * i);
                var childRectTransform = transform.GetChild(i).transform as RectTransform;
                childRectTransform.pivot = childRectTransform.anchorMin = childRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                transform.GetChild(i).localPosition = position;
            }
        }

        private Vector2 AdjustPositionWithAngle(float distance)
        {
            return MoveOnDistanceAlongAngle(_lapEndPoint, distance, movementAngle);
        }


        private float _previousFrameDistancePercentage;
        private float _currentFrameDistancePercentage;
        private float _passedTime;
        private float _itemsStep;
        private float _itemLength;


        #region Path fallowing related calculation functions

        private float CalculateWholePathPartFromWholePathPercentage(in float percentage)
        {
            return Mathf.LerpUnclamped(0, _wholePathLength, percentage);
        }

        private float CalculateLapPartFromWholePathPercentage(in float wholePathPercentage)
        {
            return Mathf.Repeat(CalculateWholePathPartFromWholePathPercentage(wholePathPercentage), _lapLength);
        }

        private float CalculateWholePathPercentageFromWholePathPartLength(in float wholePathPartLength)
        {
            return wholePathPartLength / _wholePathLength;
        }

        private float CalculateLapPercentageFromLapPartLength(in float lapPartLength)
        {
            return lapPartLength / _lapLength;
        }

        private float CalculateLapPercentageFromWholePathPercentage(in float wholePathPercentage)
        {
            return CalculateLapPercentageFromLapPartLength(CalculateLapPartFromWholePathPercentage(wholePathPercentage));
        }

        private Vector3 GetPositionOnLap(in float percentage)
        {
            return Vector3.Lerp(_lapStartPoint, _lapEndPoint, percentage);
        }

        private Vector3 GetPositionOnWholePath(in float percentage)
        {
            return Vector3.Lerp(_wholePathStartPoint, _wholePathEndPoint, percentage);
        }

        #endregion

        private void AdjustMovingObjectsPositionOnPathFromPathPercentage(in float passedFragmentPercentage)
        {
            void ProgressMovementAlongPath(PathMovingObject movingObject, in float fragmentPercentage)
            {
                var progress = movingObject.PercentageOnPath + fragmentPercentage;

                movingObject.SetPositionAndAdjustPathPercentage(AdjustPositionWithAngle(
                    CalculateLapPartFromWholePathPercentage(progress)), progress);

                if (!boundingCollider.enabled) return;

                if (boundingCollider.OverlapPoint(movingObject.WorldPosition))
                {
                    movingObject.Activate();
                }
                else
                {
                    movingObject.Deactivate();
                }
            }

            for (int i = 0; i < _pathMovingObjects.Length; i++)
            {
                ProgressMovementAlongPath(_pathMovingObjects[i], passedFragmentPercentage);
            }
        }

        private void SpinUpdate()
        {
            if (_previousFrameDistancePercentage >= 1f)
            {
                Stop();
            }

            _currentFrameDistancePercentage = Mathf.InverseLerp(0, spinTime, _passedTime);

            AdjustMovingObjectsPositionOnPathFromPathPercentage(_currentFrameDistancePercentage - _previousFrameDistancePercentage);

            _passedTime += Time.deltaTime * speedCurve.Evaluate(_currentFrameDistancePercentage);
            _previousFrameDistancePercentage = _currentFrameDistancePercentage;
        }

        private static Vector2 MoveOnDistanceAlongAngle(in Vector2 center, in float distance, in float angle)
        {
            var rad = angle * Mathf.Deg2Rad;
            return new Vector2(center.x + Mathf.Cos(rad) * distance, center.y + Mathf.Sin(rad) * distance);
        }
    }
}