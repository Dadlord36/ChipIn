using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;

namespace Views.ViewElements
{
    public interface IRadar
    {
        UICircle LargestCircle { get; }
        Vector2[] CalculateWorldPositionsForGivenRadarPoints(float[,] points, float maxPoint);
    }

    public class Radar : UIBehaviour, IRadar
    {
        private const string Tag = nameof(Radar);

        [SerializeField, HideInInspector] private List<UICircle> innerCircles;
        [SerializeField, HideInInspector] private UICircle backgroundCircle;

        [SerializeField, HideInInspector] public Color circlesColor;
        [SerializeField, HideInInspector] public uint circlesThickness;
        [SerializeField, HideInInspector] public float scaleFactor;
        [SerializeField, HideInInspector] public int circlesBaseSize;
        [SerializeField, HideInInspector] public uint arcSteps;
        [Space(10f)] [SerializeField] private float minRadiusPercentageValue;

        public UICircle LargestCircle => innerCircles[0];


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


        public Vector2[] CalculateWorldPositionsForGivenRadarPoints(float[,] points, float maxPoint)
        {
            var pointsCount = points.GetLength(0);
            var positions = new Vector2[pointsCount];

            for (int i = 0; i < pointsCount; i++)
            {
                var point = new Vector2(Mathf.Abs(points[i, 0]), Mathf.Abs(points[i, 1]));

                var distance = Vector2.Distance(Vector2.zero, point);
                var percentage = Mathf.InverseLerp(0, maxPoint, distance);

                positions[i] = new DotInCircle().CalculatePointOffsetInWorldSpace(LargestCircle,
                    CalculateAngleOfPointOnCircle(points[i, 0], points[i, 1]), percentage);
            }

            return positions;
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

        public void AddCircle()
        {
            var newGameObject = new GameObject("Circle");
            AttachToRoot(newGameObject);

            var rectTransform = newGameObject.AddComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;

            var circle = newGameObject.AddComponent<UICircle>();
            innerCircles.Add(circle);
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


        public void SetDataToVisualize(Vector2 firstColumn, Vector2 secondColumn, Vector2 thirdColumn)
        {
            ClampDiagramValues(ref firstColumn, ref secondColumn, ref thirdColumn);

            var topPointsList = new List<Vector2>(3);
            var bottomPointsList = new List<Vector2>(3);

            // AddColumnRelatedDots(firstColumn, firstColumnAngles);
            // AddColumnRelatedDots(secondColumn, secondColumnAngles);
            // AddColumnRelatedDots(thirdColumn, thirdColumnAngles);

            var resultList = new List<Vector2>(3 * 2);
            resultList.AddRange(topPointsList);
            resultList.AddRange(bottomPointsList);


            void AddColumnRelatedDots(Vector2 columnValues, Vector2 relativeAngles)
            {
                var positionA = new DotInCircle().CalculatePointOffsetInWorldSpace(LargestCircle, relativeAngles.x,
                    columnValues.x);
                var positionB = new DotInCircle().CalculatePointOffsetInWorldSpace(LargestCircle, relativeAngles.y,
                    columnValues.y);
                topPointsList.Add(LargestCircle.transform.InverseTransformPoint(positionA));
                bottomPointsList.Add(LargestCircle.transform.InverseTransformPoint(positionB));
            }
        }


        private void ClampDiagramValues(ref Vector2 firstColumn, ref Vector2 secondColumn, ref Vector2 thirdColumn)
        {
            firstColumn = ClampVector2(firstColumn);
            secondColumn = ClampVector2(secondColumn);
            thirdColumn = ClampVector2(thirdColumn);

            Vector2 ClampVector2(in Vector2 vector)
            {
                return new Vector2(Mathf.Clamp(vector.x, minRadiusPercentageValue, 1f),
                    Mathf.Clamp(vector.y, minRadiusPercentageValue, 1f));
            }
        }

        private static float CalculateAngleOfPointOnCircle(float x, float y)
        {
            return Mathf.Rad2Deg * Mathf.Atan2(y, x);
        }
    }
}