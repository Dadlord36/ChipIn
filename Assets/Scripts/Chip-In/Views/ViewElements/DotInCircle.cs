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
            var worldCorners = new Vector3[4];
            rectTransform.GetWorldCorners(worldCorners);
            var rectWidth = Vector3.Distance(worldCorners[0], worldCorners[1]) ;
            return rectWidth / 2 *  percentageOfRadius;
        }

        private float Rad => Mathf.Deg2Rad * angle;

        public Vector2 DotPosition { get; private set; }

        public Vector2 CalculatePointOffsetInWorldSpace(UICircle circle)
        {
            DotPosition = CalculateVector2DotPosition(GetRadius(circle));
            return DotPosition;
        }

        public Vector2 CalculatePointOffsetInWorldSpace(UICircle circle, float relativeAngle, float inPercentageOfRadius)
        {
            angle = relativeAngle;
            percentageOfRadius = inPercentageOfRadius;
            return CalculatePointOffsetInWorldSpace(circle);
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