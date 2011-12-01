using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace Support
{
    internal class SupportManager : IDisposable
    {
        private class _Task
        {
            internal string Name { get; set; }
            internal ITask Task { get; set; }
            internal void Process(SqlConnection connection) { Task.Process(connection); }
        }

        public IList<string> IncludePaths { get; private set; }
        private static SupportManager _dbManager;
        private string _connectionString;
        public string ConnectionString 
        {
            get { return _connectionString; }
            set
            {
                _connectionString = value;
                Connection = new SqlConnection(_connectionString);
            }
        }
        private SqlConnection _connection;
        public SqlConnection Connection 
        {
            get
            {
                if (_connection == null) _connection = new SqlConnection(ConnectionString);
                OpenConnection();
                return _connection;
            }
            private set
            {
                _connection = value;
            }
        }
        private IList<_Task> _tasks;

        internal static SupportManager Instance()
        {
            if (_dbManager == null) _dbManager = new SupportManager();
            return _dbManager;
        }

        private SupportManager()
        {
            _tasks = new List<_Task>();
            IncludePaths = new List<string>();
        }

        private void OpenConnection() 
        {
            if (_connection.State == System.Data.ConnectionState.Closed) _connection.Open();
        }

        private void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open) _connection.Close();
        }

        public void ExecuteTasks()
        {
            for (int i = 1; i < _tasks.Count + 1; ++i)
            {
                ExecuteTask(i);
            }
        }

        private void ExecuteTask(int i)
        {
            try
            {
                Console.WriteLine("@ Начало выполнения задачи {0} ...", _tasks[i].Name);
                _tasks[i].Process(Connection);
                Console.WriteLine("+ Задача {0} выполнена успешно.", _tasks[i].Name);
            }
            catch (Exception e)
            {
                Console.WriteLine("- Задача {0} не выполнена! ({1})", _tasks[i].Name, e.Message);
            }
        }

        public void AddTask(string name, ITask task)
        {
            _tasks.Add(new _Task { Name = name, Task = task });
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

        public void Dispose()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Dispose();
                _connection.Close();
            }
        }

        public void ProcessAvailableTasks()
        {
            Console.WriteLine("Выберите задачу для выполнения:");
            for (int i = 1; i < _tasks.Count + 1; ++i)
            {
                _Task task = _tasks[i - 1];
                Console.WriteLine("{0}. {1}".f(i.ToString(), task.Name));
            }
            Console.WriteLine("{0}. {1}.".f((_tasks.Count + 1).ToString(), "Все"));
            Console.WriteLine("{0}.".f("Для выхода наберите x"));
            string inc = String.Empty;
            do
            {
                Console.Write("Номер задачи: ");
                int value;
                inc = Console.ReadLine();
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
            }
            while (inc.ToLower() != "x");
        }
    }
}
