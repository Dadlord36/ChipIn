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

        protected override void OnEnable()
        {
            base.OnEnable();
            
        }

        protected override void Start()
        {
            base.Start();
            SetDiagramsValues();
        }

        public void SetDiagramsValues()
        {
            var valuesArray = new Vector2[3];

            for (int i = 0; i < valuesArray.Length; i++)
            {
                valuesArray[i] = CreateRandomVector();
            }

            radarView.SetDataToVisualize(valuesArray[0],valuesArray[1],valuesArray[2]);
            diagramView.SetValues(valuesArray[0],valuesArray[1],valuesArray[2]);
            
            Vector2 CreateRandomVector()
            {
                return new Vector2(Random.value, Random.value);
            }
        }
        
    }
}