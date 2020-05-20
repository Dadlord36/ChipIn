using System.Collections.Generic;
using ScriptableObjects.Parameters;
using UnityEngine;
using Utilities;

namespace Controllers.SlotsSpinningControllers
{
    public class LineEngine : MonoBehaviour
    {
        public class PathMovingObject
        {
            private readonly Transform _movingObject;

            public float PercentageOnPath { get; set; }

            public Vector3 LocalPosition
            {
                get => _movingObject.localPosition;
                private set => _movingObject.localPosition = value;
            }

            public Vector3 WorldPosition => _movingObject.position;

            public PathMovingObject(Transform movingObject)
            {
                _movingObject = movingObject;
            }

            public void SetPositionAndAdjustPathPercentage(in Vector3 position, float pathPercentage)
            {
                LocalPosition = position;
                PercentageOnPath = pathPercentage;
            }

            /*public void ResetPathPercentage()
            {
                PercentageOnPath = InitialPercentageOnPath;
            }*/
        }


        private PathMovingObject[] _pathMovingObjects;
        private Vector3 _lapStartPoint, _lapEndPoint;
        private Vector3 _wholePathStartPoint, _wholePathEndPoint;

        private float _wholePathLength;
        private float _lapLength;

        private int ChildCount => RootTransform.childCount;
        private Transform RootTransform => transform;

        public uint ItemToFocusOnIndex { get; set; }
        public SlotSpinnerProperties SlotSpinnerProperties { get; set; }


        public void ResetParameters()
        {
            RecalculateInitialPositionsForCurrentWholePath();
        }

        public Transform[] Initialize(Transform slotPrefab, int elementsNumber)
        {
            ClearItems();
            var children = new Transform [elementsNumber];

            for (int i = 0; i < elementsNumber; i++)
            {
                children[i] = Instantiate(slotPrefab, RootTransform);
            }

            Initialize();
            return children;
        }

        public void Initialize()
        {
            var children = new Transform[ChildCount];

            for (int i = 0; i < ChildCount; i++)
            {
                children[i] = RootTransform.GetChild(i);
            }

            _pathMovingObjects = CreatePathMovingObjectsForChildren(children);
            RecalculateInitialPositionsForCurrentWholePath();
            AlignItems();
        }

        private static PathMovingObject[] CreatePathMovingObjectsForChildren(IReadOnlyList<Transform> items)
        {
            var pathMovingObjects = new PathMovingObject[items.Count];

            for (int i = 0; i < items.Count; i++)
            {
                pathMovingObjects[i] = new PathMovingObject(items[i]);
            }

            return pathMovingObjects;
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
            AdjustMovingObjectsPositionOnPathFromPathPercentage(1f);
        }


        private Vector2 AdjustPositionWithAngle(float distance)
        {
            return MoveOnDistanceAlongAngle(_lapEndPoint, distance, SlotSpinnerProperties.MovementAngle);
        }
        

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
        }

        private void MoveObjectPositionOnPathFromPathPercentage(in float passedFragmentPercentage)
        {
            for (int i = 0; i < _pathMovingObjects.Length; i++)
            {
                ProgressMovementAlongPath(_pathMovingObjects[i], passedFragmentPercentage);
            }
        }

        private void AdjustMovingObjectsPositionOnPathFromPathPercentage(in float passedFragmentPercentage)
        {
            for (int i = 0; i < _pathMovingObjects.Length; i++)
            {
                AdjustMovingObjectPosition(_pathMovingObjects[i], passedFragmentPercentage);
            }
        }

        public void UpdateProgress(float pathDelta)
        {
            AdjustMovingObjectsPositionOnPathFromPathPercentage(pathDelta);
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