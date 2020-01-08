using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Views;

namespace UI.Elements
{
    [RequireComponent(typeof(UICircle))]
    public class CommunitySpiritAnalyticView : BaseView
    {
        private const string Tag = "CommunitySpiritAnalyticView";

        [SerializeField, HideInInspector] private List<UICircle> innerCircles;
        [SerializeField, HideInInspector] private UICircle backgroundCircle;

        [SerializeField, HideInInspector] public Color circlesColor;
        [SerializeField, HideInInspector] public uint circlesThickness;
        [SerializeField, HideInInspector] public float scaleFactor;
        [SerializeField, HideInInspector] public int circlesBaseSize;
        [SerializeField, HideInInspector] public uint arcSteps;

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
                innerCircle.SetThickness((int)thickness);
                innerCircle.SetBaseColor(color);
                innerCircle.SetArcSteps((int)arcSteps);
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
                // progress = (float)index/innerCircles.Count;

                progress += analyticViewScaleFactor;
                SetCircleScale(innerCircles[index], Mathf.Lerp(1f, 0f, progress));
            }
        }
    }
}