using UnityEngine;
using ViewModels.UI;

namespace Views
{
    public sealed class DiagramView : BaseView
    {
        [SerializeField] private DiagramColumnView diagramColumnView1;
        [SerializeField] private DiagramColumnView diagramColumnView2;
        [SerializeField] private DiagramColumnView diagramColumnView3;

        public DiagramView() : base(nameof(DiagramView))
        {
        }

        public void SetValues(Vector2 first, Vector2 second, Vector2 third)
        {
            diagramColumnView1.SetValues(first);
            diagramColumnView2.SetValues(second);
            diagramColumnView3.SetValues(third);
        }
    }
}