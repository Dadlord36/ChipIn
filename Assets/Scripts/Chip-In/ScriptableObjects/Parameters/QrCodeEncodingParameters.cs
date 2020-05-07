using System;
using UnityEngine;
using ZXing;

namespace ScriptableObjects.Parameters
{
    [Serializable]
    public struct QrCodeWriterParameters
    {
        public CodeWriter.CodeType codingFormat;
        public int width;
        public int height;
    }

    [CreateAssetMenu(fileName = nameof(Parameters), menuName = nameof(Parameters) + "/" + nameof(QrCodeEncodingParameters), order = 0)]
    public class QrCodeEncodingParameters : ScriptableObject
    {
        [SerializeField] private QrCodeWriterParameters parameters;

        public QrCodeWriterParameters Parameters => parameters;
    }
}