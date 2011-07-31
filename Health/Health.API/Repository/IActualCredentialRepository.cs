using System;
using Health.API.Entities;

namespace Health.API.Repository
{
    /// <summary>
    /// Репозиторий доступа к актуальным данным сессии
    /// </summary>
    public interface IActualCredentialRepository
    {
        /// <summary>
        /// Запись данных
        /// </summary>
        /// <param name="identifier">Идентификатор</param>
        /// <param name="credential">Мандат пользователя</param>
        void Write(string identifier, IUserCredential credential);

        /// <summary>
        /// Чтение данных
        /// </summary>
        /// <param name="identifier">Идентификатор</param>
        /// <returns>Мандат пользователя</returns>
        IUserCredential Read(string identifier);

        /// <summary>
        /// Очистка 
        /// </summary>
        void Clear();
    }
}