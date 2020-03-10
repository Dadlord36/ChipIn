using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;

namespace Views.ViewElements
{
    public class DotOnCircle : UIBehaviour
    {
        [SerializeField] private UICircle circle;
        [SerializeField, Range(0, 360)] private float angle;
        private float Radius => circle.rectTransform.sizeDelta.x / 2 * circle.rectTransform.localScale.x;
        private float Rad => Mathf.Deg2Rad * angle;
        private Vector2 _dotPosition;

        public Vector2 DotPosition => circle.rectTransform.InverseTransformPoint(_dotPosition);

        protected override void OnValidate()
        {
            base.OnValidate();
            circle = GetComponent<UICircle>();
            _dotPosition = circle.rectTransform.position;
            _dotPosition += CalculateVector2DotPosition();
        }

        private void DrawDotOnCircle()
        {
            Gizmos.DrawSphere(_dotPosition, 10);
        }

        private Vector3 CalculateVector3DotPosition()
        {
            var rad = Rad;
            return new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * Radius;
        }

        private Vector2 CalculateVector2DotPosition()
        {
            var rad = Rad;
            return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * Radius;
        }

        private void OnDrawGizmos()
        {
            DrawDotOnCircle();
        }
    }
}