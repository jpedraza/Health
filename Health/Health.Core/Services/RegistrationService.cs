using System;
using Health.Core.API;
using Health.Core.API.Repository;
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

        public RegistrationService(IDIKernel di_kernel) : base(di_kernel)
        {
            DefaultCandidateRole = di_kernel.Get<IRoleRepository>().GetByName("Patient");
        }

        #region IRegistrationService Members

        /// <summary>
        /// Принять заявку.
        /// </summary>
        /// <param name="candidate">Кандидат.</param>
        public void AcceptBid(Candidate candidate)
        {
            Get<ICandidateRepository>().Delete(candidate);
            if (candidate == null) throw new Exception("Невозможно принять заявку от пустого кандидата.");
            var patient = new Patient
                              {
                                  Id = candidate.Id,
                                  Login = candidate.Login,
                                  Password = candidate.Password,
                                  Card = candidate.Card,
                                  Birthday = candidate.Birthday,
                                  FirstName = candidate.FirstName,
                                  LastName = candidate.LastName,
                                  ThirdName = candidate.ThirdName,
                                  Policy = candidate.Policy,
                                  Token = candidate.Token,
                                  Role = candidate.Role
                              };
            Get<IPatientRepository>().Save(patient);
            Logger.Info(String.Format("Заявка на регистрацию для {0} - принята.", candidate.Login));
        }

        /// <summary>
        /// Сохранить заявку.
        /// </summary>
        /// <param name="candidate">Кандидат.</param>
        public void SaveBid(Candidate candidate)
        {
            candidate.Role = DefaultCandidateRole;
            Get<ICandidateRepository>().Save(candidate);
            Logger.Info(String.Format("Добавлена заявка на регистрацию - {0}.", candidate.Login));
        }

        /// <summary>
        /// Отклонить заявку.
        /// </summary>
        /// <param name="candidate">Кандидат.</param>
        public void RejectBid(Candidate candidate)
        {
            Get<ICandidateRepository>().DeleteById(candidate.Id);
            Logger.Info(String.Format("Заявка на регистрацию для {0} - отклонена.", candidate.Login));
        }

        #endregion
    }
}