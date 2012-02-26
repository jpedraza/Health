using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;

namespace Support
{
    internal class SupportManager : IDisposable
    {
        private static SupportManager _dbManager;

        private readonly DbContext _context;

        private readonly IList<_Task> _tasks;

        private SupportManager(DbContext context)
        {
            _context = context;
            _tasks = new List<_Task>();
            IncludePaths = new List<string>();
        }

        public IList<string> IncludePaths { get; private set; }

        #region IDisposable Members

        public void Dispose()
        {
            if (_context.Database.Connection.State == ConnectionState.Open)
            {
                _context.Dispose();
            }
        }

        #endregion

        internal static SupportManager Instance(DbContext context)
        {
            return _dbManager = new SupportManager(context);
        }

        internal static SupportManager Instance()
        {
            return _dbManager;
        }

        public void ExecuteTasks()
        {
            for (int i = 0; i < _tasks.Count; ++i)
            {
                ExecuteTask(i);
            }
        }

        private void ExecuteTask(int i)
        {
            try
            {
                Console.WriteLine("@ Выполнение задачи \"{0}\" ... ({1})", _tasks[i].Name, DateTime.Now);
                DateTime preTime = DateTime.Now;
                _tasks[i].Process(_context);
                Console.WriteLine("+ Задача \"{0}\" выполнена успешно в {1} за {2}.", _tasks[i].Name, DateTime.Now,
                                  DateTime.Now - preTime);
            }
            catch (Exception e)
            {
                Console.WriteLine("- Задача {0} не выполнена! ({1})", _tasks[i].Name, e.Message);
            }
        }

        public void AddTask(string name, ITask task)
        {
            _tasks.Add(new _Task {Name = name, Task = task});
        }

        public void AddIncludePath(string path)
        {
            if (path.StartsWith(@"\"))
            {
                IncludePaths.Add(Directory.GetCurrentDirectory() + path);
            }
            else
            {
                IncludePaths.Add(path);
            }
        }

        public void ProcessAvailableTasks()
        {
            Console.WriteLine("Выберите задачу для выполнения:");
            for (int i = 1; i < _tasks.Count + 1; ++i)
            {
                _Task task = _tasks[i - 1];
                Console.WriteLine("{0}. {1}".F(i.ToString(), task.Name));
            }
            Console.WriteLine("{0}. {1}.".F((_tasks.Count + 1).ToString(), "Все"));
            Console.WriteLine("{0}.".F("Для выхода наберите x"));
            string inc;
            do
            {
                Console.Write("Номер задачи: ");
                int value;
                inc = Console.ReadLine();
                Console.WriteLine();
                if (int.TryParse(inc, out value) && value != default(int))
                {
                    value--;
                    if (value == _tasks.Count)
                    {
                        ExecuteTasks();
                    }
                    else
                    {
                        ExecuteTask(value);
                    }
                }
                Console.WriteLine();
            } while (inc != null && inc.ToLower() != "x");
        }

        #region Nested type: _Task

        private class _Task
        {
            internal string Name { get; set; }
            internal ITask Task { private get; set; }

            internal void Process(DbContext context)
            {
                Task.Process(context);
            }
        }

        #endregion
    }
}