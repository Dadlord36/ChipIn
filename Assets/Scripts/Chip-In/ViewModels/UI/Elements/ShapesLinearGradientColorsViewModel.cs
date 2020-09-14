using Shapes2D;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.UI.Elements
{
    [Binding]
    [RequireComponent(typeof(Shape))]
    public class ShapesLinearGradientColorsViewModel : MonoBehaviour
    {
        private Shape _shape;

        private Shape.UserProps Settings => _shape.settings;

        [Binding]
        public Color StartColor
        {
            get => Settings.fillColor;
            set => Settings.fillColor = value;
        }

        [Binding]
        public Color EndColor
        {
            get => Settings.fillColor2;
            set => Settings.fillColor2 = value;
        }

        private void OnEnable()
        {
            _shape = GetComponent<Shape>();
            Settings.fillType = FillType.Gradient;
            Settings.gradientType = GradientType.Linear;
            Settings.gradientAxis = GradientAxis.Vertical;
        }
    }
}