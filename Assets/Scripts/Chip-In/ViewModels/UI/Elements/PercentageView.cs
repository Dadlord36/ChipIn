using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ViewModels.UI.Elements
{
    public class PercentageView : UIBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text textRepresentation;

        private float _percentage;

        public float Percentage
        {
            get => _percentage;
            set
            {
                _percentage = Mathf.Clamp01(value);
                ViewRepresentationPercentage = _percentage;
                TextRepresentationPercentage = _percentage;
            }
        }

        private static float GetFullPercentage(float percentage)
        {
            return Mathf.Lerp(0f, 100f, percentage);
        }

        private float ViewRepresentationPercentage
        {
            get => image.fillAmount;
            set => image.fillAmount = value;
        }

        private float TextRepresentationPercentage
        {
            get => float.Parse(textRepresentation.text);
            set => textRepresentation.text = $"{GetFullPercentage(value).ToString(CultureInfo.InvariantCulture)} %";
        }
    }
}