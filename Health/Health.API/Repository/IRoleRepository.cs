using Health.API.Entities;

namespace Health.API.Repository
{
    /// <summary>
    /// Базовый интерфейс репозитория сущности Role
    /// </summary>
    public interface IRoleRepository<TRole> : ICoreRepository<TRole>
        where TRole : class, IRole
    {
        /// <summary>
        /// Ищет роль в источнике данных по ее имени
        /// </summary>
        /// <param name="name">Имя роли</param>
        /// <returns>Найденная роль</returns>
        TRole GetByName(string name);
    }
}