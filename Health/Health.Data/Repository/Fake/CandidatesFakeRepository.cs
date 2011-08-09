using System;
using Health.API;
using Health.API.Entities;
using Health.API.Repository;
using Health.Data.Entities;

namespace Health.Data.Repository.Fake
{
    public sealed class CandidatesFakeRepository : CoreFakeRepository<ICandidate>, ICandidateRepository
    {
        public CandidatesFakeRepository(IDIKernel di_kernel, ICoreKernel core_kernel) : base(di_kernel, core_kernel)
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
                         Role = new Role
                                    {
                                        Name = "Patient",
                                        Code = 4234
                                    },
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
                         Role = new Role
                                    {
                                        Name = "Patient",
                                        Code = 4234
                                    },
                         ThirdName = "cand2"
                     });
        }
    }
}