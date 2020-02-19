using UnityEngine;

namespace Common.Timers
{
    public sealed class VariableBehaviourTimeline : BaseBehaviourTimeline
    {
        [SerializeField] private float initialTimerInterval;

        protected override void InitializerTimer(out float timerInterval)
        {
            timerInterval = initialTimerInterval;
        }
    }
}