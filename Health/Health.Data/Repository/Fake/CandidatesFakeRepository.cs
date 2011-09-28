using System;
using System.Linq;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities;
using Health.Core.Entities.POCO;

namespace Health.Data.Repository.Fake
{
    public sealed class CandidatesFakeRepository : CoreFakeRepository<Candidate>, ICandidateRepository
    {
        public CandidatesFakeRepository(IDIKernel di_kernel) : base(di_kernel)
        {
            Save(new Candidate
                     {
                         Birthday = new DateTime(1980, 12, 2),
                         Card = "some card number",
                         FirstName = "cand1",
                         LastName = "cand1",
                         Login = "cand1",
                         Password = "cand1",
                         Policy = "some policy number",
                         Role = di_kernel.Get<IRoleRepository>().GetByName("Patient"),
                         ThirdName = "cand1"
                     });
            Save(new Candidate
                     {
                         Birthday = new DateTime(1980, 12, 2),
                         Card = "some card number",
                         FirstName = "cand2",
                         LastName = "cand2",
                         Login = "cand2",
                         Password = "cand2",
                         Policy = "some policy number",
                         Role = di_kernel.Get<IRoleRepository>().GetByName("Patient"),
                         ThirdName = "cand2"
                     });
        }

        #region Implementation of ICandidateRepository

        public bool DeleteById(int candidate_id)
        {
            for (int i = 0; i < _entities.Count; i++)
            {
                Candidate candidate = _entities[i];
                if (candidate.Id == candidate_id)
                {
                    _entities.RemoveAt(i);
                }
            }
            return true;
        }

        public Candidate GetById(int candidate_id)
        {
            return _entities.Where(c => c.Id == candidate_id).FirstOrDefault();
        }

        #endregion
    }
}