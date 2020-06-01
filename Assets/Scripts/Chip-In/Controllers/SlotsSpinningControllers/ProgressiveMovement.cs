using System;
using ScriptableObjects.Parameters;
using UnityEngine;

namespace Controllers.SlotsSpinningControllers
{
    public class ProgressiveMovement
    {
        private float _passedTime;
        private float _currentFrameDistancePercentage;

        private IProgressiveMovement _progressiveMovementInterface;
        private SlotSpinnerProperties _parameters;

        private Action _movementStops;

        public void Initialize(IProgressiveMovement progressiveMovementInterface,
            SlotSpinnerProperties parameters, Action onMovementStopsAction)
        {
            _progressiveMovementInterface = progressiveMovementInterface;
            _parameters = parameters;
            _movementStops = onMovementStopsAction;

            _progressiveMovementInterface.MovementParameters = _parameters;
        }

        public void ResetParameters()
        {
            _passedTime = _currentFrameDistancePercentage = 0f;
        }

        private void Stop()
        {
            OnMovementStops();
        }

        public void ProgressMovement()
        {
            _currentFrameDistancePercentage = ProgressMovementTime(0, _parameters.SpinTime, _passedTime);
            _progressiveMovementInterface.ProgressMovementAlongPath(_currentFrameDistancePercentage);

            _passedTime += Time.deltaTime * _parameters.SpeedCurve.Evaluate(_currentFrameDistancePercentage);
            if (_currentFrameDistancePercentage >= 1f)
            {
                Stop();
            }
        }
        

        private static float ProgressMovementTime(in float startTime, in float endTime, in float passedTime)
        {
            return Mathf.InverseLerp(startTime, endTime, passedTime);
        }

        private void OnMovementStops()
        {
            _movementStops?.Invoke();
        }
    }
}