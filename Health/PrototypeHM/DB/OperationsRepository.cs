using System;
using System.Collections.Generic;

namespace PrototypeHM.DB
{
    /// <summary>
    /// Контекст операций доступных над объектом.
    /// </summary>
    /// <typeparam name="T">Тип объекта.</typeparam>
    public class OperationsContext<T>
        where T : class
    {
        /// <summary>
        /// Тип объекта который характеризует контекст.
        /// </summary>
        public Type Type
        {
            get { return typeof (T); }
        }

        /// <summary>
        /// Операция загрузки объектов их источника данных.
        /// </summary>
        public Func<IList<T>> Load { get; set; }

        /// <summary>
        /// Операция удаления объекта в источнике данных.
        /// </summary>
        public Func<T, QueryStatus> Delete { get; set; }

        /// <summary>
        /// Операция просмотра детальной информации об объекте.
        /// </summary>
        public Func<T, object> Detail { get; set; }

        /// <summary>
        /// Операция сохранения объекта в источнике данных.
        /// </summary>
        public Func<T, QueryStatus> Save { get; set; }

        /// <summary>
        /// Операция обновления объекта в источнике данных.
        /// </summary>
        public Func<T, QueryStatus> Update { get; set; }
    }

    /// <summary>
    /// Репозиторий операций.
    /// </summary>
    public class OperationsRepository
    {
        public OperationsRepository()
        {
            Operations = new List<object>();
        }

        /// <summary>
        /// Операции.
        /// </summary>
        public List<object> Operations { get; set; }
    }
}
