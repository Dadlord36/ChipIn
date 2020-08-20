using System;
using ScriptableObjects.Parameters;
using UnityEngine;

namespace Shapes2D
{
    public class ShapeWithParametricGradient : Shape
    {
        [SerializeField] private ColorsPairParameter gradientColoursValues;

        private Color FirstColor
        {
            get => settings.fillColor;
            set => settings.fillColor = value;
        }

        private Color SecondColor
        {
            get => settings.fillColor2;
            set => settings.fillColor2 = value;
        }

        public ShapeWithParametricGradient()
        {
            settings.blur = 0.1f;
            settings.fillType = FillType.Gradient;
            settings.gradientType = GradientType.Linear;
            settings.gradientAxis = GradientAxis.Vertical;
            settings.fillRotation = 180f;
        }

        private void SetColorValuesFromSource()
        {
            FirstColor = gradientColoursValues.value1;
            SecondColor = gradientColoursValues.value2;
        }
        
        private void OnEnable()
        {
            SetColorValuesFromSource();
        }
    }
}