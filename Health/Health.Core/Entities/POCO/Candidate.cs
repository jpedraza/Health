namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// Кандидат на регистрацию.
    /// </summary>
    public class Candidate : User
    {
        /// <summary>
        /// Номер полюса.
        /// </summary>
        public string Policy { get; set; }

        /// <summary>
        /// Номер больничной карты.
        /// </summary>
        public string Card { get; set; }

        /// <summary>
        /// Мать пациента
        /// </summary>
        public string Mother { get; set; }

        /// <summary>
        /// Отец пациента
        /// </summary>
        public string Father { get; set; }
    }
}