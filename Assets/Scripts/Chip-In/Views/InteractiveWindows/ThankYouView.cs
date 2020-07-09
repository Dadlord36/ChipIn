using Shapes2D;
using TMPro;
using UnityEngine;
using UnityWeld.Binding;

namespace Views.InteractiveWindows
{
    [Binding]
    public class ThankYouView : BaseView
    {
        [SerializeField] private TMP_Text productNameTextField;
        [SerializeField] private Shape productIconShape;

        [Binding]
        public string ProductName
        {
            get => productNameTextField.text;
            set => productNameTextField.text = value;
        }

        [Binding]
        public Texture2D ProductIcon
        {
            get => productIconShape.settings.fillTexture;
            set => productIconShape.settings.fillTexture = value;
        }

        public ThankYouView() : base(nameof(ThankYouView))
        {
        }
    }
}