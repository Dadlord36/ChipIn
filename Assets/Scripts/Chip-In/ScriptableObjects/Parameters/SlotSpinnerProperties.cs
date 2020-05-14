using UnityEngine;

namespace ScriptableObjects.Parameters
{
    [CreateAssetMenu(fileName = nameof(SlotSpinnerProperties),
        menuName = nameof(Parameters) + "/" + nameof(SlotSpinnerProperties), order = 0)]
    public class SlotSpinnerProperties : ScriptableObject
    {
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
    }
}