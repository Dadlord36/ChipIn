using System;
using ScriptableObjects.SwitchBindings;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using Utilities;

namespace ViewModels.UI
{
    public class ScrollViewController : UIBehaviour
    {
        public event Action DoneScrolling;

        [SerializeField] private AnimationCurve speedCurve;
        [SerializeField, Space(10)] private float scrollDuration;
        [SerializeField] private float minScrollSpeed;
        [SerializeField] private float maxScrollSpeed;
        [SerializeField, Space(10)] private RectTransform crateTransform;
        [SerializeField, Space(10)] private RectTransform rightDestinationPointTransform;


        private float _movementDistance;
        private Vector2 _centerDestinationPoint, _leftDestinationPoint, _rightDestinationPoint;

        private Action<float> _crateMovementFunction;

        private Action<float> _movingToLeft;
        private Action<float> _movingToRight;
        private Action<float> _movingFromLeft;
        private Action<float> _movingFromRight;

        private void InitializeMovementFunctionalityFunctionsDelegates()
        {
            _movingToLeft = delegate(float progress)
            {
                CratePosition = Vector2.Lerp(_centerDestinationPoint, _leftDestinationPoint, progress);
            };
            _movingToRight = delegate(float progress)
            {
                CratePosition = Vector2.Lerp(_centerDestinationPoint, _rightDestinationPoint, progress);
            };

            _movingFromLeft = delegate(float progress)
            {
                CratePosition = Vector2.Lerp(_leftDestinationPoint, _centerDestinationPoint, progress);
            };
            _movingFromRight = delegate(float progress)
            {
                CratePosition = Vector2.Lerp(_rightDestinationPoint, _centerDestinationPoint, progress);
            };
        }

        protected override void Awake()
        {
            base.Awake();
            InitializeMovementFunctionalityFunctionsDelegates();
            SetMovementDistanceAndDestinations();
            enabled = false;
        }

        private void SetMovementDistanceAndDestinations()
        {
            _centerDestinationPoint = GetComponent<RectTransform>().anchoredPosition;
            
             _movementDistance = ((RectTransform) transform).rect.width;
             _leftDestinationPoint = _rightDestinationPoint = _centerDestinationPoint;
            _leftDestinationPoint.x -= _movementDistance;
            _rightDestinationPoint.x += _movementDistance;
        }


        private Vector2 CratePosition
        {
            get => crateTransform.anchoredPosition;
            set => crateTransform.anchoredPosition = value;
        }

        protected override void Start()
        {
            base.Start();
            Assert.IsNotNull(crateTransform);
        }

        public void BeginScrollForward()
        {
            ResetCounters();
            enabled = true;
        }

        private enum MovementType
        {
            ToLeft,
            ToRight,
            FromLeft,
            FromRight
        }

        private void SelectCorrespondingMovementFunction(MovementType movementType)
        {
            BeginScrollForward();
            switch (movementType)
            {
                case MovementType.ToLeft:
                    _crateMovementFunction = _movingToLeft;
                    CratePosition = _centerDestinationPoint;
                    return;
                case MovementType.ToRight:
                    _crateMovementFunction = _movingToRight;
                    CratePosition = _centerDestinationPoint;
                    return;
                case MovementType.FromLeft:
                    _crateMovementFunction = _movingFromLeft;
                    CratePosition = _leftDestinationPoint;
                    return;
                case MovementType.FromRight:
                    _crateMovementFunction = _movingFromRight;
                    CratePosition = _rightDestinationPoint;
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(movementType), movementType, null);
            }
        }

        private float _currentTime, _pathPercentage;
        private float _speed;

        private void Update()
        {
            _pathPercentage = _currentTime / scrollDuration;
            _speed = Mathf.Lerp(minScrollSpeed, maxScrollSpeed, speedCurve.Evaluate(_pathPercentage));
            _currentTime += Time.deltaTime * _speed;

            OnScrollProgress();

            if (_pathPercentage >= 1.0f)
            {
                StopScrolling();
            }
        }

        public void BeginCrateMovement(ViewsSwitchData.AppearingSide appearingSide)
        {
            switch (appearingSide)
            {
                case ViewsSwitchData.AppearingSide.FromLeft:
                    SelectCorrespondingMovementFunction(MovementType.FromLeft);
                    return;
                case ViewsSwitchData.AppearingSide.FromRight:
                    SelectCorrespondingMovementFunction(MovementType.FromRight);
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(appearingSide), appearingSide, null);
            }
        }

        private void OnScrollProgress()
        {
            _crateMovementFunction(_pathPercentage);
        }

        private void ResetCounters()
        {
            _pathPercentage = _currentTime = 0.0f;
        }

        public void StopScrolling()
        {
            enabled = false;
            OnDoneScrolling();
        }

        protected void OnDoneScrolling()
        {
            DoneScrolling?.Invoke();
        }
    }
}