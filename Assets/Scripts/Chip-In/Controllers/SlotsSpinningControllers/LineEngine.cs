using System.Collections.Generic;
using ScriptableObjects.Parameters;
using UnityEngine;
using Utilities;

namespace Controllers.SlotsSpinningControllers
{
    public interface IProgressiveMovement
    {
        void ProgressMovementAlongPath(in float pathDelta);
        LineEngineProperties MovementParameters { get; set; }
    }

    public class LineEngine : MonoBehaviour, IProgressiveMovement
    {
        private class PathMovingObject
        {
            private readonly Transform _movingObject;
            public uint InitialNumberInOrder { get; }

            public Vector3 LocalPosition
            {
                get => _movingObject.localPosition;
                set => _movingObject.localPosition = value;
            }

            public Vector3 WorldPosition => _movingObject.position;

            public PathMovingObject(Transform movingObject, uint initialNumberInOrder)
            {
                _movingObject = movingObject;
                InitialNumberInOrder = initialNumberInOrder;
            }

            public void SetSiblingIndex(int i)
            {
                _movingObject.SetSiblingIndex(i);
            }
        }


        private PathMovingObject[] _pathMovingObjects;
        private Vector3 _lapStartPoint, _lapEndPoint;
        private Vector3 _wholePathStartPoint, _wholePathEndPoint;

        private float _wholePathLength;
        private float _lapLength;

        /// <summary>
        /// Length of item in world space
        /// </summary>
        private float _itemLength;

        /// <summary>
        /// Length of item relative to whole path in percentage equivalent
        /// </summary>
        private float _itemsStep;

        /// <summary>
        /// Length of item relative lap in percentage equivalent
        /// </summary>
        private float _itemsLapStep;

        private int ChildCount => ContainerRoot.childCount;
        public Transform ContainerRoot => transform;

        public uint ItemToFocusOnIndex { get; set; }
        /*public float CoveredPathPercentage { get; private set; }*/

        public bool ShouldControlSiblingIndexes { get; set; } = false;
        public LineEngineProperties MovementParameters { get; set; }


        public Transform[] Initialize(Transform slotPrefab, int elementsNumber)
        {
            ClearItems();
            var children = new Transform [elementsNumber];

            for (int i = 0; i < elementsNumber; i++)
            {
                children[i] = Instantiate(slotPrefab, ContainerRoot);
            }

            Initialize();
            return children;
        }

        #region Public functions

        public void Initialize()
        {
            CalculateMainParameters();
            var children = new Transform[ChildCount];

            for (int i = 0; i < ChildCount; i++)
            {
                children[i] = ContainerRoot.GetChild(i);
            }

            _pathMovingObjects = CreatePathMovingObjectsForChildren(children);
            AlignItems();
        }

        public void AlignItems()
        {
            CalculateMainParameters();
            for (int i = 0; i < ChildCount; i++)
            {
                var position = AdjustPositionFromGivenDistanceAndAngle(_itemLength * i);
                var childRectTransform = transform.GetChild(i).transform as RectTransform;

                /*Debug.Assert(childRectTransform != null, nameof(childRectTransform) + " != null");*/

                childRectTransform.pivot = MovementParameters.AnchorPivot.pivot;
                childRectTransform.anchorMin = MovementParameters.AnchorPivot.anchorMin;
                childRectTransform.anchorMax = MovementParameters.AnchorPivot.anchorMax;
                transform.GetChild(i).localPosition = position;
            }
        }

        public void SlideInstantlyToIndexPosition(uint index)
        {
            ItemToFocusOnIndex = index;
            CalculateMainParameters();
            AdjustMovingObjectsPositionOnPathFromPathPercentage(1f);
        }

        public void AdjustMovingObjectsPositionOnPathFromWholePathPart(in float pathPartLength)
        {
            AdjustMovingObjectsPositionOnPathFromPathPercentage(
                CalculateWholePathPercentageFromWholePathPartLength(pathPartLength)
            );
        }

        public void ProgressMovementAlongPath(in float wholePathPercentage)
        {
            AdjustMovingObjectsPositionOnPathFromPathPercentage(wholePathPercentage);
        }

        #endregion

        private static PathMovingObject[] CreatePathMovingObjectsForChildren(IReadOnlyList<Transform> items)
        {
            var pathMovingObjects = new PathMovingObject[items.Count];

            for (uint i = 0; i < items.Count; i++)
            {
                pathMovingObjects[i] = new PathMovingObject(items[(int) i], i);
            }

            return pathMovingObjects;
        }

        private float CalculateLapLength()
        {
            return ChildCount * MovementParameters.Offset;
        }

        private float CalculateWholePathLength()
        {
            return _itemLength * MovementParameters.ControlItemIndex + MovementParameters.Laps * CalculateLapLength() +
                   _itemLength * (ChildCount - ItemToFocusOnIndex);
        }


        private void CalculateMainParameters()
        {
            _itemLength = MovementParameters.Offset;

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
            _itemsLapStep = CalculateLapPartFromWholePathPercentage(_itemsStep);
        }

        private Vector2 AdjustPositionFromGivenDistanceAndAngle(float distance)
        {
            return MoveOnDistanceAlongAngle(_lapEndPoint, distance, MovementParameters.MovementAngle);
        }


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


        private void AdjustMovingObjectsPositionOnPathFromPathPercentage(float wholePathPercentage)
        {
            void ProgressMovementAlongPath(PathMovingObject movingObject)
            {
                var position = AdjustPositionFromGivenDistanceAndAngle(CalculateLapPartFromWholePathPercentage(
                    Mathf.Abs(wholePathPercentage) + _itemsStep * movingObject.InitialNumberInOrder)
                );
                movingObject.LocalPosition = position;
            }

            void ProgressMovementAndSiblingIndex(PathMovingObject movingObject)
            {
                var pathPart = CalculateLapPartFromWholePathPercentage(
                    Mathf.Abs(wholePathPercentage) + _itemsStep * movingObject.InitialNumberInOrder);
                
                movingObject.LocalPosition = AdjustPositionFromGivenDistanceAndAngle(pathPart);
                movingObject.SetSiblingIndex(Mathf.FloorToInt(pathPart / _itemsLapStep));
            }

            if (ShouldControlSiblingIndexes)
            {
                for (int i = 0; i < _pathMovingObjects.Length; i++)
                {
                    ProgressMovementAndSiblingIndex(_pathMovingObjects[i]);
                }
            }
            else
            {
                for (int i = 0; i < _pathMovingObjects.Length; i++)
                {
                    ProgressMovementAlongPath(_pathMovingObjects[i]);
                }
            }

            // CoveredPathPercentage = wholePathPercentage;
        }

        private static Vector2 MoveOnDistanceAlongAngle(in Vector2 center, in float distance, in float angle)
        {
            var rad = angle * Mathf.Deg2Rad;
            return new Vector2(center.x + Mathf.Cos(rad) * distance, center.y + Mathf.Sin(rad) * distance);
        }

        private void ClearItems()
        {
            GameObjectsUtility.DestroyTransformAttachments(ContainerRoot);
        }
    }
}