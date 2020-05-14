using ScriptableObjects.Parameters;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;
using ViewModels.UI.Elements.Icons;

namespace Controllers.SlotsSpinningControllers
{
    public class SlotSpinner : UIBehaviour
    {
        public class PathMovingObject
        {
            private readonly Transform _movingObject;
            private readonly Image _image;

            public float PercentageOnPath { get; set; }

            public Vector3 LocalPosition
            {
                get => _movingObject.localPosition;
                private set => _movingObject.localPosition = value;
            }

            public Vector3 WorldPosition => _movingObject.position;

            public PathMovingObject(Transform movingObject, Image image)
            {
                _movingObject = movingObject;
                _image = image;
            }

            public void SetPositionAndAdjustPathPercentage(in Vector3 position, float pathPercentage)
            {
                LocalPosition = position;
                PercentageOnPath = pathPercentage;
            }

            public void Deactivate()
            {
                _image.enabled = false;
            }

            public void Activate()
            {
                _image.enabled = true;
            }

            /*public void ResetPathPercentage()
            {
                PercentageOnPath = InitialPercentageOnPath;
            }*/
        }


        private BoxCollider2D _boundingCollider;

        private PathMovingObject[] _pathMovingObjects;
        private Vector3 _lapStartPoint, _lapEndPoint;
        private Vector3 _wholePathStartPoint, _wholePathEndPoint;

        private float _wholePathLength;
        private float _lapLength;

        private int ChildCount => RootTransform.childCount;
        private Transform RootTransform => transform;

        public uint ItemToFocusOnIndex { get; set; }
        public SlotSpinnerProperties SlotSpinnerProperties { get; set; }

        protected override void OnEnable()
        {
            base.OnEnable();
            _boundingCollider = GetComponent<BoxCollider2D>();
        }


        public void StartSpinning()
        {
            ResetParameters();
            enabled = true;
        }

        private void ResetParameters()
        {
            Stop();
            RecalculateInitialPositionsForCurrentWholePath();
        }


        public GameSlotIconView[] Initialize(GameSlotIconView slotPrefab, int elementsNumber)
        {
            ClearItems();
            var spinningElements = new GameSlotIconView [elementsNumber];

            for (int i = 0; i < elementsNumber; i++)
            {
                spinningElements[i] = Instantiate(slotPrefab, RootTransform);
            }

            Initialize();
            return spinningElements;
        }

        public void Initialize()
        {
            PathMovingObject[] CreatePathMovingObjectsForChildren()
            {
                var pathMovingObjects = new PathMovingObject[ChildCount];
                var thisTransform = transform;

                for (int i = 0; i < thisTransform.childCount; i++)
                {
                    var child = thisTransform.GetChild(i);

                    pathMovingObjects[i] = new PathMovingObject(child, child.GetComponent<Image>());
                }

                return pathMovingObjects;
            }

            _pathMovingObjects = CreatePathMovingObjectsForChildren();

            RecalculateInitialPositionsForCurrentWholePath();
        }

        private void RecalculateInitialPositionsForCurrentWholePath()
        {
            CalculateMainParameters();
            for (int i = 0; i < _pathMovingObjects.Length; i++)
            {
                _pathMovingObjects[i].PercentageOnPath = _itemsStep * i;
            }
        }

        private float CalculateLapLength()
        {
            return ChildCount * SlotSpinnerProperties.Offset;
        }

        private float CalculateWholePathLength()
        {
            return _itemLength * SlotSpinnerProperties.ControlItemIndex +
                   SlotSpinnerProperties.Laps * CalculateLapLength() +
                   _itemLength * (ChildCount - ItemToFocusOnIndex);
        }


        public void Stop()
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
            _itemLength = SlotSpinnerProperties.Offset;

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
                childRectTransform.pivot = childRectTransform.anchorMin =
                    childRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                transform.GetChild(i).localPosition = position;
            }
        }

        public void SlideInstantlyToIndexPosition(uint index)
        {
            ItemToFocusOnIndex = index;
            RecalculateInitialPositionsForCurrentWholePath();
            AdjustMovingObjectsPositionOnPathFromPathPercentage(1f, true);
        }


        private Vector2 AdjustPositionWithAngle(float distance)
        {
            return MoveOnDistanceAlongAngle(_lapEndPoint, distance, SlotSpinnerProperties.MovementAngle);
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
            return CalculateLapPercentageFromLapPartLength(
                CalculateLapPartFromWholePathPercentage(wholePathPercentage));
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

        private void AdjustMovingObjectPosition(PathMovingObject movingObject, in float passedFragmentPercentage)
        {
            ProgressMovementAlongPath(movingObject, movingObject.PercentageOnPath + passedFragmentPercentage);
        }

        private void ProgressMovementAlongPath(PathMovingObject movingObject, in float wholePathPercentage)
        {
            movingObject.SetPositionAndAdjustPathPercentage(AdjustPositionWithAngle(
                CalculateLapPartFromWholePathPercentage(wholePathPercentage)), wholePathPercentage);

            if (_boundingCollider == null || !_boundingCollider.enabled) return;
            if (_boundingCollider.OverlapPoint(movingObject.WorldPosition))
            {
                movingObject.Activate();
            }
            else
            {
                movingObject.Deactivate();
            }
        }

        private void MoveObjectPositionOnPathFromPathPercentage(in float passedFragmentPercentage)
        {
            for (int i = 0; i < _pathMovingObjects.Length; i++)
            {
                ProgressMovementAlongPath(_pathMovingObjects[i], passedFragmentPercentage);
            }
        }

        private void AdjustMovingObjectsPositionOnPathFromPathPercentage(in float passedFragmentPercentage,
            bool fromInitialPosition = false)
        {
            for (int i = 0; i < _pathMovingObjects.Length; i++)
            {
                AdjustMovingObjectPosition(_pathMovingObjects[i], passedFragmentPercentage);
            }
        }

        private void SpinUpdate()
        {
            if (_previousFrameDistancePercentage >= 1f)
            {
                Stop();
            }

            _currentFrameDistancePercentage = Mathf.InverseLerp(0, SlotSpinnerProperties.SpinTime, _passedTime);

            AdjustMovingObjectsPositionOnPathFromPathPercentage(
                _currentFrameDistancePercentage - _previousFrameDistancePercentage);

            _passedTime += Time.deltaTime * SlotSpinnerProperties.SpeedCurve.Evaluate(_currentFrameDistancePercentage);
            _previousFrameDistancePercentage = _currentFrameDistancePercentage;
        }

        private static Vector2 MoveOnDistanceAlongAngle(in Vector2 center, in float distance, in float angle)
        {
            var rad = angle * Mathf.Deg2Rad;
            return new Vector2(center.x + Mathf.Cos(rad) * distance, center.y + Mathf.Sin(rad) * distance);
        }

        private void ClearItems()
        {
            GameObjectsUtility.DestroyTransformAttachments(RootTransform);
        }
    }
}