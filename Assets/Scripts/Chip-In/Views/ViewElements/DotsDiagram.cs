using System.Collections.Generic;
using HttpRequests.RequestsProcessors.GetRequests;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;

namespace Views.ViewElements
{
    [RequireComponent(typeof(UICircle))]
    public sealed class DotsDiagram : UIBehaviour
    {
        #region Serialized Fields

        [SerializeField] private UILineRenderer axisLineRenderer;
        [SerializeField] private Radar radar;
        [SerializeField] private Object dotViewPrefab;

        [SerializeField] private Vector2 firstColumnAngles, secondColumnAngles, thirdColumnAngles;

        #endregion

        private List<GameObject> _dotsViews = new List<GameObject>();

        private IRadar Radar => radar;
        private UICircle LargestCircle => Radar.LargestCircle;


/*#if UNITY_EDITOR
        [SerializeField] private DotInCircle[] dots;
        private void OnDrawGizmos()
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].CalculatePosition(LargestCircle);
                Gizmos.DrawSphere(dots[i].DotPosition, 10f);
            }
        }
#endif*/

        public void SetAxis()
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
                var endPoint = new DotInCircle().CalculatePointOffsetInWorldSpace(LargestCircle,
                    diagramAxisAngles[i], 1f);
                endPoints[i] = transform.InverseTransformPoint(endPoint);
            }

            return endPoints;
        }


        public void SetDataToVisualize(RadarData radarData)
        {
            ClearDotsViews();

            var points = radarData.Points;
            var pointsCount = points.GetLength(0);
            _dotsViews = new List<GameObject>(pointsCount);

            var positions = Radar.CalculateWorldPositionsForGivenRadarPoints(points, radarData.Max, 1f);

            for (int i = 0; i < positions.Length; i++)
            {
                CreateDotAtPosition(positions[i]);
            }
        }

        private GameObject CreateDotAtPosition(Vector2 position)
        {
            var gO = (GameObject) Instantiate(dotViewPrefab, transform);
            gO.transform.localPosition = position;
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
    }
}