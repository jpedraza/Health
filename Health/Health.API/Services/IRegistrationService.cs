using Health.API.Entities;
using Health.API.Repository;

namespace Health.API.Services
{
    /// <summary>
    /// Сервис регистрации
    /// </summary>
    /// <typeparam name="TCandidate">Сущность кандидат</typeparam>
    public interface IRegistrationService<TCandidate> : ICore
        where TCandidate : ICandidate
    {
        /// <summary>
        /// Принять заявку 
        /// </summary>
        /// <param name="candidate">Кандидат на регистрацию</param>
        void AcceptBid(ICandidate candidate);

        /// <summary>
        /// Сохранить заявку
        /// </summary>
        /// <param name="candidate">Кандидат на регистрацию</param>
        void SaveBid(ICandidate candidate);

        /// <summary>
        /// Отклонить заявку
        /// </summary>
        /// <param name="candidate">Кандидат на регистрацию</param>
        void RejectBid(ICandidate candidate);
    }
}