using UnityEngine;
using UnityEngine.EventSystems;

namespace Controllers
{
    public class PathMovingObject
    {
        private readonly Transform _movingObject;
        private float _percentageOnPath;

        public float PercentageOnPath => _percentageOnPath;
        public Vector3 Position => _movingObject.position;

        public PathMovingObject(Transform movingObject, float initialRelativePathPercentage)
        {
            _movingObject = movingObject;
            _percentageOnPath = initialRelativePathPercentage;
        }


        public void SetPositionAndAdjustPathPercentage(in Vector3 position, float pathPercentage)
        {
            _movingObject.position = position;
            _percentageOnPath = pathPercentage;
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
        [SerializeField] private Vector2 itemSize;
        [Range(0f, 360f)] [SerializeField] private float movementAngle;

        [SerializeField] private BoxCollider2D boundingCollider;

        #endregion


        private PathMovingObject[] _pathMovingObjects;
        private RectTransform _thisTransform;
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
            Debug.Assert(_thisTransform != null, nameof(_thisTransform) + " != null");

            PathMovingObject[] CreatePathMovingObjectsForChildren()
            {
                var childCount = _thisTransform.childCount;
                var pathMovingObjects = new PathMovingObject[childCount];


                for (int i = 0; i < _thisTransform.childCount; i++)
                {
                    var progress = _itemsStep * i;
                    pathMovingObjects[i] = new PathMovingObject(_thisTransform.GetChild(i), progress);
                }

                return pathMovingObjects;
            }

            _pathMovingObjects = CreatePathMovingObjectsForChildren();
        }

        private float CalculateSingleItemLength()
        {
            return offset + itemSize.x;
        }

        private float CalculateLapLength()
        {
            return _thisTransform.childCount * CalculateSingleItemLength();
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
            _thisTransform = transform as RectTransform;
            _itemLength = CalculateSingleItemLength();

            void CalculateLapAndWholeLengths()
            {
                _lapLength = CalculateLapLength();
                _wholePathLength = CalculateWholePathLength();
            }

            void CalculateBorderPoints()
            {
                var center = transform.position;

                var wholePathHalfLength = _wholePathLength / 2;
                var lapLengthHalfLength = _lapLength / 2;

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

            for (int i = 0; i < _thisTransform.childCount; i++)
            {
                transform.GetChild(i).position = AdjustPositionWithAngle(_itemLength * i);
            }
        }

        private Vector2 AdjustPositionWithAngle(float distance)
        {
            return MoveOnDistanceAlongAngle(_lapEndPoint, distance, movementAngle);
            ;
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

                if (boundingCollider.OverlapPoint(movingObject.Position))
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