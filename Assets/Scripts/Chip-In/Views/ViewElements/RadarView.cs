﻿using System;
using System.Collections.Generic;
using System.Linq;
using HttpRequests.RequestsProcessors.GetRequests;
using Shapes2D;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Object = UnityEngine.Object;

namespace Views.ViewElements
{
    [Serializable]
    public class DotInCircle
    {
        [SerializeField, Range(0, 360)] private float angle;
        [SerializeField, Range(0f, 1f)] private float percentageOfRadius;

        private float GetRadius(UICircle circle)
        {
            var rectTransform = circle.rectTransform;
            return rectTransform.sizeDelta.x / 2 * rectTransform.localScale.x * percentageOfRadius;
        }

        private float Rad => Mathf.Deg2Rad * angle;

        public Vector2 DotPosition { get; private set; }

        public Vector2 CalculatePosition(UICircle circle)
        {
            DotPosition = circle.rectTransform.position;
            DotPosition += CalculateVector2DotPosition(GetRadius(circle));

            return DotPosition;
        }

        public Vector2 CalculatePosition(UICircle circle, float relativeAngle, float percentageOfRadius)
        {
            angle = relativeAngle;
            this.percentageOfRadius = percentageOfRadius;
            return CalculatePosition(circle);
        }

        private Vector3 CalculateVector3DotPosition(float radius)
        {
            var rad = Rad;
            return new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * radius;
        }

        private Vector2 CalculateVector2DotPosition(float radius)
        {
            var rad = Rad;
            return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;
        }
    }

    [RequireComponent(typeof(UICircle))]
    public class RadarView : BaseView
    {
        private const string Tag = "CommunitySpiritAnalyticView";

        #region Serialized Fields

        [SerializeField, HideInInspector] private List<UICircle> innerCircles;
        [SerializeField, HideInInspector] private UICircle backgroundCircle;

        [SerializeField, HideInInspector] public Color circlesColor;
        [SerializeField, HideInInspector] public uint circlesThickness;
        [SerializeField, HideInInspector] public float scaleFactor;
        [SerializeField, HideInInspector] public int circlesBaseSize;
        [SerializeField, HideInInspector] public uint arcSteps;
        [SerializeField] private Vector2 firstColumnAngles, secondColumnAngles, thirdColumnAngles;

        [SerializeField] private Shape pointsPathVisualizer;
        [SerializeField] private float distance;
        [SerializeField] private UILineRenderer axisLineRenderer;

        [SerializeField] private Object dotViewPrefab;

        #endregion

        private List<GameObject> _dotsViews = new List<GameObject>();

        private UICircle LargestCircle => innerCircles[0];
        private Vector2 LargestCircleCenter => LargestCircle.transform.position;


#if UNITY_EDITOR
        [SerializeField] private DotInCircle[] dots;
        private void OnDrawGizmos()
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].CalculatePosition(LargestCircle);
                Gizmos.DrawSphere(dots[i].DotPosition, 10f);
            }
        }
