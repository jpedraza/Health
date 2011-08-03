using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Health.API;

namespace Health.Core
{
    public class Logger : ILogger
    {
        private readonly NLog.Logger _logger;

        public Logger(string class_name)
        {
            _logger = NLog.LogManager.GetLogger(class_name);
        }

        #region Implementation of ILogger

        /// <summary>
        /// Записать сообщение в лог.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="level">Уровень.</param>
        public void Log(string message, int level)
        {
            _logger.Log(NLog.LogLevel.FromOrdinal(level), message);
        }

        /// <summary>
        /// Записать сообщение трассировки в лог.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        /// <summary>
        /// Записать сообщение отладки в лог.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        /// <summary>
        /// Записать информационное сообщение в лог.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public void Info(string message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Записать предупреждающее сообщение в лог.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        /// <summary>
        /// Записать сообщение об ошибке в лог.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public void Error(string message)
        {
            _logger.Error(message);
        }

        /// <summary>
        /// Записать критическое сообщение в лог.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        /// <summary>
        /// Включено ли логгирование?
        /// </summary>
        /// <param name="level">Уровень.</param>
        /// <returns>Статус.</returns>
        public bool IsEnabled (int level)
        {
            return _logger.IsEnabled(NLog.LogLevel.FromOrdinal(level));
        }

        /// <summary>
        /// Включен ли лог для сообщений трассировки?
        /// </summary>
        public bool IsTraceEnabled
        {
            get { return _logger.IsTraceEnabled; }
        }

        /// <summary>
        /// Включен ли лог для сообщений отладки?
        /// </summary>
        public bool IsDebugEnabled
        {
            get { return _logger.IsDebugEnabled; } 
        }

        /// <summary>
        /// Включен ли лог для информационных сообщений?
        /// </summary>
        public bool IsInfoEnabled
        {
            get { return _logger.IsInfoEnabled; }
        }

        /// <summary>
        /// Включен ли лог для предупреждющих сообщений?
        /// </summary>
        public bool IsWarnEnabled
        {
            get { return _logger.IsWarnEnabled; }
        }

        /// <summary>
        /// Включен ли лог для сообщений об ошибках?
        /// </summary>
        public bool IsErrorEnabled
        {
            get { return _logger.IsErrorEnabled; }
        }

        /// <summary>
        /// Включен ли лог для критических сообщений?
        /// </summary>
        public bool IsFatalEnabled
        {
            get { return _logger.IsFatalEnabled; }
        }

        #endregion
    }
}
