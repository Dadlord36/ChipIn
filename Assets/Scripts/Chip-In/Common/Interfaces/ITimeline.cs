using System;
using ActionsTranslators;

namespace Common.Interfaces
{
    public interface IProgressTracker
    {
        void UpdateProgress(float percentage);
    }

    public interface ITimeline : IInitialize, IUpdatable
    {
        float Interval { get; set; }
        event Action Elapsed;
        event Action<float> Progressing;
        bool AutoReset { get; set; }
        void StartTimer();
        void StartTimer(float interval);
        
        void StopTimer();
        void RestartTimer();
    }
}