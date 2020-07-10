using System;
using UnityEngine;
using UnityEngine.UI.Extensions;

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
}