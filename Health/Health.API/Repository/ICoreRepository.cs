using System.Collections.Generic;
using Health.API.Entities;
using Ninject;

namespace Health.API.Repository
{
    /// <summary>
    /// Базовый обобщенный интерфейс для репозиториев
    /// </summary>
    /// <typeparam name="TEntity">Реализует интерфейс IEntity</typeparam>
    public interface ICoreRepository<TEntity> : ICore
        where TEntity : IEntity
    {
        /// <summary>
        /// Получает все сущности из источника данных
        /// </summary>
        /// <returns>Список сущностей</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Сохраняет сущность в источнике данных
        /// </summary>
        /// <param name="entity">Сущность, реализующая IEntity</param>
        /// <returns>Результат сохранения</returns>
        bool Save(TEntity entity);

        /// <summary>
        /// Удаляет сущность из источника данных
        /// </summary>
        /// <param name="entity">Сущность, реализующая IEntity</param>
        /// <returns>Результат удаления</returns>
        bool Delete(TEntity entity);
    }
}