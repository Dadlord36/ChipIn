﻿using System;
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
        private readonly SwipeDetectorParameters _parameters;

        public SwipeDetector(SwipeDetectorParameters parameters)
        {
            _parameters = parameters;
        }

#if UNITY_EDITOR
        private Vector2 _tempCursorPosition;
        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _touchUpPosition = _touchDownPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0) && !_parameters.DetectSwipeOnlyAfterRelease)
            {
                _tempCursorPosition = Input.mousePosition;
                if (Math.Abs(Math.Abs(Vector2.Distance(_tempCursorPosition, _touchUpPosition))) < 0.2f) return;
                _touchUpPosition = _tempCursorPosition;
                DetectSwipe();
            }

            if (Input.GetMouseButtonUp(0))
            {
                _touchUpPosition = Input.mousePosition;
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
                _touchDownPosition = _touchUpPosition = touches[0].position;
            }

            if (!_parameters.DetectSwipeOnlyAfterRelease && touches[0].phase == TouchPhase.Moved)
            {
                _tempCursorPosition = touches[0].position;
                if (Math.Abs(Math.Abs(Vector2.Distance(_tempCursorPosition, _touchUpPosition))) < 0.1f) return;
                _touchUpPosition = _tempCursorPosition;
                DetectSwipe();
            }

            if (touches[0].phase == TouchPhase.Ended)
            {
                _touchUpPosition = touches[0].position;
                DetectSwipe();
            }
        }
#endif

        private void DetectSwipe()
        {
            if (!SwipeDistanceCheckMet()) return;

            MoveDirection direction;
            var touchDelta = _touchDownPosition - _touchUpPosition;

            if (IsVerticalSwipe())
            {
                direction = touchDelta.y > 0f
                    ? MoveDirection.Down
                    : MoveDirection.Up;
            }
            else
            {
                direction = touchDelta.x > 0f
                    ? MoveDirection.Left
                    : MoveDirection.Right;
            }

            SendSwipe(direction, _touchDownPosition, _touchUpPosition, touchDelta);
        }

        private void SendSwipe(in MoveDirection direction, in Vector2 touchDownPosition, in Vector2 touchUpPosition, in Vector2 deltaPosition)
        {
            LogUtility.PrintLog(nameof(SwipeDetector), $"Swiped to the {direction.ToString()}");
            OnSwiped(new SwipeData(direction, deltaPosition, touchDownPosition, touchUpPosition));
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
            return Mathf.Abs(_touchDownPosition.y - _touchUpPosition.y);
        }

        private float HorizontalMovementDistance()
        {
            return Mathf.Abs(_touchDownPosition.x - _touchUpPosition.x);
        }

        public struct SwipeData
        {
            public readonly MoveDirection Direction;
            public readonly Vector2 TouchDownPoint, TouchUpPoint;
            public Vector2 DeltaVector;

            public SwipeData(MoveDirection direction, Vector2 deltaVector, Vector2 touchDownPoint, Vector2 touchUpPoint)
            {
                Direction = direction;
                DeltaVector = deltaVector;
                TouchDownPoint = touchDownPoint;
                TouchUpPoint = touchUpPoint;
            }
        }

        private void OnSwiped(in SwipeData swipeData)
        {
            Swiped?.Invoke(swipeData);
        }
    }
}