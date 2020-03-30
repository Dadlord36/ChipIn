using UnityEngine;
using ViewModels.UI;

namespace Views
{
    public class DiagramView : BaseView
    {
        [SerializeField] private DiagramColumnView diagramColumnView1;
        [SerializeField] private DiagramColumnView diagramColumnView2;
        [SerializeField] private DiagramColumnView diagramColumnView3;

        protected override void OnEnable()
        {
            base.OnEnable();
            SetValues(CreateRandomVector(), CreateRandomVector(), CreateRandomVector());

            Vector2 CreateRandomVector()
            {
                return new Vector2(Random.value, Random.value);
            }
        }

        public void SetValues(Vector2 first, Vector2 second, Vector2 third)
        {
            diagramColumnView1.SetValues(first);
            diagramColumnView2.SetValues(second);
            diagramColumnView3.SetValues(third);
        }
    }
}