using System;
using System.Collections.Generic;
using HttpRequests.RequestsProcessors.GetRequests;
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

        [SerializeField] private Object dotViewPrefab;
        #endregion

        private GameObject[] _dotsViews;

        private UICircle LargestCircle => innerCircles[0];

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

            _dotsViews = new GameObject[pointsCount];

            var parent = transform;

            for (int i = 0; i < pointsCount; i++)
            {
                var point = new Vector2(Mathf.Abs(points[i, 0]), Mathf.Abs(points[i, 1]));

                var distance = Vector2.Distance(Vector2.zero, point);
                var percentage = Mathf.InverseLerp(0, maxPoint, distance);

                var position = new DotInCircle().CalculatePosition(LargestCircle,
                    CalculateAngleOfPointOnCircle(points[i, 0], points[i, 1]), percentage);
                
                _dotsViews[i] = (GameObject) Instantiate(dotViewPrefab, parent);
                _dotsViews[i].transform.position = position;
            }
        }

        private void ClearDotsViews()
        {
            if (_dotsViews == null) return;

            for (int i = 0; i < _dotsViews.Length; i++)
            {
                Destroy(_dotsViews[i]);
            }
        }

        private static float CalculateAngleOfPointOnCircle(float x, float y)
        {
            return Mathf.Rad2Deg * Mathf.Atan2(y, x);
        }
    }
}