using System;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;
using UnityEngine.UI.Extensions;
using BezierPath = PathCreation.BezierPath;

namespace Views.ViewElements
{
    public readonly struct AngleAndDistancePercentage
    {
        public readonly float Angle;
        public readonly float Percentage;

        public AngleAndDistancePercentage(float angle, float percentage)
        {
            Angle = angle;
            Percentage = percentage;
        }
    }
    
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

        public void VisualizePoints(float[,] points, float radarDataMax)
        {
            VisualizePoints(Radar.CalculateWorldPositionsForGivenRadarPoints(points, radarDataMax, GameManager.ScreenResolutionScale.y));
        }

        public void VisualizePoints(AngleAndDistancePercentage[] data)
        {
            VisualizePoints(Radar.CalculateWorldPositionsForGivenDistancePercentages(data, GameManager.ScreenResolutionScale.y));
        }

        private void VisualizePoints(IEnumerable<Vector2> points)
        {
            pointsVisualizerLineRenderer.enabled = true;
            SetupPointsVisualization(points);
        }

        private void SetupPointsVisualization(IEnumerable<Vector2> vectorsArray)
        {
            SetupPathCreator(vectorsArray);
            pointsVisualizerLineRenderer.Points = GetPathPointsSample(sampleSteps);
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

        private Vector2[] CalculatePointsForGivenDistancePercentages(IReadOnlyList<float> percentages)
        {
            var points = new Vector2[percentages.Count];
            for (int i = 0; i < percentages.Count; i++)
            {
                points[i] = GetPoint(percentages[i]);
            }

            return points;
        }

        private Vector2 GetPoint(float percentage)
        {
            return pointsVisualizerLineRenderer.transform.InverseTransformPoint(pathCreator.path.GetPointAtDistance(percentage));
        }
    }
}