using UnityEngine;

namespace Common.Timers
{
    public sealed class VariableBehaviourTimeline : BaseBehaviourTimeline
    {
        [SerializeField] private float initialTimerInterval;

        protected override void InitializeTimer(out float timerInterval)
        {
            timerInterval = initialTimerInterval;
        }
    }
}