#endif

        protected override void OnEnable()
        {
            base.OnEnable();
            if (innerCircles == null)
            {
                CreateBackground();
            }
        }

        private void CreateBackground()
        {
            backgroundCircle = gameObject.AddComponent<UICircle>();
        }

        public void AddCircle()
        {
            var newGameObject = new GameObject("Circle");
            AttachToRoot(newGameObject);

            var rectTransform = newGameObject.AddComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;

            var circle = newGameObject.AddComponent<UICircle>();
            innerCircles.Add(circle);
        }

        public void SetCirclesStyle(bool fill, int analyticViewCirclesBaseSize, uint arcSteps, uint thickness, in Color color)
        {
            void SetCircleSize(GameObject gO, float size)
            {
                if (!gO.TryGetComponent(out RectTransform rectTransform))
                {
                    rectTransform = gO.AddComponent<RectTransform>();
                }

                rectTransform.sizeDelta = new Vector2(size, size);
            }

            SetCircleSize(gameObject, analyticViewCirclesBaseSize);

            foreach (var innerCircle in innerCircles)
            {
                SetCircleSize(innerCircle.gameObject, analyticViewCirclesBaseSize);
                innerCircle.raycastTarget = false;
                innerCircle.SetFill(fill);
                innerCircle.SetThickness((int) thickness);
                innerCircle.SetBaseColor(color);
                innerCircle.SetArcSteps((int) arcSteps);
            }
        }

        private void AttachToRoot(GameObject toAttach)
        {
            toAttach.transform.parent = transform;
        }

        public void ClearCirclesArray()
        {
            for (int i = 0; i < innerCircles.Count; i++)
            {
                DestroyImmediate(innerCircles[i]?.gameObject);
            }

            innerCircles.Clear();
        }

        public void RemoveLastCircle()
        {
            var lastIndex = innerCircles.Count - 1;
            if (lastIndex < 0)
            {
                Debug.unityLogger.Log(LogType.Error, Tag, "There is no more circles in array", this);
                return;
            }

            var lastCircle = innerCircles[lastIndex];
            innerCircles.RemoveAt(lastIndex);
            DestroyImmediate(lastCircle.gameObject);
        }

        public void ScaleCircles(float analyticViewScaleFactor)
        {
            var progress = 0f;
            if (innerCircles.Count == 0)
            {
                Debug.unityLogger.Log(LogType.Error, Tag, "There is no circles in array", this);
                return;
            }

            SetCircleScale(innerCircles[0], 1f);

            void SetCircleScale(Component component, float scale)
            {
                component.transform.localScale = new Vector3(scale, scale, 1f);
            }

            for (var index = 1; index < innerCircles.Count; index++)
            {
                progress += analyticViewScaleFactor;
                SetCircleScale(innerCircles[index], Mathf.Lerp(1f, 0f, progress));
            }
        }

        public void SetDataToVisualize(RadarData radarData)
        {
            ClearDotsViews();

            var maxPoint = radarData.Max;
            var points = radarData.Points;
            var pointsCount = points.GetLength(0);

            _dotsViews = new List<GameObject>(pointsCount);


            for (int i = 0; i < pointsCount; i++)
            {
                var point = new Vector2(Mathf.Abs(points[i, 0]), Mathf.Abs(points[i, 1]));

                var distance = Vector2.Distance(Vector2.zero, point);
                var percentage = Mathf.InverseLerp(0, maxPoint, distance);

                var position = new DotInCircle().CalculatePosition(LargestCircle,
                    CalculateAngleOfPointOnCircle(points[i, 0], points[i, 1]), percentage);

                CreateDotAtPosition(position);
            }
        }

        private GameObject CreateDotAtPosition(Vector2 position)
        {
            var gO = (GameObject) Instantiate(dotViewPrefab, transform);
            gO.transform.position = position;
            _dotsViews.Add(gO);
            return gO;
        }

        private void ClearDotsViews()
        {
            if (_dotsViews == null) return;

            for (int i = 0; i < _dotsViews.Count; i++)
            {
                DoDestroy(_dotsViews[i]);
            }
        }

        private void DoDestroy(Object objectToDestroy)
        {
#if UNITY_EDITOR
            DestroyImmediate(objectToDestroy);
#else
    Destroy(objectToDestroy);
#endif
        }

        private static float CalculateAngleOfPointOnCircle(float x, float y)
        {
            return Mathf.Rad2Deg * Mathf.Atan2(y, x);
        }

        private void SetAxis()
        {
            var axisArray = GetDiagramAxisEndPoints();
            var segmentsFromCenter = new List<Vector2>(axisArray.Length * 2);

            var center = LargestCircle.transform.localPosition;

            for (int i = 0; i < axisArray.Length; i++)
            {
                segmentsFromCenter.Add(center);
                segmentsFromCenter.Add(axisArray[i]);
            }

            axisLineRenderer.Points = segmentsFromCenter.ToArray();
        }

        private Vector2[] GetDiagramAxisEndPoints()
        {
            var diagramAxisAngles = new[]
            {
                firstColumnAngles.x, secondColumnAngles.x, thirdColumnAngles.x,
                thirdColumnAngles.y, secondColumnAngles.y, firstColumnAngles.y
            };

            var endPoints = new Vector2[diagramAxisAngles.Length];

            for (int i = 0; i < diagramAxisAngles.Length; i++)
            {
                endPoints[i] = LargestCircle.transform.InverseTransformPoint(new DotInCircle().CalculatePosition(LargestCircle,
                    diagramAxisAngles[i], 1f));
            }

            return endPoints;
        }

        public void SetDataToVisualize(Vector2 firstColumn, Vector2 secondColumn, Vector2 thirdColumn)
        {
            ClearDotsViews();
            SetAxis();

            var topPointsList = new List<Vector2>(3);
            var bottomPointsList = new List<Vector2>(3);

            AddColumnRelatedDots(firstColumn, firstColumnAngles);
            AddColumnRelatedDots(secondColumn, secondColumnAngles);
            AddColumnRelatedDots(thirdColumn, thirdColumnAngles);

            var resultList = new List<Vector2>(3 * 2);
            resultList.AddRange(topPointsList);
            resultList.AddRange(bottomPointsList);
            resultList.Add(topPointsList.First());

            SetupPointsVisualization(resultList, LargestCircleCenter);

            void AddColumnRelatedDots(Vector2 columnValues, Vector2 relativeAngles)
            {
                var positionA = new DotInCircle().CalculatePosition(LargestCircle, relativeAngles.x,
                    columnValues.x);
                var positionB = new DotInCircle().CalculatePosition(LargestCircle, relativeAngles.y,
                    columnValues.y);
                /*topPointsList.Add(LargestCircle.transform.InverseTransformPoint(positionA));
                bottomPointsList.Add(LargestCircle.transform.InverseTransformPoint(positionB)); */
                topPointsList.Add(positionA);
                bottomPointsList.Add(positionB);
            }
        }

        private void SetupPointsVisualization(IReadOnlyList<Vector2> pointsOnCircle, Vector2 circleCenter)
        {
            pointsPathVisualizer.settings.shapeType = ShapeType.Path;
            var pathSegments = new List<PathSegment>();

            for (int i = 0; i < pointsOnCircle.Count - 1; i++)
            {
                var currentPointPosition = pointsOnCircle[i];
                var nextPointPosition = pointsOnCircle[i + 1];

                var middle = (nextPointPosition + currentPointPosition) / 2;
                var directionToCenter = GetDirection(middle, circleCenter);

                var curveOffset = middle + directionToCenter * distance;

                pathSegments.Add(new PathSegment(currentPointPosition, curveOffset, nextPointPosition));
            }

            pathSegments.Add(new PathSegment(pointsOnCircle[0], pointsOnCircle[pointsOnCircle.Count - 1]));
            var resultArray = pathSegments.ToArray();
            pointsPathVisualizer.SetPathWorldSegments(resultArray);
        }

        private static Vector2 GetDirection(Vector2 from, Vector2 to)
        {
            // Gets a vector that points from the player's position to the target's.
            var heading = from - to;
            return heading / heading.magnitude; // This is now the normalized direction.
        }
    }
}