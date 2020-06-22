using Repositories.Remote;
using ScriptableObjects.Parameters;
using UnityEngine;
using UnityEngine.UI;
using WebOperationUtilities;

namespace Views
{
    public sealed class QrCodeView : BaseView
    {
        [SerializeField] private Image qrImage;
        [SerializeField] private QrCodeEncodingParameters qrCodeParameters;
        [SerializeField] private UserProductsRepository userProductsRepository;

        [SerializeField] private CodeWriter codeWriter;

        public QrCodeView() : base(nameof(QrCodeView))
        {
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            CodeWriter.onCodeEncodeFinished += CodeWriterOnCodeEncodeFinished;
            QrCode = userProductsRepository.CurrentlySelectedProduct.QrData;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            CodeWriter.onCodeEncodeFinished -= CodeWriterOnCodeEncodeFinished;
        }

        private void CodeWriterOnCodeEncodeFinished(Texture2D tex)
        {
            QrImageSprite = SpritesUtility.CreateSpriteWithDefaultParameters(tex);
        }

        private Sprite QrImageSprite
        {
            set => qrImage.sprite = value;
        }

        public string QrCode
        {
            set => codeWriter.CreateCode(qrCodeParameters.Parameters.codingFormat, qrCodeParameters.Parameters.height, value);
        }
    }
}