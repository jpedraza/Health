using Health.API.Entities;

namespace Health.API.Services
{
    /// <summary>
    /// Сервис регистрации.
    /// </summary>
    public interface IRegistrationService : ICoreService
    {
        /// <summary>
        /// Принять заявку.
        /// </summary>
        /// <param name="candidate">Кандидат на регистрацию.</param>
        void AcceptBid(ICandidate candidate);

        /// <summary>
        /// Сохранить заявку.
        /// </summary>
        /// <param name="candidate">Кандидат на регистрацию.</param>
        void SaveBid(ICandidate candidate);

        /// <summary>
        /// Отклонить заявку.
        /// </summary>
        /// <param name="candidate">Кандидат на регистрацию.</param>
        void RejectBid(ICandidate candidate);
    }
}