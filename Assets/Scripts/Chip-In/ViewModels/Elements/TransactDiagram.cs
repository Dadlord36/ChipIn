using UnityEngine;
using UnityEngine.EventSystems;
using Views;
using Views.ViewElements;

namespace ViewModels.Elements
{
    public class TransactDiagram : UIBehaviour
    {
        [SerializeField] private RadarView radarView;
        [SerializeField] private DiagramView diagramView;

        protected override void Start()
        {
            base.Start();
           SetMinimalDiagramValues();
        }

#if UNITY_EDITOR
        public void DrawAxis()
        {
            radarView.SetAxis();
        }

        public void InsertMinimalValues()
        {
            SetMinimalDiagramValues();
        }

        public void InsertMaximalValues()
        {
            SetMaximalDiagramValues();
        }
#endif

        public void SetRandomDiagramsValues()
        {
            var valuesArray = new Vector2[3];

            for (int i = 0; i < valuesArray.Length; i++)
            {
                valuesArray[i] = CreateRandomVector();
            }

            SetDiagramValues(valuesArray);

            Vector2 CreateRandomVector()
            {
                return new Vector2(Random.value, Random.value);
            }
        }

        private void SetDiagramValues(Vector2[] valuesArray)
        {
            radarView.SetDataToVisualize(valuesArray[0], valuesArray[1], valuesArray[2]);
            diagramView.SetValues(valuesArray[0], valuesArray[1], valuesArray[2]);
        }

        private void SetMinimalDiagramValues()
        {
            var vector = new Vector2(0f, 0f);
            SetDiagramValues(new[] {vector, vector, vector});
        }

        private void SetMaximalDiagramValues()
        {
            var vector = new Vector2(1f, 1f);
            SetDiagramValues(new[] {vector, vector, vector});
        }
    }
}