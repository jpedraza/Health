using Health.API.Entities;
using Health.API.Services;

namespace Health.Core.Services
{
    /// <summary>
    /// Сервис регистрации.
    /// </summary>
    /// <typeparam name="TCandidate">Тип кандидата.</typeparam>
    public class RegistrationService<TCandidate> : CoreService, IRegistrationService<ICandidate>
        where TCandidate : class, ICandidate, new()
    {
        #region IRegistrationService<ICandidate> Members

        /// <summary>
        /// Принять заявку.
        /// </summary>
        /// <param name="candidate">Кандидат.</param>
        public void AcceptBid(ICandidate candidate)
        {
        }

        /// <summary>
        /// Сохранить заявку.
        /// </summary>
        /// <param name="candidate">Кандидат.</param>
        public void SaveBid(ICandidate candidate)
        {
            CoreKernel.CandRepo.Save(candidate);
        }

        /// <summary>
        /// Отклонить заявку.
        /// </summary>
        /// <param name="candidate">Кандидат.</param>
        public void RejectBid(ICandidate candidate)
        {
            CoreKernel.CandRepo.Delete(candidate);
        }

        #endregion
    }
}