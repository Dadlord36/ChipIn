using System.Collections.Generic;
using PathCreation;
using UnityEngine;
using UnityEngine.UI.Extensions;
using BezierPath = PathCreation.BezierPath;
using RectTransformUtility = Utilities.RectTransformUtility;

namespace Views.ViewElements
{
    public sealed class SplineDiagram : MonoBehaviour
    {
        [Space(15)] [SerializeField] private int sampleSteps;
        [SerializeField] private UILineRenderer pointsVisualizerLineRenderer;
        [SerializeField] private PathCreator pathCreator;
        [SerializeField] private Radar radar;

        private IRadar Radar => radar;

        private void ResetPathCreatorAndLineRendererPositions()
        {
            pointsVisualizerLineRenderer.transform.position = pathCreator.transform.position = Vector3.zero;
        }

        private static Vector2[] ConvertToScreenSpace(Vector2[] vectorsArray)
        {
            return RectTransformUtility.ConvertFromWorldToScreenSpace(GameManager.MainCamera, vectorsArray);
        }

        private void SetupPathCreator(IEnumerable<Vector2> vectorsArray)
        {
            var bezierPath = new BezierPath(vectorsArray, PathSpace.xy, true)
            {
                ControlPointMode = BezierPath.ControlMode.Automatic
            };
            bezierPath.CalculateBoundsWithTransform(transform);
            bezierPath.ResetNormalAngles();
            pathCreator.bezierPath = bezierPath;
            pathCreator.TriggerPathUpdate();
        }

        public void VisualizePoints(float[,] points, float radarDataMax)
        {
            pointsVisualizerLineRenderer.enabled = true;
            var positionsInWorldSpace = Radar.CalculateWorldPositionsForGivenRadarPoints(points, radarDataMax);

            SetupPointsVisualization(positionsInWorldSpace);
        }

        private void SetupPointsVisualization(Vector2[] vectorsArray)
        {
            SetupPathCreator(vectorsArray);
            pointsVisualizerLineRenderer.Points = GetPathPointsSample(sampleSteps);
        }

        private Vector2[] GetPathPointsSample(int stepsCount)
        {
            var resultList = new List<Vector2>(pathCreator.path.NumPoints);
            var pathLength = pathCreator.path.length;
            var progress = 0f;
            var stepLength = pathLength / stepsCount;

            for (int i = 0; i < stepsCount; i++)
            {
                resultList.Add(GetPoint(progress));
                progress += stepLength;
            }

            return resultList.ToArray();
        }

        private Vector2 GetPoint(float percentage)
        {
            return pathCreator.transform.InverseTransformPoint(pathCreator.path.GetPointAtDistance(percentage));
        }
    }
}