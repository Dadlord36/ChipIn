using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tasking
{
    public static class TasksFactories
    {
        public static readonly TaskFactory MainThreadTaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.DenyChildAttach,
            TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

        public static void ExecuteOnMainThread(Action action)
        {
            MainThreadTaskFactory.StartNew(action).GetAwaiter().GetResult();
        }

        public static T ExecuteOnMainThread<T>(Func<T> func)
        {
            return MainThreadTaskFactory.StartNew(func).GetAwaiter().GetResult();
        }
    }
}