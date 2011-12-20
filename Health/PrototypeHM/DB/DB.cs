using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PrototypeHM.DB.DI;

namespace PrototypeHM.DB
{
    /// <summary>
    /// Класс для работы с источником данных.
    /// </summary>
    internal class DB : IDIInjected, IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _sqlConnection;

        public DB(string connectionString, IDIKernel diKernel)
        {
            _diKernel = diKernel;
            _connectionString = connectionString;
        }

        /// <summary>
        /// Текущее подключение к базе данных.
        /// </summary>
        internal SqlConnection Connection
        {
            get
            {
                if (_sqlConnection == null) _sqlConnection = new SqlConnection(_connectionString);
                if (_sqlConnection.State == ConnectionState.Closed) _sqlConnection.Open();
                return _sqlConnection;
            }
        }

        #region Implementation of IDIInjected

        private readonly IDIKernel _diKernel;
        public IDIKernel DIKernel
        {
            get { return _diKernel; }
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (_sqlConnection != null && _sqlConnection.State == ConnectionState.Open)
            {
                _sqlConnection.Close();
                _sqlConnection.Dispose();
            }
        }

        #endregion
    }
}
