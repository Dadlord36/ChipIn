using System.Collections.Generic;
using ScriptableObjects.Parameters;
using UnityEngine;
using Utilities;

namespace Controllers.SlotsSpinningControllers
{
    public interface IProgressiveMovement
    {
        void ProgressMovementAlongPath(in float pathDelta);
        LineEngineParameters MovementParameters { get; set; }
    }


    public class LineEngineBehaviour : MonoBehaviour, IProgressiveMovement
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

        #region Serialized Properties

        [SerializeField] private LineEngineParameters movementParameters;

        #endregion


        public float CoveredPathPercentage { get; private set; }
        public RectTransform ContainerRoot => transform as RectTransform;

        private int ChildCount => ContainerRoot.childCount;

        public uint IndexOfItemToFocusOn { get; set; }

        public bool ShouldControlSiblingIndexes { get; set; }

        public float WholePathLength { get; set; }

        public float LapLength { get; set; }

        public float ItemLength => _lineEngine.ItemLength;
        public float ItemStepOnWholePercentage => _lineEngine.ItemStep;

        public LineEngineParameters MovementParameters
        {
            get => movementParameters;
            set => movementParameters = value;
        }

        private PathMovingObject[] _pathMovingObjects;
        private readonly LineEngine _lineEngine = new LineEngine();


        public void Initialize()
        {
            Initialize((uint) ChildCount, (uint) (ChildCount * MovementParameters.Laps));
        }

        public Transform[] Initialize(Transform slotPrefab, uint lapItemsNumber)
        {
            return Initialize(slotPrefab, lapItemsNumber, (uint) (ChildCount * MovementParameters.Laps));
        }

        public Transform[] Initialize(Transform slotPrefab, uint lapItemsNumber, uint allItemsNumber)
        {
            var children = CreateChildren(slotPrefab, lapItemsNumber);
            Initialize(lapItemsNumber, allItemsNumber);
            return children;
        }

        public void Initialize(uint lapItemsNumber, uint allItemsNumber)
        {
            float CalculatePathLength(uint itemsAmount)
            {
                return itemsAmount * MovementParameters.ItemLength /*+ (itemsAmount - 1) * MovementParameters.OffsetBetweenItems*/;
            }

            LapLength = CalculatePathLength(lapItemsNumber);
            WholePathLength = CalculatePathLength(allItemsNumber);

            ResetLineEngine();
            var children = new Transform[ChildCount];

            for (var i = 0; i < ChildCount; i++)
            {
                children[i] = ContainerRoot.GetChild(i);
            }

            _pathMovingObjects = CreatePathMovingObjectsForChildren(children);
            AlignItems();
        }

        #region Public functions

        private Transform[] CreateChildren(Transform slotPrefab, uint elementsNumber)
        {
            ClearItems();
            var children = new Transform [elementsNumber];
            for (int i = 0; i < elementsNumber; i++)
            {
                children[i] = Instantiate(slotPrefab, ContainerRoot);
            }

            return children;
        }

        private void ResetLineEngine()
        {
            _lineEngine.RecalculatePathSizes(LapLength, WholePathLength, MovementParameters);
        }

        public void AlignItems()
        {
            ResetLineEngine();
            for (int i = 0; i < ChildCount; i++)
            {
                var position = _lineEngine.CalculateAnglePositionFromDistance(_lineEngine.ItemLength * i);
                var childRectTransform = (RectTransform) transform.GetChild(i).transform;

                childRectTransform.pivot = MovementParameters.AnchorPivot.pivot;
                childRectTransform.anchorMin = MovementParameters.AnchorPivot.anchorMin;
                childRectTransform.anchorMax = MovementParameters.AnchorPivot.anchorMax;
                transform.GetChild(i).localPosition = position;
            }
        }

        private void AdjustMovingObjectsPositionOnPathFromPathPercentage(float wholePathPercentage)
        {
            float ProgressMovementAlongPath(PathMovingObject movingObject)
            {
                var lapPart = _lineEngine.CalculateItemLapPartFromWholePathPercentage(wholePathPercentage,
                    movingObject.InitialNumberInOrder);
                movingObject.LocalPosition = _lineEngine.CalculateAnglePositionFromDistance(lapPart);
                return lapPart;
            }

            void ProgressMovementAndSiblingIndex(PathMovingObject movingObject)
            {
                /*var lapPart = _lineEngine.CalculateItemLapPartFromWholePathPercentage(
                    Mathf.Abs(wholePathPercentage), movingObject.InitialNumberInOrder);*/
                var lapPart = ProgressMovementAlongPath(movingObject);
                // movingObject.LocalPosition = _lineEngine.CalculateAnglePositionFromDistance(lapPart);
                movingObject.SetSiblingIndex(CalculateItemSiblingIndex(lapPart));
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

            CoveredPathPercentage = wholePathPercentage;
        }

        private int CalculateItemSiblingIndex(float lapPart)
        {
            return Mathf.FloorToInt(lapPart / _lineEngine.ItemsLapStep);
        }

        public void SlideInstantlyToIndexPosition(uint index)
        {
            IndexOfItemToFocusOn = index;
            ResetLineEngine();
            AdjustMovingObjectsPositionOnPathFromPathPercentage(1f);
        }

        public void AdjustMovingObjectsPositionOnPathFromWholePathPart(in float pathPartLength)
        {
            AdjustMovingObjectsPositionOnPathFromPathPercentage(_lineEngine.CalculateWholePathPercentageFromWholePathPartLength(pathPartLength));
        }

        public void AdjustReverseMovingObjectsPositionOnPathFromWholePathPart(in float pathPartLength)
        {
            AdjustMovingObjectsPositionOnPathFromPathPercentage(Mathf.Clamp01(
                1 - _lineEngine.CalculateWholePathPercentageFromWholePathPartLength(pathPartLength)));
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

        private void ClearItems()
        {
            GameObjectsUtility.DestroyTransformAttachments(ContainerRoot);
        }
    }
}