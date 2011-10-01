using Health.Core.Entities;
using Health.Core.Entities.POCO;

namespace Health.Core.API.Services
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
        /// <param name="doctor">Доктор.</param>
        void AcceptBid(Candidate candidate, Doctor doctor);

        /// <summary>
        /// Сохранить заявку.
        /// </summary>
        /// <param name="candidate">Кандидат на регистрацию.</param>
        void SaveBid(Candidate candidate);

        /// <summary>
        /// Отклонить заявку.
        /// </summary>
        /// <param name="candidate">Кандидат на регистрацию.</param>
        void RejectBid(Candidate candidate);
    }
}