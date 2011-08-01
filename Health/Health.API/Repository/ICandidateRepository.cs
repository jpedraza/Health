using Health.API.Entities;

namespace Health.API.Repository
{
    /// <summary>
    /// Интерфейс репозитория кандидатов на регистрацию.
    /// </summary>
    /// <typeparam name="TCandidate">Тип кандидата.</typeparam>
    public interface ICandidateRepository<TCandidate> : ICoreRepository<TCandidate>
        where TCandidate : class, ICandidate
    {
        /// <summary>
        /// Роль кандидата по-умолчанию.
        /// </summary>
        IRole DefaultCandidateRole { get; set; }
    }
}