using System;

namespace Common.Interfaces
{
    public interface INotifyProgressReachesEnd
    {
        event Action ProgressReachesEnd;
    }
}