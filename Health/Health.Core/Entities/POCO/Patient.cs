namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// Пациент.
    /// </summary>
    public class Patient : Candidate
    {
        /// <summary>
        /// Лечащий врач.
        /// </summary>
        public Doctor Doctor { get; set; }
    }
}