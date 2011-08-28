using Health.Core.Entities;
using Health.Core.Entities.POCO;

namespace Health.Core.API.Repository
{
    /// <summary>
    /// Репозиторий для параметров.
    /// </summary>
    public interface IParameterRepository : ICoreRepository<Parameter>
    {
        /// <summary>
        /// Получить параметр по его названию.
        /// </summary>
        /// <param name="value">Название параметра/</param>
        /// <returns>Параметр здоровья с заданным названием/</returns>
        Parameter GetByValue(string value);

        /// <summary>
        /// Получить параметр по его Id.
        /// </summary>
        /// <param name="Id">Id параметра.</param>
        /// <returns>Параметр здоровья с заданным названием/</returns>
        Parameter GetById(int Id);

        /// <summary>
        /// Получить все параметры здоровья
        /// </summary>
        /// <returns>Все параметры здоровья человека/</returns>
        Parameter[] GetAllParam();

        /// <summary>
        /// Добавить новый параметр здоровья человека
        /// </summary>
        /// /// <param name="NewParam">Новый параметр.</param>
        /// <returns>Результат добавления нового параметра</returns>
        bool Add(Parameter NewParam);

        /// <summary>
        /// Удалить существующий параметр здоровья человека
        /// </summary>
        /// /// <param name="Id">Id параметра.</param>
        /// <returns>Результат удаления нового параметра</returns>
        bool Delete(int Id);

        /// <summary>
        /// Редактировать параметр здоровья человека
        /// </summary>
        /// /// <param name="NewParam">Редактируемый параметр.</param>
        /// <returns>Результат редактирования нового параметра</returns>
        bool Edit(Parameter Param);
    }
}