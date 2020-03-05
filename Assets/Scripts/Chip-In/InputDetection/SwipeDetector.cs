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
            for (int i = 0; i < touches.Length; i++)
            {
                if (touches[i].phase == TouchPhase.Began)
                {
                    _touchDownPosition = _touchUpPosition = touches[i].position;
                }

                if (!_parameters.DetectSwipeOnlyAfterRelease && touches[i].phase == TouchPhase.Moved)
                {
                    _touchDownPosition = touches[i].position;
                    DetectSwipe();
                }

                if (touches[i].phase == TouchPhase.Ended)
                {
                    _touchDownPosition = touches[i].position;
                    DetectSwipe();
                }
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