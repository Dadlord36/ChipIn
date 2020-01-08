using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Views;


namespace UI.Elements
{
    [RequireComponent(typeof(UICircle))]
    public class CommunitySpiritAnalyticView : BaseView
    {
        [SerializeField, HideInInspector] private List<UICircle> innerCircles;
        [SerializeField, HideInInspector] private UICircle backgroundCircle;

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

        private void AddCircle()
        {
            var newGameObject = new GameObject("Circle");
            var circle = newGameObject.AddComponent<UICircle>();
            
            
            innerCircles.Add(circle);
        }

        private void RemoveLastCircle()
        {
            var lastIndex = innerCircles.Count - 1;
            var lastCircle = innerCircles[lastIndex];
            innerCircles.RemoveAt(lastIndex);
            Destroy(lastCircle.gameObject);
        }
    }
}