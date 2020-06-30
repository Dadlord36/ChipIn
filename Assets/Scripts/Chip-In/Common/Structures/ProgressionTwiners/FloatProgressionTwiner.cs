using UnityEngine;

namespace Common.Structures.ProgressionTwiners
{
    public struct FloatProgressionTwiner
    {
        private float _initial, _target, _current;
        public float CurrentValue => _current;

        public FloatProgressionTwiner(float initial, float target) : this()
        {
            _initial = initial;
            _target = target;
        }

        public float PreProgress(in float difference)
        {
            _current += difference;
            return Mathf.InverseLerp(_initial, _target, _current);
        }
    }
}