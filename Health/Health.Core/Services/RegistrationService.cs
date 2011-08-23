using System;
using Health.Core.API;
using Health.Core.API.Services;
using Health.Core.Entities;
using Health.Core.Entities.POCO;

namespace Health.Core.Services
{
    /// <summary>
    /// Сервис регистрации.
    /// </summary>
    public class RegistrationService : CoreService, IRegistrationService
    {
        protected Role DefaultCandidateRole;

        public RegistrationService(IDIKernel di_kernel, ICoreKernel core_kernel) : base(di_kernel, core_kernel)
        {
            DefaultCandidateRole = new Role
                                       {
                                           Name = "Patient",
                                           Code = 4
                                       };
        }

        #region IRegistrationService Members

        /// <summary>
        /// Принять заявку.
        /// </summary>
        /// <param name="candidate">Кандидат.</param>
        public void AcceptBid(Candidate candidate)
        {
            Logger.Info(String.Format("Заявка на регистрацию для {0} - принята.", candidate.Login));
        }

        /// <summary>
        /// Сохранить заявку.
        /// </summary>
        /// <param name="candidate">Кандидат.</param>
        public void SaveBid(Candidate candidate)
        {
            candidate.Role = DefaultCandidateRole;
            CoreKernel.CandRepo.Save(candidate);
            Logger.Info(String.Format("Добавлена заявка на регистрацию - {0}.", candidate.Login));
        }

        /// <summary>
        /// Отклонить заявку.
        /// </summary>
        /// <param name="candidate">Кандидат.</param>
        public void RejectBid(Candidate candidate)
        {
            CoreKernel.CandRepo.Delete(candidate);
            Logger.Info(String.Format("Заявка на регистрацию для {0} - отклонена.", candidate.Login));
        }

        #endregion
    }
}