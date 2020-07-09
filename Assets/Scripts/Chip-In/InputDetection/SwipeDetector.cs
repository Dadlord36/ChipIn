using System;
using ActionsTranslators;
using ScriptableObjects.Parameters;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace InputDetection
{
    public class SwipeDetector : IUpdatable
    {
        public event Action<SwipeData> Swiped;

        private readonly SwipeDetectorParameters _parameters;

        public SwipeDetector(SwipeDetectorParameters parameters)
        {
            _parameters = parameters;
        }

        private Vector2 TouchUpPosition { get; set; }

        private Vector2 TouchDownPosition { get; set; }

        private Vector2 TempCursorPosition { get; set; }

        private Vector2 DeltaPosition { get; set; }

        private bool CanSwipe()
        {
            return Math.Abs(Math.Abs(Vector2.Distance(TempCursorPosition, TouchUpPosition))) > _parameters.MinDistanceForSwipe;
        }

        private void RecalculateDeltaPosition()
        {
            DeltaPosition = TempCursorPosition - TouchUpPosition;
        }

#if UNITY_EDITOR
        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TouchUpPosition = TouchDownPosition = TempCursorPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0) && !_parameters.DetectSwipeOnlyAfterRelease)
            {
                TempCursorPosition = Input.mousePosition;
                if (!CanSwipe()) return;
                RecalculateDeltaPosition();

                TouchUpPosition = TempCursorPosition;
                DetectSwipe();
            }

            if (Input.GetMouseButtonUp(0))
            {
                RecalculateDeltaPosition();
                TouchUpPosition = Input.mousePosition;
                DetectSwipe();
            }
        }
#else
        public void Update()
        {
            var touches = Input.touches;
            if(touches.Length < 1) return;

            if (touches[0].phase == TouchPhase.Began)
            {
                TouchDownPosition = TouchUpPosition = touches[0].position;
            }

            if (!_parameters.DetectSwipeOnlyAfterRelease && touches[0].phase == TouchPhase.Moved)
            {
                TempCursorPosition = touches[0].position;
                 if (!CanSwipe()) return;
                TouchUpPosition = TempCursorPosition;
                DetectSwipe();
            }

            if (touches[0].phase == TouchPhase.Ended)
            {
                TouchUpPosition = touches[0].position;
                DetectSwipe();
            }
        }
#endif

        private void DetectSwipe()
        {
            if (!SwipeDistanceCheckMet()) return;

            MoveDirection direction;


            if (IsVerticalSwipe())
            {
                direction = DeltaPosition.y > 0f
                    ? MoveDirection.Down
                    : MoveDirection.Up;
            }
            else
            {
                direction = DeltaPosition.x > 0f
                    ? MoveDirection.Left
                    : MoveDirection.Right;
            }

            SendSwipe(direction, TouchDownPosition, TouchUpPosition, DeltaPosition);
        }

        private void SendSwipe(in MoveDirection direction, in Vector2 touchDownPosition, in Vector2 touchUpPosition, Vector2 deltaPosition)
        {
            LogUtility.PrintLog(nameof(SwipeDetector), $"Swiped to the {direction.ToString()}");
            OnSwiped(new SwipeData(direction, touchDownPosition, touchUpPosition, deltaPosition));
        }

        private bool SwipeDistanceCheckMet()
        {
            return VerticalMovementDistance() > _parameters.MinDistanceForSwipe ||
                   HorizontalMovementDistance() > _parameters.MinDistanceForSwipe;
        }

        private bool IsVerticalSwipe()
        {
            return VerticalMovementDistance() > HorizontalMovementDistance();
        }

        private bool IsHorizontalSwipe()
        {
            return HorizontalMovementDistance() > VerticalMovementDistance();
        }

        private float VerticalMovementDistance()
        {
            return Mathf.Abs(TouchDownPosition.y - TouchUpPosition.y);
        }

        private float HorizontalMovementDistance()
        {
            return Mathf.Abs(TouchDownPosition.x - TouchUpPosition.x);
        }

        public readonly struct SwipeData
        {
            public readonly MoveDirection Direction;
            public readonly Vector2 TouchDownPoint, TouchUpPoint;
            public readonly Vector2 TouchDelta;

            public SwipeData(MoveDirection direction, Vector2 touchDownPoint, Vector2 touchUpPoint, Vector2 deltaPosition)
            {
                Direction = direction;
                TouchDownPoint = touchDownPoint;
                TouchUpPoint = touchUpPoint;
                TouchDelta = deltaPosition;
            }
        }

        private void OnSwiped(in SwipeData swipeData)
        {
            Swiped?.Invoke(swipeData);
        }
    }
}