using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.AsyncTasksManagement
{
    public class TaskManager<TKey>
    {
        private readonly Dictionary<TKey, OneToManyTask> _tasks = new Dictionary<TKey, OneToManyTask>();

        public bool TaskIsRequested(TKey key)
        {
            return _tasks.ContainsKey(key);
        }

        public void RequestTask(TKey key, Task task)
        {
            if (!TaskIsRequested(key))
            {
                var oneToManyTask = new OneToManyTask(key, task);
                oneToManyTask.TaskFinished += delegate(TKey taskKay) { _tasks.Remove(taskKay); };
                _tasks.Add(key, oneToManyTask);
            }
        }

        public Task this[TKey pageNumber] => _tasks[pageNumber].CorrespondingTask;

        private sealed class OneToManyTask
        {
            public event Action<TKey> TaskFinished;

            private readonly TKey _key;

            public Task CorrespondingTask { get; }

            public OneToManyTask(TKey key, Task taskToComplete)
            {
                _key = key;
                CorrespondingTask = taskToComplete;
                CorrespondingTask.ContinueWith(delegate { OnTaskCompleted(); });
            }

            private void OnTaskCompleted()
            {
                OnTaskFinished();
            }

            private void OnTaskFinished()
            {
                TaskFinished?.Invoke(_key);
            }
        }
    }
}