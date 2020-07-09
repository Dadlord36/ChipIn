using System;
using UnityEngine;

namespace ScriptableObjects.Parameters
{
    [CreateAssetMenu(fileName = nameof(LineEngineParameters),
        menuName = nameof(Parameters) + "/" + nameof(LineEngineParameters), order = 0)]
    public class LineEngineParameters : ScriptableObject
    {
        [Serializable]
        public struct AnchorPivotData
        {
            public Vector2 pivot;
            public Vector2 anchorMin;
            public Vector2 anchorMax;

            public AnchorPivotData(in Vector2 pivot, in Vector2 anchorMin, in Vector2 anchorMax)
            {
                this.pivot = pivot;
                this.anchorMin = anchorMin;
                this.anchorMax = anchorMax;
            }
        }


        [SerializeField] private AnchorPivotData anchorPivotData;
        [SerializeField] private float spinTime;
        [SerializeField] private float itemLength;
        [SerializeField] private float offsetBetweenItems;
        [SerializeField] private uint laps = 1;
        [SerializeField] private uint controlItemIndex;
        [SerializeField] private AnimationCurve speedCurve;
        [Range(0f, 360f)] [SerializeField] private float movementAngle;


        public float SpinTime => spinTime;
        public float OffsetBetweenItems => offsetBetweenItems;
        
        public uint Laps => laps;

        public float ItemLength => itemLength;

        public uint ControlItemIndex => controlItemIndex;

        public AnimationCurve SpeedCurve => speedCurve;

        public float MovementAngle => movementAngle;

        public AnchorPivotData AnchorPivot => anchorPivotData;
    }
}