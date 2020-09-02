using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace Views.ViewElements
{
    [Serializable]
    public struct DotInCircle
    {
        [SerializeField, Range(0, 360)] private float angle;
        [SerializeField, Range(0f, 1f)] private float percentageOfRadius;

        private float GetRadius(RectTransform circle, float widthScale)
        {
            return widthScale * (circle.sizeDelta.x * circle.localScale.x / 2 * percentageOfRadius);
        }

        private float Rad => Mathf.Deg2Rad * angle;

        public Vector2 DotPosition { get; private set; }

        private Vector2 CalculatePointOffset(Graphic circle, float widthScale)
        {
            return  CalculateVector2DotPosition(GetRadius(circle.rectTransform, widthScale));
        }

        public Vector2 CalculatePointOffsetInWorldSpace(UICircle circle, float relativeAngle, float inPercentageOfRadius, float widthScale = 1f)
        {
            angle = relativeAngle;
            percentageOfRadius = inPercentageOfRadius;
            return CalculatePointOffset(circle,widthScale);
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