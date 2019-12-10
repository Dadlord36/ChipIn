using System;

namespace Common
{
    public interface IProgressTracker
    {
        void UpdateProgress(float percentage);
    }
    
    public interface ITimeline
    {
        event Action OnElapsed;
        event Action<float> Progressing;
        bool AutoReset { get; set; }
        void StartTimer();
        void StopTimer();
        void RestartTimer();
    }
}