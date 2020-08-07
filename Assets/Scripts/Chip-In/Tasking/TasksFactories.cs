using System.Threading;
using System.Threading.Tasks;

namespace Tasking
{
    public static class TasksFactories
    {
        public static readonly TaskFactory MainThreadTaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.DenyChildAttach,
            TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
    }
}