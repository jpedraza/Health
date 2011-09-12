using Health.Core.Entities;
using Health.Core.Entities.POCO;

namespace Health.Core.API.Repository
{
    /// <summary>
    /// Базовый интерфейс репозитория сущности Role.
    /// </summary>
    public interface IRoleRepository : ICoreRepository<Role>
    {
        /// <summary>
        /// Ищет роль в источнике данных по ее имени.
        /// </summary>
        /// <param name="name">Имя роли.</param>
        /// <returns>Найденная роль.</returns>
        Role GetByName(string name);

        /// <summary>
        /// Получить роль по ее идентификатору.
        /// </summary>
        /// <param name="role_id">Идентификатор роли.</param>
        /// <returns>Роль.</returns>
        Role GetById(int role_id);
    }
}