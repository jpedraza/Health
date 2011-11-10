using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Health.Core.Entities.POCO;

namespace Health.Core.API.Repository
{
    /// <summary>
    /// Репозиторий для диагнозов 
    /// (по международной классификации)
    /// </summary>
    public interface IDiagnosisRepository:ICoreRepository<Diagnosis>
    {
        /// <summary>
        /// Получить диагноз по его Id
        /// </summary>
        /// <param name="id">id диагноза</param>
        /// <returns>Дигноз</returns>
        Diagnosis GetById(int id);

        /// <summary>
        /// Получить дигноз по его коду
        /// </summary>
        /// <param name="code">Код диагноза</param>
        /// <returns>Диагноз</returns>
        Diagnosis GetByCode(string code);

        /// <summary>
        /// Получить все дигнозы
        /// </summary>
        /// <returns>Список дигнозов</returns>
        IList<Diagnosis> GetAllDiagnosises();

        /// <summary>
        /// Добавление нового диагноза
        /// </summary>
        /// <param name="newDiagnosis">Новый дигноз</param>
        /// <returns>Результат добавления (true - успешно добавлен, false - не добавлен, 
        /// где-то произошла ошибка)</returns>
        bool Add(Diagnosis newDiagnosis);

        /// <summary>
        /// Редактирование дигноза
        /// </summary>
        /// <param name="editingDiagnosis">Редактируемый диагноз</param>
        /// <returns>Результат редактирования
        /// (true - успешно отредактирован, false - не отредактирован, 
        /// где-то произошла ошибка)
        /// </returns>
        bool Edit(Diagnosis editingDiagnosis);

        /// <summary>
        /// Удаление дигноза по его Id
        /// </summary>
        /// <param name="id">Id диагноза</param>
        /// <returns>Результат удаления
        /// (true - успешно удален, false - не удален, 
        /// где-то произошла ошибка)</returns>
        bool DeleteById(int id);

        /// <summary>
        /// Удаление дигноза
        /// </summary>
        /// <param name="deleteDiagnosis">Удаление дигноза</param>
        /// <returns>Результат удаления
        /// (true - успешно удален, false - не удален, 
        /// где-то произошла ошибка)</returns>
        bool DeleteByExamp(Diagnosis deleteDiagnosis);
    }
}
