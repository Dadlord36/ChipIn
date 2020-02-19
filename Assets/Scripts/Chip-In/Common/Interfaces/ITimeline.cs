using System;

namespace Common.Interfaces
{
    public interface IProgressTracker
    {
        void UpdateProgress(float percentage);
    }
    
    public interface ITimeline : IInitialize
    {
        event Action OnElapsed;
        event Action<float> Progressing;
        bool AutoReset { get; set; }
        void StartTimer();
        void StopTimer();
        void RestartTimer();
    }
}