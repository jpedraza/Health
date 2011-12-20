using System;
using System.Collections.Generic;
using System.Linq;

namespace PrototypeHM.DB
{
    /// <summary>
    /// Репозиторий запросов к базе данных.
    /// </summary>
    internal class QueryRepository
    {
        /// <summary>
        /// Контекст запроса.
        /// </summary>
        private class QueryContext 
        {
            /// <summary>
            /// Имя запроса.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Строка запроса.
            /// </summary>
            public string Query { get; set; }
        }

        /// <summary>
        /// Контексты запросов.
        /// </summary>
        private readonly IList<QueryContext> _queryContexts;

        public QueryRepository()
        {
            _queryContexts = new List<QueryContext>();
        }

        /// <summary>
        /// Добавить запрос.
        /// </summary>
        /// <param name="name">Имя запроса.</param>
        /// <param name="query">Запрос.</param>
        internal void Add(string name, string query)
        {
            if (_queryContexts.Where(c => c.Name == name).Count() == 0)
            {
                _queryContexts.Add(new QueryContext { Name = name, Query = query });
            }
            else
            {
                throw new Exception("Команда с таким именем уже существует.");
            }
        }

        /// <summary>
        /// Добавить процедуру.
        /// </summary>
        /// <param name="name">Имя процедуры.</param>
        internal void AddProcedure(string name)
        {
            Add(name, string.Format("exec [dbo].[{0}]", name));
        }

        /// <summary>
        /// Получить запрос по имени.
        /// </summary>
        /// <param name="name">Имя запроса.</param>
        /// <returns>Запрос.</returns>
        internal string Get(string name)
        {
            QueryContext query = _queryContexts.Where(c => c.Name == name).FirstOrDefault();
            if (query == null)
            {
                throw new Exception("Данная команда отсутсвтует в репозитории.");
            }
            return query.Query;
        }

        /// <summary>
        /// Получить запрос с параметрами.
        /// </summary>
        /// <param name="name">Имя запроса.</param>
        /// <param name="params">Параметры запроса.</param>
        /// <returns>Запрос.</returns>
        internal string Get(string name, Dictionary<string, string> @params)
        {
            string commandText = Get(name);
            const string format = "[{0}]";
            foreach (KeyValuePair<string, string> param in @params)
            {
                commandText = commandText.Replace(string.Format(format, param.Key), param.Value);
            }
            return commandText;
        }

        /// <summary>
        /// Возможно ли получить запрос с заданным именем?
        /// </summary>
        /// <param name="name">Имя запроса.</param>
        /// <param name="query">Запрос.</param>
        /// <returns>Существует ли запрос с заданным именем.</returns>
        internal bool TryGet(string name, out string query)
        {
            QueryContext comm = _queryContexts.Where(c => c.Name == name).FirstOrDefault();
            if (comm == null)
            {
                throw new Exception("Данная команда отсутсвтует в репозитории.");
            }
            query = comm.Query;
            return true;
        }
    }
}
