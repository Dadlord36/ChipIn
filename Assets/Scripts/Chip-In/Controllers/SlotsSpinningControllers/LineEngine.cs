using ScriptableObjects.Parameters;
using UnityEngine;
using Utilities;

namespace Controllers.SlotsSpinningControllers
{

    public class LineEngine
    {
        private Vector3 _lapStartPoint, _lapEndPoint;
        private Vector3 _wholePathStartPoint, _wholePathEndPoint;

        private float _wholePathLength;
        private float _lapLength;

        /// <summary>
        /// Length of item in world space
        /// </summary>
        public float ItemLength => MovementParameters.ItemLength;

        /// <summary>
        /// Length of item relative to whole path in percentage equivalent
        /// </summary>
        public float ItemStep { get; private set; }

        /// <summary>
        /// Length of item relative lap in percentage equivalent
        /// </summary>
        public float ItemsLapStep { get; private set; }

        public float WholePathLength => _wholePathLength;

        public float LapLength => _lapLength;

        public LineEngineParameters MovementParameters { get; private set; }

        /*private float CalculateLapLength(uint itemsCount)
        {
            return itemsCount * MovementParameters.Offset;
        }*/

        /*private float CalculateWholePathLength(uint itemsCount, uint indexOfItemToStopOn)
        {
            return ItemLength * MovementParameters.ControlItemIndex + MovementParameters.Laps * CalculateLapLength(itemsCount) +
                   ItemLength * (itemsCount - indexOfItemToStopOn);
        }*/

        public void RecalculateEngineParametersForNewPath(uint numberOfItemsOnLap, uint numberOfItemsOnWholePath)
        {
        }

        public void RecalculatePathSizes(float lapLength, float wholePathLength, LineEngineParameters parameters)
        {
            _lapLength = lapLength;
            _wholePathLength = wholePathLength;
            MovementParameters = parameters;

            /*void CalculateLapAndWholeLengths()
            {
                _lapLength = CalculateLapLength(itemsCount);
                _wholePathLength = CalculateWholePathLength(itemsCount, indexOfItemToStopOn);
            }*/

            void CalculateBorderPoints()
            {
                var center = Vector3.zero;

                _lapEndPoint = _lapStartPoint = center;

                _wholePathStartPoint.x = center.x - _wholePathLength;
                _wholePathEndPoint.x = center.x;

                _lapStartPoint.x = center.x - _lapLength;
                _lapEndPoint.x = center.x;
            }

            /*CalculateLapAndWholeLengths();*/
            CalculateBorderPoints();

            ItemStep = CalculateWholePathPercentageFromWholePathPartLength(ItemLength /*+MovementParameters.OffsetBetweenItems*/);
            ItemsLapStep = CalculateLapPartFromWholePathPercentage(ItemStep);
        }

        public Vector2 CalculateAnglePositionFromDistance(float distance)
        {
            return CircleUtility.FindAnglePosition(_lapEndPoint, distance, MovementParameters.MovementAngle);
        }

        public Vector3 CalculateItemAnglePosition(float wholePathPercentage, uint itemNumber)
        {
            var distance = CalculateLapPartFromWholePathPercentage(Mathf.Abs(wholePathPercentage) + ItemStep * itemNumber);
            return CalculateAnglePositionFromDistance(distance);
        }

        #region Path fallowing related calculation functions

        private float CalculateWholePathPartFromWholePathPercentage(in float percentage)
        {
            return Mathf.LerpUnclamped(0, _wholePathLength, percentage);
        }

        public float CalculateLapPartFromWholePathPercentage(in float wholePathPercentage)
        {
            return Mathf.Repeat(CalculateWholePathPartFromWholePathPercentage(wholePathPercentage), _lapLength);
        }

        public float CalculateWholePathPercentageFromWholePathPartLength(in float wholePathPartLength)
        {
            return wholePathPartLength / _wholePathLength;
        }

        private float CalculateLapPercentageFromLapPartLength(in float lapPartLength)
        {
            return lapPartLength / _lapLength;
        }

        private float CalculateLapPercentageFromWholePathPercentage(in float wholePathPercentage)
        {
            return CalculateLapPercentageFromLapPartLength(CalculateLapPartFromWholePathPercentage(wholePathPercentage));
        }

        public float CalculateItemLapPartFromWholePathPercentage(float wholePathPercentage, uint itemNumber)
        {
            return CalculateLapPartFromWholePathPercentage(Mathf.Abs(wholePathPercentage) + ItemStep * itemNumber);
        }

        private Vector3 GetPositionOnLap(in float percentage)
        {
            return CalculatePointBetweenTwoVectors(_lapStartPoint, _lapEndPoint, percentage);
        }

        private Vector3 GetPositionOnWholePath(in float percentage)
        {
            return CalculatePointBetweenTwoVectors(_wholePathStartPoint,_wholePathEndPoint,percentage);
        }

        #endregion

        #region Static Part

        private static Vector3 CalculatePointBetweenTwoVectors(Vector3 startPoint, Vector3 endPoint ,float percentage)
        {
            return Vector3.Lerp(startPoint, endPoint, percentage);
        }
        

        #endregion
    }
}