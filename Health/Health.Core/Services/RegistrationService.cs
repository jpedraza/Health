using System;
using Health.API;
using Health.API.Entities;
using Health.API.Services;

namespace Health.Core.Services
{
    /// <summary>
    /// Сервис регистрации.
    /// </summary>
    /// <typeparam name="TCandidate">Тип кандидата.</typeparam>
    public class RegistrationService<TCandidate> : CoreService, IRegistrationService
        where TCandidate : class, ICandidate, new()
    {
        protected RegistrationService(IDIKernel di_kernel, ICoreKernel core_kernel) : base(di_kernel, core_kernel)
        {
            DefaultCandidateRole = Instance<IRole>(o =>
                                                       {
                                                           o.Name = "Patient";
                                                           o.Code = 4;
                                                       });
        }

        protected IRole DefaultCandidateRole;

        #region IRegistrationService<ICandidate> Members

        /// <summary>
        /// Принять заявку.
        /// </summary>
        /// <param name="candidate">Кандидат.</param>
        public void AcceptBid(ICandidate candidate)
        {
            Logger.Info(String.Format("Заявка на регистрацию для {0} - принята.", candidate.Login));
        }

        /// <summary>
        /// Сохранить заявку.
        /// </summary>
        /// <param name="candidate">Кандидат.</param>
        public void SaveBid(ICandidate candidate)
        {
            candidate.Role = DefaultCandidateRole;
            CoreKernel.CandRepo.Save(candidate);
            Logger.Info(String.Format("Добавлена заявка на регистрацию - {0}.", candidate.Login));
        }

        /// <summary>
        /// Отклонить заявку.
        /// </summary>
        /// <param name="candidate">Кандидат.</param>
        public void RejectBid(ICandidate candidate)
        {
            CoreKernel.CandRepo.Delete(candidate);
            Logger.Info(String.Format("Заявка на регистрацию для {0} - отклонена.", candidate.Login));
        }

        #endregion
    }
}