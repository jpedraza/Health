namespace Health.API.Entities
{
    /// <summary>
    /// Кандидат на регистрацию.
    /// </summary>
    public interface ICandidate : IUser
    {
        /// <summary>
        /// Номер полюса.
        /// </summary>
        string Policy { get; set; }

        /// <summary>
        /// Номер больничной карты.
        /// </summary>
        string Card { get; set; }
    }
}