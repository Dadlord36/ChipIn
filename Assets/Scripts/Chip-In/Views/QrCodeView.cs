using ScriptableObjects.Parameters;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using ZXing.QrCode;

namespace Views
{
    public class QrCodeView : BaseView
    {
        [SerializeField] private Image qrImage;
        [SerializeField] private QrCodeEncodingParameters qrCodeParameters;
        private readonly QRCodeWriter _codeWriter = new QRCodeWriter();


        private Sprite QrImageSprite
        {
            set => qrImage.sprite = value;
        }

        public string QrCode
        {
            set
            {
                var bitMatrix = _codeWriter.encode(value, qrCodeParameters.Parameters.codingFormat, qrCodeParameters.Parameters.width,
                    qrCodeParameters.Parameters.height);
                QrImageSprite = QrCodeUtility.ConvertBitMatrixToSprite(bitMatrix);
            }
        }
    }
}