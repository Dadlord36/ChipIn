using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class QrCodeView : BaseView
    {
        [SerializeField] private Image qrImage;

        public Sprite QrImageSprite
        {
            set => qrImage.sprite = value;
        }
    }
}