using System;
using UnityEngine;

namespace ScriptableObjects.Parameters
{
    [CreateAssetMenu(fileName = nameof(LineEngineProperties),
        menuName = nameof(Parameters) + "/" + nameof(LineEngineProperties), order = 0)]
    public class LineEngineProperties : ScriptableObject
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
        [SerializeField] private float offset;
        [SerializeField] private uint laps = 1;
        [SerializeField] private uint controlItemIndex;
        [SerializeField] private AnimationCurve speedCurve;
        [Range(0f, 360f)] [SerializeField] private float movementAngle;


        public float SpinTime => spinTime;

        public float Offset => offset;

        public uint Laps => laps;

        public uint ControlItemIndex => controlItemIndex;

        public AnimationCurve SpeedCurve => speedCurve;

        public float MovementAngle => movementAngle;

        public AnchorPivotData AnchorPivot => anchorPivotData;
    }
}