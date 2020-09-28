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

        public static Task<T> ExecuteOnMainThreadTaskAsync<T>(Func<T> func)
        {
            return MainThreadTaskFactory.StartNew(func);
        }

        public static T ExecuteOnMainThread<T>(Func<T> func)
        {
            return MainThreadTaskFactory.StartNew(func).GetAwaiter().GetResult();
        }
        
        public static async Task PeriodicAsync(Func<Task> taskFactory, TimeSpan interval, CancellationToken cancellationToken = default)
        {
            while (true)
            {
                var delayTask = Task.Delay(interval, cancellationToken);
                await taskFactory().ConfigureAwait(false);
                await delayTask.ConfigureAwait(false);
            }
        }
    }
}