using ScriptableObjects.Parameters;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common.Timers
{
    public sealed class ParametricBehaviourTimeline : BaseBehaviourTimeline
    {
        [SerializeField] private FloatParameter intervalParameter;

        protected override void InitializeTimer(out float timerInterval)
        {
            Assert.IsNotNull(intervalParameter, $"There is no {nameof(FloatParameter)} on: {name}");
            timerInterval = intervalParameter.value;
        }
    }
}