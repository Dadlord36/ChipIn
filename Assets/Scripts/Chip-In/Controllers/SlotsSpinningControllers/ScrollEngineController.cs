using System;
using ActionsTranslators;
using Common.UnityEvents;
using InputDetection;
using ScriptableObjects.Parameters;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using Utilities;

namespace Controllers.SlotsSpinningControllers
{
    [RequireComponent(typeof(LineEngine))]
    public class ScrollEngineController : MonoBehaviour
    {
        private const string Tag = nameof(ScrollEngineController);

        public FloatUnityEvent coveredPathPercentageValueChanged;
        public IntUnityEvent onItemsBeingSwiped;

        [SerializeField] private MainInputActionsTranslator swipeDetector;
        [SerializeField] private SliderDirection scrollDirection;
        [SerializeField] private SlotSpinnerProperties properties;

        // private readonly ProgressiveMovement _progressiveMovement = new ProgressiveMovement();

        private LineEngine _lineEngine;

        private float _wholeDistance = 10000f;
        private float _coveredDistancePercentage;
        private float _coveredDistance;
        private uint _itemsBeingSwiped;
        private uint _itemsOnFullPath = 50;

        #region Properties

        public uint ItemsOnFullPath
        {
            get => _itemsOnFullPath;
            set
            {
                _itemsOnFullPath = value;
                _wholeDistance = _itemsOnFullPath * properties.Offset;
            }
        }

        private uint ItemsBingSwiped
        {
            get => _itemsBeingSwiped;
            set
            {
                if (value == _itemsBeingSwiped) return;
                OnItemsBeingSwiped((int) value - (int) _itemsBeingSwiped);
                _itemsBeingSwiped = value;
            }
        }

        private float CoveredDistancePercentage
        {
            get => _coveredDistancePercentage;
            set
            {
                if (value.Equals(_coveredDistance)) return;
                _coveredDistancePercentage = Mathf.Clamp01(value);
                ItemsBingSwiped = (uint) (_coveredDistancePercentage * _itemsOnFullPath);
                OnCoveredPathPercentageValueChanged();
            }
        }

        private float CoveredDistance
        {
            get => _coveredDistance;
            set
            {
                if (value.Equals(_coveredDistance)) return;

                _coveredDistance = Mathf.Clamp(value, 0, _wholeDistance);
            }
        }

        #endregion

        #region Public functions

        public void AlignItems()
        {
            Assert.IsTrue(TryGetComponent(out _lineEngine));
            _lineEngine.MovementParameters = properties;
            _lineEngine.AlignItems();
        }

        #endregion

        private void OnEnable()
        {
            Assert.IsTrue(TryGetComponent(out _lineEngine));
            _lineEngine.ShouldControlSiblingIndexes = true;
            SubscribeToInputEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromInputEvents();
        }

        private void SubscribeToInputEvents()
        {
            swipeDetector.Swiped += Scroll;
        }

        private void UnsubscribeFromInputEvents()
        {
            swipeDetector.Swiped -= Scroll;
        }


        private void Start()
        {
            _lineEngine.MovementParameters = properties;
            _lineEngine.Initialize();
            _wholeDistance = properties.Offset * _itemsOnFullPath;
        }

        /*void OnStop()
        {
            enabled = false;
        }

        void StartScrolling()
        {
            enabled = true;
        }*/


        private void Scroll(SwipeDetector.SwipeData swipeData)
        {
            // swipeData.DeltaVector *= -1;

            switch (scrollDirection)
            {
                case SliderDirection.Horizontal:

                    CoveredDistance += swipeData.DeltaVector.x;

                    break;
                case SliderDirection.Vertical:
                    CoveredDistance += swipeData.DeltaVector.y;

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            LogUtility.PrintLog(Tag, CoveredDistance.ToString());
            _lineEngine.AdjustMovingObjectsPositionOnPathFromWholePathPart(CoveredDistance);
            CoveredDistancePercentage = CoveredDistance / _wholeDistance;
        }

        private void OnCoveredPathPercentageValueChanged()
        {
            coveredPathPercentageValueChanged.Invoke(_coveredDistancePercentage);
        }

        private void OnItemsBeingSwiped(int itemsAmount)
        {
            onItemsBeingSwiped.Invoke(itemsAmount);
        }

        /*private void Update()
        {
            _movementProgress += 0.01f;
            _lineEngine.ProgressMovementAlongPath(_coveredDistancePercentage);
            _coveredDistancePercentage ;
        }*/


        /*private void vod()
        {
            _lineEngine.ShiftItemsAlongPathForPercentageOfWholePath(0f);
        }*/
    }
}