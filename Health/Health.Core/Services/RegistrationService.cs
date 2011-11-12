using System;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.API.Services;
using Health.Core.Entities.POCO;

namespace Health.Core.Services
{
    /// <summary>
    /// Сервис регистрации.
    /// </summary>
    public class RegistrationService : CoreService, IRegistrationService
    {
        protected Role DefaultCandidateRole;

        public RegistrationService(IDIKernel diKernel) : base(diKernel)
        {
            DefaultCandidateRole = diKernel.Get<IRoleRepository>().GetByName("Patient");
        }

        #region IRegistrationService Members

        /// <summary>
        /// Принять заявку.
        /// </summary>
        /// <param name="candidateId"></param>
        /// <param name="doctorId"></param>
        public void AcceptBid(int candidateId, int doctorId)
        {
            Candidate candidate = Get<ICandidateRepository>().GetById(candidateId);
            if (candidate == null) throw new Exception("Данный кандидат отсутствует.");
            Doctor doctor = Get<IDoctorRepository>().GetById(doctorId);
            if (candidate == null) throw new Exception("Данный доктор отсутствует.");
            Get<ICandidateRepository>().DeleteById(candidateId);
            var patient = new Patient
                              {
                                  Id = candidate.Id,
                                  Login = candidate.Login,
                                  Password = candidate.Password,
                                  //Card = candidate.Card,
                                  Birthday = candidate.Birthday,
                                  FirstName = candidate.FirstName,
                                  LastName = candidate.LastName,
                                  ThirdName = candidate.ThirdName,
                                  //Policy = candidate.Policy,
                                  Token = candidate.Token,
                                  Role = candidate.Role,
                                  Doctor = doctor
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
        /// <param name="candidateId">Идентификатор кандидата.</param>
        public void RejectBid(int candidateId)
        {
            Get<ICandidateRepository>().DeleteById(candidateId);
            Logger.Info(String.Format("Заявка на регистрацию для {0} - отклонена.", candidateId));
        }

        #endregion
    }
}