using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.AsyncTasksManagement
{
    public class TaskManager<TKey>
    {
        private readonly Dictionary<TKey, Task> _tasks = new Dictionary<TKey, Task>();

        private bool TaskIsRequested(TKey key)
        {
            return _tasks.ContainsKey(key);
        }

        public Task RequestTask(TKey key, Task task)
        {
            if (!TaskIsRequested(key))
            {
                _tasks.Add(key, task);
            }

            return _tasks[key];
        }
    }
}