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
        /// <param name="сandidateId"></param>
        /// <param name="doctorId"></param>
        void AcceptBid(int сandidateId, int doctorId);

        /// <summary>
        /// Сохранить заявку.
        /// </summary>
        /// <param name="candidate">Кандидат на регистрацию.</param>
        void SaveBid(Candidate candidate);

        /// <summary>
        /// Отклонить заявку.
        /// </summary>
        /// <param name="candidateId">Идентификатор кандидата.</param>
        void RejectBid(int candidateId);
    }
}