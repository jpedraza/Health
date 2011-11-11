using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Health.Core.Entities.POCO;

namespace Health.Core.API.Repository
{
    /// <summary>
    /// Репозиторий для видов медицинских операций/Лечений
    /// </summary>
    public interface ISurgeryRepository : ICoreRepository<Surgery>
    {
        /// <summary>
        /// Получить операцию/лечение по ее Id
        /// </summary>
        /// <param name="id">id операции/лечения</param>
        /// <returns>Операция/лечения</returns>
        Surgery GetById(int id);
              
        /// <summary>
        /// Получить все операции/лечения
        /// </summary>
        /// <returns>Список всех операций/лечений</returns>
        IList<Surgery> GetAllSurgerys();

        /// <summary>
        /// Добавление нового операции/лечения
        /// </summary>
        /// <param name="newSurgery">Новая операция/лечение</param>
        /// <returns>Результат добавления (true - успешно добавлен, false - не добавлен, 
        /// где-то произошла ошибка)</returns>
        bool Add(Surgery newSurgery);

        /// <summary>
        /// Редактирование операции/лечения
        /// </summary>
        /// <param name="editingSurgery">Редактируемая операция/лечение</param>
        /// <returns>Результат редактирования
        /// (true - успешно отредактирован, false - не отредактирован, 
        /// где-то произошла ошибка)
        /// </returns>
        bool Edit(Surgery editingSurgery);

        /// <summary>
        /// Удаление операции/лечения по ее Id
        /// </summary>
        /// <param name="id">Id операции/лечения</param>
        /// <returns>Результат удаления
        /// (true - успешно удален, false - не удален, 
        /// где-то произошла ошибка)</returns>
        bool DeleteById(int id);

        /// <summary>
        /// Удаление операции/лечения
        /// </summary>
        /// <param name="deleteSurgery">Удаляемая операция/лечение</param>
        /// <returns>Результат удаления
        /// (true - успешно удален, false - не удален, 
        /// где-то произошла ошибка)</returns>
        bool DeleteByExamp(Surgery deleteSurgery);
    }
}
