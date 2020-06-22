using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Common.UnityEvents;
using InputDetection;
using ScriptableObjects.Parameters;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI.Extensions;
using UnityEngine.UIElements;
using Utilities;

namespace Controllers.SlotsSpinningControllers.RecyclerView
{
    [RequireComponent(typeof(LineEngineBehaviour))]
    public class RecyclerView : MonoBehaviour
    {
        private const string Tag = nameof(RecyclerView);
        public FloatUnityEvent coveredPathPercentageValueChanged;
        public IntUnityEvent onItemsBeingSwiped;

        [SerializeField] private SliderDirection scrollDirection;
        [SerializeField] private bool swipeInReverse;

        private LineEngineBehaviour _lineEngineBehaviour;

        private float _coveredDistancePercentage;
        private float _coveredDistance;
        private uint _itemsBeingSwiped;

        #region Properties

        private RectTransform ItemsContainerRoot => _lineEngineBehaviour.ContainerRoot;

        private bool ShouldScroll { get; set; }


        private uint ItemsBingSwiped
        {
            get => _itemsBeingSwiped;
            set
            {
                if (value == _itemsBeingSwiped) return;
                //Calculates difference between previous swiped items and new to determine changed amount and direction     
                OnItemsBeingSwiped((int) value - (int) _itemsBeingSwiped);
                _itemsBeingSwiped = value;
            }
        }

        private float CoveredDistancePercentage
        {
            get => _coveredDistancePercentage;
            set
            {
                if (value.Equals(_coveredDistancePercentage)) return;
                _coveredDistancePercentage = Mathf.Clamp01(value);
                ItemsBingSwiped = (uint) (_coveredDistancePercentage /_lineEngineBehaviour.ItemStepOnWholePercentage);
                OnCoveredPathPercentageValueChanged();
            }
        }

        private LineEngineParameters MovementParameters => _lineEngineBehaviour.MovementParameters;

        private float CoveredDistance
        {
            get => _coveredDistance;
            set
            {
                if (value.Equals(_coveredDistance)) return;
                _coveredDistance = Mathf.Clamp(value, 0, _lineEngineBehaviour.WholePathLength);
            }
        }

        #endregion

        #region Public functions

        public void AlignItems()
        {
            Assert.IsTrue(TryGetComponent(out _lineEngineBehaviour));
            _lineEngineBehaviour.AlignItems();
        }

        #endregion

        private void OnEnable()
        {
            if (TryGetComponent(out IPaginatedRepositoryRecyclerViewAdapter adapter))
            {
                onItemsBeingSwiped.AddListener(adapter.OnSwiped);
            }
        }

        private void OnDisable()
        {
            onItemsBeingSwiped.RemoveAllListeners();
        }

        public async Task Initialize()
        {
            Assert.IsTrue(TryGetComponent(out _lineEngineBehaviour));
            _lineEngineBehaviour.Initialize();
            // _lineEngineBehaviour.ShouldControlSiblingIndexes = true;

            try
            {
                if (TryGetComponent(out IPaginatedRepositoryRecyclerViewAdapter adapter))
                {
                    var visibleItemsCount = CalculateVisibleItemsCount() + 2;
                    var result = await adapter.Initialize(visibleItemsCount);
                    ShouldScroll = true;
                    _lineEngineBehaviour.Initialize(result.VisibleItemsNumber, result.TotalItemsInRepository);
                }
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }
        }


        private uint CalculateVisibleItemsCount()
        {
            var rect = ItemsContainerRoot.rect;
            switch (scrollDirection)
            {
                case SliderDirection.Horizontal:
                    return (uint) (rect.width / (MovementParameters.ItemLength + MovementParameters.OffsetBetweenItems));
                case SliderDirection.Vertical:
                    return (uint) (rect.height / (MovementParameters.ItemLength + MovementParameters.OffsetBetweenItems));
            }

            throw new InvalidEnumArgumentException(nameof(SliderDirection), (int) scrollDirection,
                typeof(ScrollSnap.ScrollDirection));
        }

        public void Scroll(SwipeDetector.SwipeData swipeData)
        {
            if (!ShouldScroll) return;

            switch (scrollDirection)
            {
                case SliderDirection.Horizontal:
                    CoveredDistance += swipeData.TouchDelta.x;
                    break;
                case SliderDirection.Vertical:
                    CoveredDistance += swipeData.TouchDelta.y;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // CoveredDistancePercentage = _lineEngineBehaviour.AdjustMovingObjectsPositionOnPathFromWholePathPart(CoveredDistance);
            _lineEngineBehaviour.AdjustReverseMovingObjectsPositionOnPathFromWholePathPart(CoveredDistance);
            CoveredDistancePercentage = 1 - _lineEngineBehaviour.CoveredPathPercentage;
        }

        private void OnCoveredPathPercentageValueChanged()
        {
            coveredPathPercentageValueChanged.Invoke(_coveredDistancePercentage);
        }

        private void OnItemsBeingSwiped(int itemsAmount)
        {
            onItemsBeingSwiped.Invoke(itemsAmount);
        }
    }

    public readonly struct AdapterInitializationResult
    {
        public readonly bool EnoughItemsToScroll;
        public readonly uint TotalItemsInRepository;
        public readonly uint VisibleItemsNumber;

        public AdapterInitializationResult(uint totalItemsInRepository, uint visibleItemsNumber,bool enoughItemsToScroll)
        {
            TotalItemsInRepository = totalItemsInRepository;
            EnoughItemsToScroll = enoughItemsToScroll;
            VisibleItemsNumber = visibleItemsNumber;
        }
    }
}