using System;
using ActionsTranslators;
using ScriptableObjects.Parameters;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace InputDetection
{
    public sealed class SwipeDetector : IUpdatable
    {
        public event Action<SwipeData> Swiped;

        private Vector2 _touchDownPosition, _touchUpPosition;
        private SwipeDetectorParameters _parameters;

        public SwipeDetector(SwipeDetectorParameters parameters)
        {
            _parameters = parameters;
        }

        public void Update()
        {
            var touches = Input.touches;

            if (touches[0].phase == TouchPhase.Began)
            {
                _touchDownPosition = _touchUpPosition = touches[0].position;
            }

            if (!_parameters.DetectSwipeOnlyAfterRelease && touches[0].phase == TouchPhase.Moved)
            {
                _touchDownPosition = touches[0].position;
                DetectSwipe();
            }

            if (touches[0].phase == TouchPhase.Ended)
            {
                _touchDownPosition = touches[0].position;
                DetectSwipe();
            }
        }

        private void DetectSwipe()
        {
            if (SwipeDistanceCheckMet())
            {
                if (IsVerticalSwipe())
                {
                    var direction = _touchDownPosition.y - _touchUpPosition.y > 0f ? MoveDirection.Up : MoveDirection.Down;
                    SendSwipe(direction);
                }
                else
                {
                    var direction = _touchDownPosition.x - _touchUpPosition.x > 0f ? MoveDirection.Right : MoveDirection.Left;
                    SendSwipe(direction);
                }

                _touchUpPosition = _touchDownPosition;
            }
        }

        private void SendSwipe(MoveDirection direction)
        {
            LogUtility.PrintLog(nameof(SwipeDetector), $"Swiped to the {direction.ToString()}");
            OnSwiped(new SwipeData(direction));
        }

        private bool SwipeDistanceCheckMet()
        {
            return VerticalMovementDistance() > _parameters.MinDistanceForSwipe || HorizontalMovementDistance() > _parameters.MinDistanceForSwipe;
        }

        private bool IsVerticalSwipe()
        {
            return VerticalMovementDistance() > HorizontalMovementDistance();
        }

        private bool IsHorizontalSwipe()
        {
            return HorizontalMovementDistance() > VerticalMovementDistance();
        }

        float VerticalMovementDistance()
        {
            return Mathf.Abs(_touchDownPosition.y - _touchUpPosition.y);
        }

        float HorizontalMovementDistance()
        {
            return Mathf.Abs(_touchDownPosition.x - _touchUpPosition.x);
        }

        public struct SwipeData
        {
            public readonly MoveDirection Direction;

            public SwipeData(MoveDirection direction)
            {
                Direction = direction;
            }
        }

        private void OnSwiped(in SwipeData swipeData)
        {
            Swiped?.Invoke(swipeData);
        }
    }
}