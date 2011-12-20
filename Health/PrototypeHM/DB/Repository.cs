using System.Collections.Generic;
using System.Data.SqlClient;
using PrototypeHM.DB.DI;

namespace PrototypeHM.DB
{
    /// <summary>
    /// Базовый класс для репозиториев.
    /// </summary>
    internal abstract class Repository : IDIInjected
    {
        /// <summary>
        /// Создать запрос.
        /// </summary>
        /// <param name="query">Запрос.</param>
        /// <returns>Объект запроса к базе.</returns>
        protected SqlCommand CreateQuery(string query)
        {
            SqlCommand sqlCommand = DIKernel.Get<DB>().Connection.CreateCommand();
            sqlCommand.CommandText = query;
            return sqlCommand;
        }

        #region Implementation of IDIInjected

        private readonly IDIKernel _diKernel;
        public IDIKernel DIKernel
        {
            get { return _diKernel; }
        }

        #endregion

        protected Repository(IDIKernel diKernel)
        {
            _diKernel = diKernel;
        }

        /// <summary>
        /// Получить объект из DI-ядра.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <returns>Экземпляр объекта.</returns>
        protected T Get<T>()
        {
            return DIKernel.Get<T>();
        }

        /// <summary>
        /// Поучить запрос из репозитория по имени запроса.
        /// </summary>
        /// <param name="queryName">Имя запроса.</param>
        /// <returns>Запрос.</returns>
        protected string GetQueryText(string queryName)
        {
            return Get<QueryRepository>().Get(queryName);
        }

        /// <summary>
        /// Поучить запрос с параметрами из репозитория по имени запроса.
        /// </summary>
        /// <param name="queryName">Имя запроса.</param>
        /// <param name="params">Параметры.</param>
        /// <returns>Запрос.</returns>
        protected string GetQueryText(string queryName, Dictionary<string, string> @params)
        {
            return Get<QueryRepository>().Get(queryName, @params);
        }
    }
}
