using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Coroutines
{
    public class StepTask
    {
        public static class Manager
        {
            private readonly static List<StepTask> tasks = new();
            private readonly static Stack<IEnumerator> updateBuffer = new();

            public static void Update()
            {
                for (int i = tasks.Count - 1; i >= 0; i--)
                {
                    updateBuffer.Clear();

                    StepTask task = tasks[i];

                    try
                    {
                        if (!MoveNext(task.Iterator))
                        {
                            task.Complete();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new AggregateException($"({nameof(StepTask)} inner exception) {ex.GetType()}: {ex.Message}");
                    }
                }
            }

            private static bool MoveNext(IEnumerator mainTask)
            {
                Stack<IEnumerator> nestedTasks = updateBuffer;

                //finding nested tasks
                IEnumerator task = mainTask;
                while (task.Current is IEnumerator subTask)
                {
                    task = subTask;

                    if (subTask != null)
                        nestedTasks.Push(task);
                }

                //updating nested tasks
                while (nestedTasks.Count > 0)
                {
                    if (nestedTasks.Peek().MoveNext())
                    {
                        return true;
                    }

                    nestedTasks.Pop();
                }

                //all nested tasks are completed
                return mainTask.MoveNext();
            }

            public static void Register(StepTask task) => tasks.Add(task);
            public static void Unregister(StepTask task) => tasks.Remove(task);
        }

        public static YieldInstruction Yields { get; } = new();

        public Func<IEnumerator> IteratorFactory { get; set; }
        public IEnumerator Iterator { get; private set; } = null;
        public bool IsRunning => Iterator != null;

        public event Action Completed;

        public StepTask(Func<IEnumerator> factory)
        {
            IteratorFactory = factory;
        }

        public void Start()
        {
            Break();

            if (IteratorFactory == null)
                return;

            Iterator = IteratorFactory.Invoke();

            if (Iterator == null)
                return;

            Manager.Register(this);
        }
        public void Break()
        {
            if (!IsRunning)
                return;

            Iterator = null;
            Manager.Unregister(this);
        }
        public void Complete()
        {
            Break();
            Completed?.Invoke();
        }

        public static StepTask Run(Func<IEnumerator> factory)
        {
            StepTask task = new(factory);
            task.Start();
            return task;
        }
    }
}
